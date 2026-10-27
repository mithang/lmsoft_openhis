using Newtouch.Infrastructure.EF;
using FrameworkBase.MultiOrg.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Core.Common;
using NewtouchHIS.Lib.Base.Exceptions;
using Newtouch.HIS.Domain.DTO.OutOrInStoredOperate;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.DrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using SqlSugar;
using System.Linq;

namespace Newtouch.HIS.DomainServices.BillManage
{
    /// <summary>
    /// Đơn lĩnh nội bộ
    /// </summary>
    public class ApplyDmnService : DmnServiceBase, IApplyDmnService
    {
        private readonly ISysMedicineStockInfoDmnService kcxxDmnService;
        private readonly ISysMedicineStorageIOReceiptRepo crkdjRepo;
        private readonly ISysMedicineStorageIOReceiptDetailRepo crkdjmxRepo;
        private readonly ISysMedicineRequisitionRepo sldRepo;
        private readonly ISysMedicineRequisitionDetailRepo sldmxRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseFactory"></param>
        
        /// <summary>
        /// Gửi lĩnh nội bộ
        /// </summary>
        /// <param name="sysMedicineRequisitionEntity"></param>
        /// <param name="sysMedicineRequisitionDetailEntities"></param>
        /// <returns></returns>
        public string SubmitApply(SysMedicineRequisitionEntity sysMedicineRequisitionEntity, List<SysMedicineRequisitionDetailEntity> sysMedicineRequisitionDetailEntities)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {
                db.Insert(sysMedicineRequisitionEntity);
                db.Insert(sysMedicineRequisitionDetailEntities);
                db.Commit();
                return "";
            }
        }

        /// <summary>
        /// Lấy thông tin chính đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="slbm">Bộ phận lĩnh</param>
        /// <returns></returns>
        public IList<ApplyMainVEntity> GetApplyMainInfo(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string yfbmCode, string organizeid, string slbm = "")
        {
            const string sql = @"
SELECT sld.sldId, LTRIM(RTRIM(sld.Sldh)) Sldh, sld.ffzt, slyfbm.yfbmmc slbmmc, ckyfbm.yfbmmc ckbmmc, sld.CreateTime, sld.CreatorCode, sld.LastModifyTime
FROM dbo.xt_yp_sld(NOLOCK) sld
LEFT JOIN [NewtouchHIS_Base].dbo.V_S_xt_yfbm(nolock) slyfbm on slyfbm.yfbmCode = sld.Slbm and slyfbm.OrganizeId = sld.OrganizeId 
LEFT JOIN [NewtouchHIS_Base].dbo.V_S_xt_yfbm(nolock) ckyfbm on ckyfbm.yfbmCode = sld.Ckbm and ckyfbm.OrganizeId = sld.OrganizeId
WHERE sld.OrganizeId=@Organizeid
AND sld.zt='1'
AND (sld.Slbm=@yfbmCode OR sld.Ckbm=@yfbmCode)
AND sld.Sldh LIKE '%'+@sldh+'%'
AND sld.CreateTime BETWEEN @startTime AND @endTime
AND (sld.ffzt=@ffzt OR -1=@ffzt)
AND (sld.Slbm=@rkbm OR ''=@rkbm)
";
            var param = new SugarParameter[] {
                new SugarParameter("@ffzt",ffzt),
                new SugarParameter("@Organizeid",organizeid),
                new SugarParameter("@yfbmCode",yfbmCode),
                new SugarParameter("@sldh",djh),
                new SugarParameter("@rkbm",slbm),
                new SugarParameter("@startTime",startTime),
                new SugarParameter("@endTime",endTime)
            };
            return QueryWithPage<ApplyMainVEntity>(sql, pagination, param);
        }

        /// <summary>
        /// Lấy chi tiết đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public IList<ApplyDetailVEntity> GetApplyDetails(Pagination pagination, string sldId, string organizeid)
        {
            const string sql = @"
SELECT sldmx.sldmxId, yp.ypCode ypdm, yp.ypmc, sfdl.dlmc, LTRIM(RTRIM(ISNULL(sldmx.ph,''))) ph, LTRIM(RTRIM(ISNULL(sldmx.pc,''))) pc,sldmx.Yxrq yxq
,dbo.f_getComplexYpSlandDw(sldmx.slsl, yp.bzs, yp.bzdw, yp.zxdw) slslStr,sldmx.slsl
,dbo.f_getComplexYpSlandDw(sldmx.yfsl,yp.bzs,yp.bzdw,yp.zxdw) yfslStr,sldmx.yfsl
,sldmx.LastModifyTime,sldmx.LastModifierCode 
FROM dbo.xt_yp_sldmx(NOLOCK) sldmx
INNER JOIN dbo.xt_yp_sld(NOLOCK) sld ON sld.sldId=sldmx.sldId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=sldmx.ypCode AND yp.OrganizeId=sld.OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=sld.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=sld.OrganizeId AND sfdl.zt='1'
WHERE sld.OrganizeId=@Organizeid
AND sld.sldId=@sldId
AND sldmx.zt='1'
";
            var param = new SugarParameter[] {
                new SugarParameter("@Organizeid",organizeid),
                new SugarParameter("@sldId",sldId)
            };
            return QueryWithPage<ApplyDetailVEntity>(sql, pagination, param);
        }

