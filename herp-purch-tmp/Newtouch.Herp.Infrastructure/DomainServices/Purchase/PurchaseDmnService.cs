using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.DomainServices;
using Newtouch.Herp.Domain.ValueObjects.Purchase;
using Newtouch.Herp.Infrastructure;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.DomainServices.Purchase
{
    public class PurchaseDmnService : DmnServiceBase, IPurchaseDmnService
    {
        public PurchaseDmnService()
        {
        }


        public List<PurchaseEntity> QueryPurchasebyId(string cgId, string orgId)
        {

            var strSql = new StringBuilder(@"
select * from xt_wz_cg a
  where a.zt = '1'
  and a.OrganizeId=@orgId
  and a.cgId=@cgId
");
            var param = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@cgId", cgId),
            };

            return this.FindList<PurchaseEntity>(strSql.ToString(), param.ToArray());
        }
        public List<PurchaseDetailVO> QueryPurchaseDetailbyId(string cgId, string orgId)
        {

            var strSql = new StringBuilder(@"

select cgmx.*,wz.name productmc ,gys.name qymc ,
case cglx when 1 then 'Đấu thầu trong phạm vi BHYT' when 2 then 'Chưa đấu thầu trong phạm vi BHYT' end cglxmc,
case dcpsbs when 0 then 'Không cho phép' when 1 then 'Cho phép' end dcpsbsmc,
case sfjj when 0 then 'Không cần gấp' when 1 then 'Cần gấp' end sfjjmc,
case psyq when 1 then 'Giao theo đơn' when 2 then 'Giao theo nhu cầu' end psyqmc
from xt_wz_cgmx cgmx
left join [dbo].[wz_product] wz on cgmx.productCode =wz.productCode and cgmx.OrganizeId=wz.OrganizeId and cgmx.zt=wz.zt
left join gys_supplier gys on cgmx.qybm=gys.py and cgmx.OrganizeId=gys.OrganizeId and cgmx.zt=gys.zt

  where cgmx.zt = '1'
  and cgmx.OrganizeId=@orgId
  and cgmx.cgId=@cgId
");


            var param = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@cgId", cgId),
            };

            return this.FindList<PurchaseDetailVO>(strSql.ToString(), param.ToArray());
        }

        public string PurchaseSubmit(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string orgId, OperatorModel user)
        {
            try
            {
                if (cgEntity.cgId != null)
                {
                    string sql2 = "delete from xt_wz_cgmx where cgId=@cgId and OrganizeId=@orgId and zt='1' ";
                    SugarParameter[] para2 = {
                            new SugarParameter("@cgId", cgEntity.cgId),
                            new SugarParameter("@orgId", orgId) };
                    ExecuteSqlCommand(sql2, para2);
                    return "Cập nhật đơn mua sắm thành công!";
                }
                else
                {
                    using (var db = new EFDbTransaction(Db).BeginTrans())
                    {
                        var cgId = Guid.NewGuid().ToString();
                        var cg = new PurchaseEntity
                        {
                            cgId = cgId,
                            OrganizeId = orgId,
                            ddsj = DateTime.Now.ToString("yyyy-MM-dd HH:ss:mm"),
                            ddbh = "CG" + DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                            ddzt = 1,
                            zt = "1",
                            CreatorCode = user.rygh,
                            CreateTime = DateTime.Now,
                            LastModifierCode = "",
                            LastModifyTime = null,
                            czlx = cgEntity.czlx,
                            ddlx = cgEntity.ddlx,
                            psdbm = cgEntity.psdbm,
                            yybm = cgEntity.yybm,
                            yyjhdh = cgEntity.yyjhdh,
                            zwdhrq = cgEntity.zwdhrq,
                        };
                        db.Insert(cg);
                        foreach (var cgmxEntity in cgmxList)
                        {
                            cgmxEntity.cgmxId = Guid.NewGuid().ToString();
                            cgmxEntity.cgId = cgId;
                            cgmxEntity.OrganizeId = orgId;
                            cgmxEntity.zt = "1";
                            cgmxEntity.CreatorCode = user.rygh;
                            cgmxEntity.CreateTime = DateTime.Now;
                            cgmxEntity.LastModifierCode = "";
                            cgmxEntity.LastModifyTime = null;
                            db.Insert(cgmxEntity);
                        }
                        db.Commit();
                    }
                    return "Lưu đơn mua sắm thành công!";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region 采购退货

        public List<ReturnedEntity> QueryPurchaseReturnbyId(string thId, string orgId)
        {

            var strSql = new StringBuilder(@"
select * from xt_wz_th a
  where a.zt = '1'
  and a.OrganizeId=@orgId
  and a.thId=@thId
");
            var param = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@thId", thId),
            };

            return this.FindList<ReturnedEntity>(strSql.ToString(), param.ToArray());
        }

        public List<PurchaseReturnDetailVO> QueryPurchaseReturnDetailbyId(string thId, string orgId)
        {

            var strSql = new StringBuilder(@"

select thmx.*,wz.name productmc ,gys.name qymc ,
case cglx when 1 then 'Mua sắm đấu thầu' when 2 then 'Mua sắm theo số lượng' when 3 then 'Mua sắm qua mạng' when 9 then 'Khác'end cglxmc,
case thlx when 1 then 'Trả hàng bình thường' when 2 then 'Trả hàng bù chênh lệch' end thlxmc  ,
case psmxtmlx when '01' then 'Mã vạch GS1' when '02' then 'Mã vạch HIBC' when '99' then 'Khác' end psmxtmlxmc
from xt_wz_thmx thmx
left join [dbo].[wz_product] wz on thmx.YYBDDM =wz.productCode and thmx.OrganizeId=wz.OrganizeId and thmx.zt=wz.zt
left join gys_supplier gys on thmx.qybm=gys.py and thmx.OrganizeId=gys.OrganizeId and thmx.zt=gys.zt

  where thmx.zt = '1'
  and thmx.OrganizeId=@orgId
  and thmx.thId=@thId
");


            var param = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@thId", thId),
            };

            return this.FindList<PurchaseReturnDetailVO>(strSql.ToString(), param.ToArray());
        }

        #endregion

        #region 外部入库 导入采购单
        public List<PurchaseStoreVO> QueryPurchaseStorebyId(string cgId, string orgId, string warehouseId)
        {
            var strSql = new StringBuilder(@"

select * from 
(
select cgmx.*,wz.name productmc ,gys.name qymc ,wz.Id productId,
case cglx when 1 then 'Mua sắm đấu thầu' when 2 then 'Mua sắm theo số lượng' when 3 then 'Mua sắm qua mạng' when 9 then 'Khác' end  cglxmc,
case psyq when 1 then 'Giao theo đơn' when 2 then 'Giao theo nhu cầu' end psyqmc
from xt_wz_cgmx cgmx
left join wz_product wz on cgmx.productCode =wz.productCode and cgmx.OrganizeId=wz.OrganizeId and cgmx.zt=wz.zt
left join gys_supplier gys on cgmx.qybm=gys.py and cgmx.OrganizeId=gys.OrganizeId and cgmx.zt=gys.zt

  where cgmx.zt = '1'
  and cgmx.OrganizeId=@orgId
  and cgmx.cgId=@cgId
  ) a
  left join (
SELECT dbo.f_getComplexWzSlandDw(kykcsl,zhyz, bmdw.name, zxdw.name) slstr
, zxdw.name mindwmc,bmdw.name bmdwmc, a.* 
FROM (
    SELECT p.Id,p.OrganizeId,p.name,p.minUnit zxdwId, rpw.unitId bmdwId 
    ,ISNULL(SUM(kcxx.kcsl),0) kcsl, ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0) kykcsl
    ,p.lsj minlsj, ISNULL(CONVERT(NUMERIC(11,4),p.lsj*rpu.zhyz),0) bmlsj
    ,p.py,p.gg,p.supplierId,s.name supplierName,wt.name lbmc,p.typeId lbId,ISNULL(rpu.zhyz,1) zhyz,ISNULL(rpu.zhyz,1) bmdwzhyz,p.productCode,p.hcgjybdm,p.hcgjybdm gjybdm
    FROM dbo.wz_product(NOLOCK) p 
    INNER JOIN dbo.gys_supplier(NOLOCK) s ON p.supplierId = s.Id AND p.OrganizeId = s.OrganizeId AND s.zt = '1'
    INNER JOIN dbo.rel_productWarehouse(NOLOCK) rpw ON rpw.OrganizeId = p.OrganizeId AND rpw.productId = p.Id 
    LEFT JOIN dbo.kf_kcxx(NOLOCK) kcxx ON kcxx.productId=p.Id AND kcxx.warehouseId=rpw.warehouseId AND kcxx.zt='1'
    LEFT JOIN dbo.kf_crkmx(NOLOCK) mx ON mx.Id=kcxx.crkmxId AND mx.pc=kcxx.pc AND mx.ph=kcxx.ph AND mx.zt='1' 
    LEFT JOIN dbo.kf_crkdj(NOLOCK) dj ON dj.Id=mx.crkId AND dj.OrganizeId=p.OrganizeId AND dj.zt='1'
    LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu on rpu.productId=rpw.productId AND rpu.unitId=rpw.unitId AND rpu.OrganizeId=p.OrganizeId AND rpu.zt='1'
    LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wt ON wt.id = p.typeId AND wt.zt='1'
    WHERE p.OrganizeId = @orgId 
    AND rpw.warehouseId=@warehouseId 
    AND rpw.zt='1'
    AND p.zt = 1
	
    GROUP BY p.Id, p.OrganizeId, p.name, p.py, p.supplierId, s.name, p.minUnit, p.gg, wt.name,p.typeId,p.lsj,rpu.zhyz, p.minUnit, rpw.unitId,p.productCode,p.hcgjybdm) a
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) bmdw ON bmdw.Id=a.bmdwId AND bmdw.zt='1'  
LEFT JOIN NewtouchHIS_Base.dbo.wz_unit(NOLOCK) zxdw ON zxdw.Id=a.zxdwId AND zxdw.zt='1' 
) b on a.productCode=b.productCode
");


            var param = new List<SugarParameter>
            {
                new SugarParameter("@orgId", orgId),
                new SugarParameter("@cgId", cgId),
                new SugarParameter("@warehouseId", warehouseId),
            };

            return this.FindList<PurchaseStoreVO>(strSql.ToString(), param.ToArray());
        }
        #endregion

    }
}
