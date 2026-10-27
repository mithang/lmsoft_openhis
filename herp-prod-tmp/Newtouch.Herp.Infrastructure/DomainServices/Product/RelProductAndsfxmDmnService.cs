using System.Collections.Generic;
using System.Text;
using SqlSugar;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.DomainServices
{
    /// <summary>
    /// 物资收费项目对照. NOTE: this file was not in the brief's file list
    /// (which listed RelProductAndsfxmApp/Repo but not this DmnService);
    /// it is ported here because ProductSfxmController's grid depends on it
    /// and no other area owns rel_productAndsfxm.
    /// </summary>
    public class RelProductAndsfxmDmnService : DmnServiceBase, IRelProductAndsfxmDmnService
    {
        public RelProductAndsfxmDmnService()
        {
        }

        public IList<RelProductAndsfxmVo> SelectProductAndsfxm(Pagination pagination, string sfdlCode, string productTypeId, string organizeId, string zt)
        {
            var sql = new StringBuilder(@"
SELECT relps.Id, relps.productId, wz.name productName, wzlb.Id productTypeId, wzlb.name productTypeName, relps.sfdlCode, relps.sfdlmc, relps.OrganizeId
,relps.sfxmCode, relps.sfxmmc, relps.zt, relps.CreatorCode, relps.CreateTime, relps.LastModifyTime, relps.LastModifierCode
FROM dbo.rel_productAndsfxm(NOLOCK) relps
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=relps.productId AND wz.OrganizeId=relps.OrganizeId AND wz.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wzlb ON wzlb.Id=wz.typeId AND wzlb.zt='1'
WHERE relps.OrganizeId=@OrganizeId ");
            var param = new List<SugarParameter>
            {
                new SugarParameter("@OrganizeId", organizeId)
            };
            if (!string.IsNullOrWhiteSpace(zt))
            {
                sql.AppendLine("AND relps.zt=@zt ");
                param.Add(new SugarParameter("@zt", zt));
            }
            if (!string.IsNullOrWhiteSpace(sfdlCode))
            {
                sql.AppendLine("AND relps.sfdlCode=@sfdlCode ");
                param.Add(new SugarParameter("@sfdlCode", sfdlCode));
            }
            if (!string.IsNullOrWhiteSpace(productTypeId))
            {
                sql.AppendLine("AND wzlb.Id=@wzlb ");
                param.Add(new SugarParameter("@wzlb", productTypeId));
            }
            return QueryWithPage<RelProductAndsfxmVo>(sql.ToString(), pagination, param.ToArray());
        }

        public RelProductAndsfxmVo SelectProductAndsfxm(string relId, string organizeId)
        {
            const string sql = @"
SELECT relps.Id, relps.productId, wz.name productName, wzlb.Id productTypeId, wzlb.name productTypeName, relps.sfdlCode, relps.sfdlmc, relps.OrganizeId
,relps.sfxmCode, relps.sfxmmc, relps.zt, relps.CreatorCode, relps.CreateTime, relps.LastModifyTime, relps.LastModifierCode
FROM dbo.rel_productAndsfxm(NOLOCK) relps
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=relps.productId AND wz.OrganizeId=relps.OrganizeId AND wz.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) wzlb ON wzlb.Id=wz.typeId AND wzlb.zt='1'
WHERE relps.OrganizeId=@OrganizeId
AND relps.Id=@id ";
            var param = new SugarParameter[]
            {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@id", relId)
            };
            return FirstOrDefault<RelProductAndsfxmVo>(sql, param);
        }
    }
}