        /// <summary>
        /// Lấy chi tiết xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <param name="yfbmCode">Bộ phận hiện tại</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public IList<ApplyOutStockVEntity> GetApplyOutStockDetail(Pagination pagination, string sldIds, string yfbmCode, string organizeid)
        {
            const string sql = @"
SELECT sldmx.sldmxId,sld.sldId,sld.Sldh sldh,sld.Slbm
,CONVERT(INT,(sldmx.slsl-sldmx.yfsl)) xfsl,CONVERT(INT,(sldmx.slsl-sldmx.yfsl)) sysl,1 zhyz,yp.zxdw,CONVERT(INT,yp.bzs) bzs,yp.bzdw,(sldmx.slsl-sldmx.yfsl) zxdwxfsl
,dbo.f_getComplexYpSlandDw(sldmx.yfsl,yp.bzs,yp.bzdw,yp.zxdw) yfslStr,dbo.f_getComplexYpSlandDw(sldmx.slsl,yp.bzs,yp.bzdw,yp.zxdw) slslStr
,yp.ypmc,yp.ypCode ypdm,yp.ycmc sccj
,dbo.f_getComplexYpSlandDw(SUM(kcxx.kcsl-kcxx.djsl),yp.bzs,yp.bzdw,yp.zxdw) kyslStr,SUM(kcxx.kcsl-kcxx.djsl) kykc
,CONCAT(CONVERT(NUMERIC(11,2),yp.pfj),'元/'+yp.bzdw) pfjdwdj,CONCAT(CONVERT(NUMERIC(11,2),yp.lsj),'元/'+yp.bzdw) lsjdwdj
,CONVERT(NUMERIC(11,4),yp.pfj/yp.bzs) zxdwpfj,CONVERT(NUMERIC(11,4),yp.lsj/yp.bzs) zxdwlsj
,CONVERT(NUMERIC(11,2),yp.pfj*sldmx.slsl/yp.bzs) pjze,CONVERT(NUMERIC(11,2),yp.lsj*sldmx.slsl/yp.bzs) lsze 
FROM dbo.xt_yp_sldmx(NOLOCK) sldmx 
INNER JOIN dbo.xt_yp_sld(NOLOCK) sld ON sld.sldId=sldmx.sldId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=sldmx.ypCode AND yp.OrganizeId=sld.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=sld.OrganizeId
LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=sldmx.ypCode AND kcxx.zt='1' AND kcxx.yfbmCode=@yfbmCode
WHERE sld.OrganizeId=@Organizeid
AND sld.sldId IN (SELECT * FROM dbo.f_split(@sldIds,','))
AND sldmx.zt='1'
GROUP BY sldmx.sldmxId,sld.sldId,sld.Slbm,sld.Sldh,sldmx.slsl,sldmx.yfsl,yp.bzs,yp.bzdw,yp.zxdw,yp.ypmc,yp.ypCode,yp.ycmc,yp.pfj,yp.lsj
";
            var param = new SugarParameter[] {
                new SugarParameter("@Organizeid",organizeid),
                new SugarParameter("@sldIds",sldIds),
                new SugarParameter("@yfbmCode",yfbmCode)
            };
            return QueryWithPage<ApplyOutStockVEntity>(sql, pagination, param);
        }

