using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.DomainServices.Product
{
    /// <summary>
    /// Thông tin điều chỉnh giá vật tư. SqlSugar port of the Framework service: DbParameter ->
    /// SugarParameter. Approval is split into UPDATE + SELECT (the original
    /// single multi-statement batch) so each step is explicit. The
    /// FailedException rollback branches are expressed directly (restore the
    /// old retail price / execution flag) without the binary exception type.
    /// </summary>
    public class WzPriceAdjustmentDmnService : DmnServiceBase, IWzPriceAdjustmentDmnService
    {
        private readonly IWzProductDmnService _wzProductDmnService;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IWzPriceAdjustmentProfitAndLossRepo _wzPriceAdjustmentProfitAndLossRepo;
        private readonly IWzPriceAdjustmentRepo _wzPriceAdjustmentRepo;

        public WzPriceAdjustmentDmnService(IWzProductDmnService wzProductDmnService, IWzProductRepo wzProductRepo,
            IWzPriceAdjustmentProfitAndLossRepo wzPriceAdjustmentProfitAndLossRepo, IWzPriceAdjustmentRepo wzPriceAdjustmentRepo)
        {
            _wzProductDmnService = wzProductDmnService;
            _wzProductRepo = wzProductRepo;
            _wzPriceAdjustmentProfitAndLossRepo = wzPriceAdjustmentProfitAndLossRepo;
            _wzPriceAdjustmentRepo = wzPriceAdjustmentRepo;
        }

        public IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentList(Pagination pagination, string keyWord, string shzt, string organizeId, string zxbz = "")
        {
            const string sql = @"
SELECT pa.wztjId, pa.shzt, pa.zxbz, lb.name lbmc, wz.name wzmc, wz.gg, pa.lsj, pa.ylsj, pa.zhyz, pa.dwmc, gys.name supplierName, pa.zt, pa.tzwj, pa.tzsj, pa.zxsj
,CASE WHEN pa.zxsj<=getdate() then 'true' else 'false' end Isgq
FROM dbo.wz_priceAdjustment(NOLOCK) pa
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=pa.productId AND wz.OrganizeId=pa.OrganizeId AND wz.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId AND gys.zt='1'
WHERE pa.OrganizeId=@OrganizeId
AND (pa.shzt=@shzt OR ''=@shzt)
AND (wz.py LIKE '%'+ @keyWord +'%' OR wz.name LIKE '%'+@keyWord+'%')
AND (pa.zxbz=@zxbz OR ''=@zxbz)
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@keyWord", keyWord ?? ""),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@shzt", shzt ?? ""),
                new SugarParameter("@zxbz", zxbz)
            };
            return QueryWithPage<VPriceAdjustmentInfoEntity>(sql, pagination, param);
        }

        public string Approval(List<string> idList, string operationType, string userCode, string organizeId)
        {
            if (idList == null || idList.Count <= 0) return "";
            // Original mutated operationType inside the loop (revoke -> waiting
            // on the first item, then stayed waiting); map once up front.
            if (operationType == ((int)EnumTjShzt.Revoke).ToString())
            {
                operationType = ((int)EnumTjShzt.Waiting).ToString();
                userCode = "";
            }
            var expireApply = new List<string>();
            foreach (var id in idList)
            {
                var updateSql = @"
UPDATE dbo.wz_priceAdjustment
SET shzt=@shzt, shczy=@userCode, LastModifierCode=@userCode, LastModifyTime=GETDATE()
WHERE wztjId=@Id AND OrganizeId=@OrganizeId AND zxbz=@zxbz AND zxsj>GETDATE()
";
                var param = new SugarParameter[]
                {
                    new SugarParameter("@shzt", operationType),
                    new SugarParameter("@userCode", userCode),
                    new SugarParameter("@Id", id),
                    new SugarParameter("@OrganizeId", organizeId),
                    new SugarParameter("@zxbz", ((int)EnumTjZxbz.Not).ToString())
                };
                ExecuteSqlCommand(updateSql, param);
                const string selectSql = @"
SELECT wz.name wzmc, pa.ylsj, pa.lsj, pa.zhyz, pa.zxsj
FROM dbo.wz_priceAdjustment pa
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=pa.productId AND wz.OrganizeId=pa.OrganizeId AND wz.zt='1'
WHERE pa.wztjId=@Id AND pa.zxbz=@zxbz AND pa.OrganizeId=@OrganizeId
";
                var ret = FirstOrDefault<VPriceAdjustmentInfoEntity>(selectSql, param);
                if (ret != null && ret.zxsj < DateTime.Now && ret.zxsj > Constants.MinDateTime) expireApply.Add(ret.wzmc);
            }
            return expireApply.Count > 0 ? string.Format("Thao tác thất bại đối với vật tư có mã {0}, đã quá thời gian thực hiện muộn nhất.", string.Join(",", expireApply)) : "";
        }

        public List<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(string wztjId, string productId, string organizeid)
        {
            const string sql = @"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#kcxx') and type='U')
BEGIN
    DROP TABLE #kcxx;
END
SELECT SUM(kcsl/zhyz) AS dssl, warehouseId, zhyz, productId
INTO #kcxx
FROM dbo.kf_kcxx
WHERE productId=@proId AND ISNULL(kcsl, 0)>0 AND OrganizeId=@OrganizeId AND zt='1'
GROUP BY warehouseId, zhyz, productId

SELECT tj.productId, kcxx.warehouseId, tj.zxsj tjsj, tj.tzwj tjwj, kcxx.dssl, tj.ylsj, tj.lsj xlsj, kcxx.zhyz
,(tj.lsj-tj.ylsj)*kcxx.dssl lsjtjlr
FROM dbo.wz_priceAdjustment(NOLOCK) tj
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=tj.productId AND wz.OrganizeId=tj.OrganizeId
INNER JOIN #kcxx kcxx ON tj.productId=kcxx.productId
WHERE tj.OrganizeId=@OrganizeId
AND tj.wztjId=@Id
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeid),
                new SugarParameter("@Id", wztjId),
                new SugarParameter("@proId", productId)
            };
            return FindList<VPriceAdjustmentProfitLossEntity>(sql, param);
        }

        public string AdjustPriceExecute(WzPriceAdjustmentEntity tjInfo)
        {
            var oldzxsj = DateTime.Now.AddDays(1);
            try
            {
                if (_wzProductDmnService.UpdateProductPrice(tjInfo.productId, tjInfo.lsj, tjInfo.tzczy, tjInfo.OrganizeId) > 0)
                {
                    var executeTime = DateTime.Now;
                    tjInfo.zxbz = ((int)EnumTjZxbz.Already).ToString();
                    oldzxsj = tjInfo.zxsj;
                    tjInfo.zxsj = executeTime;
                    tjInfo.zxczy = OperatorProvider.GetCurrent().UserCode;
                    tjInfo.Modify();
                    if (_wzPriceAdjustmentRepo.Update(tjInfo) <= 0)
                    {
                        _wzProductDmnService.UpdateProductPrice(tjInfo.productId, tjInfo.ylsj, tjInfo.tzczy, tjInfo.OrganizeId);
                        return "Sửa cờ thực hiện điều chỉnh giá thất bại";
                    }

                    var lrList = GetPriceAdjustmentProfitLoss(tjInfo.wztjId, tjInfo.productId, tjInfo.OrganizeId);
                    var tjlrList = new List<WzPriceAdjustmentProfitAndLossEntity>();
                    if (lrList != null && lrList.Count > 0)
                    {
                        foreach (var p in lrList)
                        {
                            var tjlr = new WzPriceAdjustmentProfitAndLossEntity
                            {
                                dssl = p.dssl,
                                lsjtjlr = p.lsjtjlr,
                                OrganizeId = tjInfo.OrganizeId,
                                pc = "",
                                ph = "",
                                productId = p.productId,
                                tjsj = p.tjsj,
                                tjwj = p.tjwj,
                                warehouseId = p.warehouseId,
                                xlsj = p.xlsj,
                                ylsj = p.ylsj,
                                zhyz = p.zhyz,
                                zt = ((int)Enumzt.Enable).ToString()
                            };
                            tjlr.Create(true);
                            if (string.IsNullOrWhiteSpace(tjlr.TjsyId)) tjlr.TjsyId = Guid.NewGuid().ToString();
                            tjlrList.Add(tjlr);
                        }
                    }
                    if (tjlrList.Count <= 0) return "";
                    if (_wzPriceAdjustmentProfitAndLossRepo.Insert(tjlrList) <= 0)
                    {
                        _wzProductDmnService.UpdateProductPrice(tjInfo.productId, tjInfo.ylsj, tjInfo.tzczy, tjInfo.OrganizeId);
                        tjInfo.zxsj = oldzxsj;
                        tjInfo.zxczy = "";
                        tjInfo.zxbz = ((int)EnumTjZxbz.Not).ToString();
                        tjInfo.Modify();
                        _wzPriceAdjustmentRepo.Update(tjInfo);
                        return "Chèn lợi nhuận điều chỉnh giá thất bại";
                    }
                }
                else
                {
                    var wz1 = _wzProductRepo.FindEntity(p => p.Id == tjInfo.productId && p.OrganizeId == tjInfo.OrganizeId);
                    return string.Format("Sửa giá bán lẻ của vật tư [{0}] thất bại!", wz1 != null ? wz1.name : "");
                }
                return "";
            }
            catch (Exception ex)
            {
                var wz2 = _wzProductRepo.FindEntity(p => p.Id == tjInfo.productId && p.OrganizeId == tjInfo.OrganizeId);
                return string.Format("Vật tư [{0}] điều chỉnh giá thất bại, {1}", wz2 != null ? wz2.name : "", ex.Message);
            }
        }

        public IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentHistoryList(Pagination pagination, string keyWord, string organizeId, DateTime bt, DateTime et, string warehouseId)
        {
            const string sql = @"
SELECT pa.wztjId, pa.shzt, pa.zxbz, lb.name lbmc, wz.name wzmc, wz.gg, pa.lsj, pa.ylsj, pa.zhyz, pa.dwmc, gys.name supplierName, pa.zt, pa.tzwj, pa.tzsj, pa.zxsj, pa.CreatorCode
FROM dbo.wz_priceAdjustment(NOLOCK) pa
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=pa.productId AND wz.OrganizeId=pa.OrganizeId AND wz.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId AND gys.zt='1'
WHERE pa.OrganizeId=@OrganizeId
AND (pa.shzt=@shzt)
AND (wz.py LIKE '%'+ @keyWord +'%' OR wz.name LIKE '%'+@keyWord+'%')
AND pa.zxbz=@zxbz
AND pa.zxsj BETWEEN @kssj AND @jssj
AND pa.warehouseId=@warehouseId
";
            if (bt < Constants.MinDateTime) bt = Constants.MinDateTime;
            if (et < Constants.MinDateTime) et = Constants.MinDateTime;
            if (bt > et) et = bt.AddMinutes(1);

            var param = new SugarParameter[]
            {
                new SugarParameter("@keyWord", keyWord ?? ""),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@shzt", ((int)EnumTjShzt.Audited).ToString()),
                new SugarParameter("@zxbz", ((int)EnumTjZxbz.Already).ToString()),
                new SugarParameter("@kssj", bt),
                new SugarParameter("@jssj", et),
                new SugarParameter("@warehouseId", warehouseId)
            };
            return QueryWithPage<VPriceAdjustmentInfoEntity>(sql, pagination, param);
        }

        public IList<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(Pagination pagination, PriceAdjustmentProfitLossDTO searchDto)
        {
            var sql = new StringBuilder(@"
SELECT tjlr.TjsyId, tjlr.productId, lb.name lbmc, wz.name wzmc, wz.gg, gys.name supplierName
,ISNULL(CONVERT(INT,tjlr.dssl/tjlr.zhyz*rpu.zhyz),0) dssl, rpu.unit dwmc, tjlr.tjwj
,ISNULL(CONVERT(NUMERIC(11,4),tjlr.ylsj/tjlr.zhyz*rpu.zhyz), 0) ylsj, ISNULL(CONVERT(NUMERIC(11,4),tjlr.xlsj/tjlr.zhyz*rpu.zhyz), 0) xlsj
,tjlr.lsjtjlr, tjlr.zt, tjlr.CreatorCode, tjlr.tjsj
FROM dbo.wz_priceAdjustmentProfitAndLoss(NOLOCK) tjlr
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=tjlr.productId AND wz.OrganizeId=tjlr.OrganizeId AND wz.zt='1'
INNER JOIN dbo.rel_productWarehouse(NOLOCK) rpw ON rpw.productId=tjlr.productId AND rpw.warehouseId=tjlr.warehouseId AND rpw.OrganizeId=tjlr.OrganizeId AND rpw.zt='1'
LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu ON rpu.unitId=rpw.unitId AND rpu.productId=rpw.productId AND rpu.OrganizeId=tjlr.OrganizeId AND rpu.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1'
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId AND gys.zt='1'
WHERE tjlr.OrganizeId=@OrganizeId
AND (tjlr.warehouseId=@warehouseId OR ''=@warehouseId)
AND (wz.py LIKE '%'+ @keyWord +'%' OR wz.name LIKE '%'+@keyWord+'%')
AND tjlr.tjsj BETWEEN @kssj AND @jssj ");
            if (searchDto.lkc == "0")
            {
                sql.AppendLine("AND tjlr.dssl > 0 ");
            }
            if (searchDto.kfList == null || searchDto.kfList.Count == 0)
            {
                // Original produced IN () (SQL error) for users with no
                // warehouse list; return no rows instead.
                sql.AppendLine("AND 1=0 ");
            }
            else
            {
                sql.AppendLine(string.Format("AND tjlr.warehouseId IN ({0})", string.Join(",", searchDto.kfList)));
            }
            if (searchDto.startTime < Constants.MinDateTime) searchDto.startTime = Constants.MinDateTime;
            if (searchDto.endTime < Constants.MinDateTime) searchDto.endTime = Constants.MinDateTime;
            if (searchDto.startTime > searchDto.endTime) searchDto.endTime = searchDto.startTime.AddMinutes(1);
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", searchDto.organizeId),
                new SugarParameter("@warehouseId", searchDto.kfbm ?? ""),
                new SugarParameter("@keyWord", searchDto.keyWord ?? ""),
                new SugarParameter("@kssj", searchDto.startTime),
                new SugarParameter("@jssj", searchDto.endTime)
            };
            return QueryWithPage<VPriceAdjustmentProfitLossEntity>(sql.ToString(), pagination, param);
        }
    }
}