        /// <summary>
        /// Sửa trạng thái phát đơn lĩnh
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="ffzt">Trạng thái đích</param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public string UpgradeStatus(string sldId, int ffzt, string organizeid)
        {
            var sql = string.Format(@"
UPDATE dbo.xt_yp_sld SET ffzt={0} WHERE sldId=@sldId AND zt='1' AND OrganizeId=@Organizeid
SELECT @@ROWCOUNT;
", ffzt);
            var param = new SugarParameter[] {
                new SugarParameter("@Organizeid",organizeid),
                new SugarParameter("@sldId",sldId)
            };
            return FirstOrDefault<int>(sql, param) > 0 ? "" : "Sửa trạng thái phát thất bại";
        }

        /// <summary>
        /// Gửi phát thuốc theo đơn lĩnh Lấy đơn lĩnh làm nguyên tử，Kết quả chỉ có hai loại 1.Tất cả thành công 2.Tất cả thất bại   Không có trường hợp thành công một nửa 
        /// </summary>
        /// <param name="group">Thông tin phát thuốc của cùng một đơn lĩnh</param>
        /// <param name="source">Tất cả thông tin phát thuốc</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubmitAppOutStock(List<ApplyOutStockVEntity> group, List<ApplyOutStockVEntity> source, string organizeId, string userCode)
        {
            using (var db = new EFDbTransaction().BeginTrans())
            {

                var curSldId = source[0].sldId;
                var tmpFrozenSuccesseList = new FrozenStockAndGenerateCrkdjDTO
                {
                    rkbm = source[0].slbm,
                    organizeId = organizeId,
                    userCode = userCode,
                    yfbmCode = Constants.CurrentYfbm.yfbmCode,
                    frozenseMedicines = new List<FrozenedMedicineInfoDTO>()
                };
                foreach (var item in group)
                {
                    var frozenBatchesInfo = new List<FrozenBatchesDetailVEntity>();
                    var frozenResult = kcxxDmnService.FrozenStork(item.xfsl, item.ypdm, tmpFrozenSuccesseList.yfbmCode, tmpFrozenSuccesseList.organizeId, tmpFrozenSuccesseList.userCode, out frozenBatchesInfo);
                    if (!string.IsNullOrWhiteSpace(frozenResult))
                    {
                        return frozenResult;
                    }
                    else
                    {
                        tmpFrozenSuccesseList.frozenseMedicines.Add(new FrozenedMedicineInfoDTO
                        {
                            djsl = item.xfsl,
                            ypdm = item.ypdm,
                            bzs = item.bzs,
                            kykc = item.kykc,
                            sldmxId = item.sldmxId,
                            zhyz = item.zhyz,
                            //zxdwjj = (decimal)item.zxdwjj,
                            zxdwlsj = item.zxdwlsj,
                            zxdwpfj = item.zxdwpfj,
                            frozenedMedicineBatchs = frozenBatchesInfo
                        });
                    }
                    source.Remove(item);
                }
                var resultGenerateCrkdj = GenerateCrkdj(tmpFrozenSuccesseList);
                if (!string.IsNullOrWhiteSpace(resultGenerateCrkdj))
                {
                    return resultGenerateCrkdj;
                }
                UpdateSld(db, curSldId, group);
                db.Commit();
                return "";
            }
        }

        /// <summary>
        /// Sửa thông tin đơn lĩnh
        /// </summary>
        /// <param name="db"></param>
        private void UpdateSld(Newtouch.Infrastructure.EFDbTransaction db, string sldId, List<ApplyOutStockVEntity> group)
        {
            #region Sửa thông tin bảng chính
            var sldEntity = sldRepo.FindEntity(sldId);
            if (sldEntity == null) throw new Exception(string.Format("Không tìm thấy khóa chính là【{0}】đơn lĩnh của", sldId));
            sldEntity.ffzt = IsPartProvide(group) ? (int)EnumSLDDeliveryStatus.Part : (int)EnumSLDDeliveryStatus.All;
            sldEntity.Modify();
            db.Update(sldEntity);
            #endregion

            #region Sửa thông tin bảng chi tiết
            group.ForEach(p =>
            {
                var mx = sldmxRepo.FindEntity(p.sldmxId);
                if (mx == null) throw new Exception(string.Format("Không tìm thấy khóa chính là【{0}】chi tiết đơn lĩnh của", p.sldmxId));
                mx.yfsl = mx.yfsl + p.xfsl;
                mx.Modify();
                db.Update(mx);
            });
            #endregion
        }

        /// <summary>
        /// Kiểm tra có phải phát thuốc một phần không
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool IsPartProvide(List<ApplyOutStockVEntity> source)
        {
            var result = new List<ApplyOutStockVEntity>();
            if (source == null || source.Count <= 0) throw new FailedException("Thông tin phát thuốc không được để trống");
            foreach (var p in source)
            {
                if (p.sysl != p.xfsl) return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fsgc">đóng băng thành công</param>
        /// <returns></returns>
        private string GenerateCrkdj(FrozenStockAndGenerateCrkdjDTO fsgc)
        {
            if (fsgc.frozenseMedicines == null || fsgc.frozenseMedicines.Count <= 0) throw new FailedException("Không có dữ liệu phát thuốc thành công");
            var crkdj = AssembleCrkdjInfo(fsgc.yfbmCode, fsgc.organizeId, fsgc.userCode, fsgc.rkbm);
            var crkdjmx = new List<SysMedicineStorageIOReceiptDetailEntity>();
            fsgc.frozenseMedicines.ForEach(p =>
            {
                crkdjmx.AddRange(AssembleCrkdjmxInfo(p, crkdj));
            });
            crkdjRepo.Insert(crkdj);
            crkdjmxRepo.Insert(crkdjmx);
            return "";
        }

        /// <summary>
        /// Lắp ráp bảng chính chứng từ xuất nhập kho
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeid"></param>
        /// <param name="userCode"></param>
        /// <param name="rkbm"></param>
        /// <param name="dj"></param>
        private SysMedicineStorageIOReceiptEntity AssembleCrkdjInfo(string yfbmCode, string organizeid, string userCode, string rkbm)
        {
            var result = new SysMedicineStorageIOReceiptEntity
            {
                Ckbm = yfbmCode,
                CreateTime = DateTime.Now,
                CreatorCode = userCode,
                Crkfsdm = "",
                djlx = (int)EnumDanJuLX.shenlingfayao,
                Czsj = DateTime.Now,
                crkId = Guid.NewGuid().ToString(),
                Pdh = EFDBBaseFuncHelper.Instance.GetNewMedicineReceiptNo("内部申领出库", yfbmCode, organizeid),
                OrganizeId = organizeid,
                Rkbm = rkbm,
                Sqsj = DateTime.Now,
                zt = "1",
                shzt = ((int)EnumDjShzt.WaitingApprove).ToString()
            };
            result.Create();
            return result;
        }

        /// <summary>
        /// Lắp ráp chi tiết chứng từ xuất nhập kho
        /// </summary>
        /// <param name="dj"></param>
        /// <param name="frozenList"></param>
        /// <returns></returns>
        private List<SysMedicineStorageIOReceiptDetailEntity> AssembleCrkdjmxInfo(FrozenedMedicineInfoDTO fmi, SysMedicineStorageIOReceiptEntity dj)
        {
            var result = new List<SysMedicineStorageIOReceiptDetailEntity>();
            if (fmi.frozenedMedicineBatchs.Count <= 0)
            {
                throw new FailedException("Không tìm thấy thông tin lô đóng băng hiệu lực");
            }
            fmi.frozenedMedicineBatchs.ForEach(p =>
            {
                var item = new SysMedicineStorageIOReceiptDetailEntity
                {
                    Ckbmkc = fmi.kykc,
                    Ckzhyz = fmi.zhyz,
                    CreateTime = DateTime.Now,
                    CreatorCode = dj.CreatorCode,
                    crkId = dj.crkId,
                    crkmxId = Guid.NewGuid().ToString(),
                    jj = p.jj / fmi.bzs * fmi.zhyz,
                    Lsj = fmi.zxdwlsj * fmi.zhyz,
                    pc = p.pc,
                    Pfj = fmi.zxdwpfj * fmi.zhyz,
                    Ph = p.ph,
                    Sl = fmi.djsl / fmi.zhyz,
                    sldmxId = fmi.sldmxId,
                    Yklsj = fmi.zxdwlsj * fmi.bzs,
                    Ykpfj = fmi.zxdwpfj * fmi.bzs,
                    Ypdm = fmi.ypdm,
                    Yxq = p.yxq,
                    Zje = fmi.zxdwjj * fmi.djsl,
                    zt = "1"
                };
                item.Create();
                result.Add(item);
            });
            return result;
        }

        /// <summary>
        /// Theo chứng từIDLấy chi tiết chứng từ xuất nhập kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        public List<ReceiptQueryDetailVO> SelectOutOrInStorageBillDetail(string crkId, int djlx)
        {
            var sql = new System.Text.StringBuilder(@"
SELECT crkdjmx.crkmxId,crkdjmx.Yxq yxq,REPLACE(LTRIM(RTRIM(CONVERT(VARCHAR(20),crkdjmx.Fph))),'	','') fph,yp.dlCode ypdlCode,sfdl.dlmc yplbmc,yp.py,yp.ypmc,ypsx.ypgg gg,yp.ycmc sccj,crkdjmx.Sl sl
,CONVERT(NUMERIC(11,2),crkdjmx.Pfj*crkdjmx.Sl) pjze
,CONVERT(NUMERIC(11,2),crkdjmx.Lsj*crkdjmx.Sl) ljze
,CONCAT(isnull(crkdjmx.jj,0.00),'元/',yp.bzdw) jjdwdj ");
            sql.Append(new[] {(int) EnumDanJuLX.yaopinruku}.Contains(djlx)
                ? @"
,dbo.f_getComplexYpSlandDw(crkdjmx.Sl*crkdjmx.Rkzhyz,yp.bzs,yp.bzdw,yp.zxdw) slanddw
,CONVERT(NUMERIC(11,2),(yp.Lsj-isnull(crkdjmx.jj,0.00))*crkdjmx.Sl*crkdjmx.rkzhyz/yp.bzs) jxcj
,CONVERT(NUMERIC(11,2),ISNULL(isnull(crkdjmx.jj,0.00)*crkdjmx.Sl*crkdjmx.rkzhyz/yp.bzs,0)) zje "
                : @"
,dbo.f_getComplexYpSlandDw(crkdjmx.Sl*crkdjmx.Ckzhyz,yp.bzs,yp.bzdw,yp.zxdw) slanddw
,CONVERT(NUMERIC(11,4),(yp.Lsj-isnull(crkdjmx.jj,0.00))*crkdjmx.Sl*crkdjmx.ckzhyz/yp.bzs) jxcj
,CONVERT(NUMERIC(11,2),ISNULL(isnull(crkdjmx.jj,0.00)*crkdjmx.Sl*crkdjmx.ckzhyz/yp.bzs,0)) zje ");
            sql.Append(@"
,crkdjmx.kl,LTRIM(LTRIM(ISNULL(crkdjmx.pc,''))) pc,LTRIM(RTRIM(ISNULL(crkdjmx.Ph,''))) ph,ISNULL(crkdjmx.Thyy,'') thyy,
crkdjmx.zsm, case crkdjmx.sfcl when 1 then '是' when 2 then '否' else  '' end as sfcl
FROM dbo.xt_yp_crkdj(NOLOCK) crkdj
INNER JOIN dbo.xt_yp_crkmx(NOLOCK) crkdjmx ON crkdjmx.crkId=crkdj.crkId AND crkdjmx.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=crkdjmx.Ypdm AND yp.OrganizeId=crkdj.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=crkdj.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=crkdj.OrganizeId AND sfdl.zt='1'
WHERE crkdj.crkId=@crkId ");
            var param = new SugarParameter[] {
                new SugarParameter("@crkId",crkId)
            };
            return FindList<ReceiptQueryDetailVO>(sql.ToString(), param);
        }
        /// <summary>
        /// Theo chứng từIDLấy chi tiết chứng từ xuất nhập kho
        /// </summary>
        /// <param name="crkId"></param>
        /// <param name="djlx"></param>
        /// <returns></returns>
        public List<DrupreparationMXVO> SelectDrupreparationInfoMX(string byid)
        {
            var sql = new System.Text.StringBuilder(@"SELECT yp.dlCode ypdlCode,sfdl.dlmc yplbmc,yp.py,yp.ypmc,ypsx.ypgg gg,yp.ycmc sccj,ksbymx.Sl sl
,CONVERT(NUMERIC(11,2),CONVERT(NUMERIC(11,2),ksbymx.Pfj)*CONVERT(NUMERIC(11,2),ksbymx.Sl)) pjze
,CONVERT(NUMERIC(11,2),CONVERT(NUMERIC(11,2),ksbymx.Lsj)*CONVERT(NUMERIC(11,2),ksbymx.Sl)) ljze
,dbo.f_getComplexYpSlandDw(CONVERT(NUMERIC(11),ksbymx.Sl)*CONVERT(NUMERIC(11),ksbymx.zhyz),yp.bzs,yp.bzdw,yp.zxdw) slanddw
FROM dbo.[xt_bqksby](NOLOCK) ksby
INNER JOIN dbo.xt_bqksbymx(NOLOCK) ksbymx ON ksby.Id=ksbymx.byid AND ksby.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=ksbymx.Ypdm AND yp.OrganizeId=ksby.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=ksby.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=ksby.OrganizeId AND sfdl.zt='1'
where ksby.Id=@byid
");
            var param = new SugarParameter[] {
                new SugarParameter("@byid",byid)
            };
            return FindList<DrupreparationMXVO>(sql.ToString(), param);
        }
    }
}
