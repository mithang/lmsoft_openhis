using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using SqlSugar;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Supplier operations. Port of the Framework GysSupplierRepo (EF6 +
    /// FrameworkBase RepositoryBase) to SqlSugar via the copy's RepositoryBase.
    /// This is the canonical implementation shared by Purchase/Storage areas
    /// (they only use the IQueryable base members plus the GetList overloads).
    /// NOTE: the copy's RepositoryBase has no QueryWithPage helper (only
    /// DmnServiceBase does), so the paged GetList implements count + page
    /// inline with the same semantics (sidx/sord override when a plain
    /// identifier, otherwise no ORDER BY beyond OFFSET/FETCH).
    /// </summary>
    public class GysSupplierRepo : RepositoryBase<GysSupplierEntity>, IGysSupplierRepo
    {
        public GysSupplierRepo()
        {
        }

        /// <summary>
        /// get GysSupplierEntity list (paged)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<GysSupplierEntity> GetList(Pagination pagination, string organizeId, string keyWord, string zt = "1")
        {
            const string sql = @"
            SELECT sup.[Id],sup.[OrganizeId],sup.[name],sup.[py],sup.[address],sup.[zipCode],sup.[tel],sup.[fax],sup.[supplierType]
            ,sup.[khh],sup.[khhzh],sup.[sh],sup.[jszq],sup.[zt],sup.[CreatorCode],sup.[CreateTime],sup.[LastModifyTime],sup.[LastModifierCode]
            FROM dbo.gys_supplier(NOLOCK) sup
            WHERE sup.OrganizeId=@OrganizeId
            AND (sup.name LIKE '%'+@keyWord+'%' OR sup.py LIKE '%'+@keyWord+'%')
            AND sup.zt=@zt ";
            var param = new SugarParameter[]
            {
                new SugarParameter("@keyWord", keyWord ?? ""),
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@zt", zt)
            };
            if (pagination.rows == null || pagination.rows <= 0) pagination.rows = 15;
            if (pagination.page == null || pagination.page <= 0) pagination.page = 1;
            int pageSize = pagination.rows.Value;
            int pageIndex = pagination.page.Value;
            int total = Db.Ado.SqlQuery<int>(
                "SELECT COUNT(1) FROM (" + sql + ") AS _t", param).FirstOrDefault();
            pagination.records = total;
            string orderBy = " ORDER BY (SELECT NULL)";
            if (!string.IsNullOrWhiteSpace(pagination.sidx)
                && Regex.IsMatch(pagination.sidx, @"^[A-Za-z0-9_\.]+$"))
            {
                string sord = string.Equals(pagination.sord, "desc", System.StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
                orderBy = " ORDER BY " + pagination.sidx + " " + sord;
            }
            string pageSql = sql + orderBy
                + " OFFSET " + ((pageIndex - 1) * pageSize) + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY";
            return Db.SqlQueryable<GysSupplierEntity>(pageSql).AddParameters(param).ToList();
        }

        /// <summary>
        /// get GysSupplierEntity list
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="keyWord"></param>
        /// <param name="supplierType">0：Khác     1：Nhà sản xuất      2：Nhà phân phối</param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public IList<GysSupplierEntity> GetList(string organizeId, string keyWord, int supplierType, string zt = "1")
        {
            const string sql = @"
SELECT sup.[Id],sup.[OrganizeId],sup.[name],sup.[py],sup.[address],sup.[zipCode],sup.[tel],sup.[fax],sup.[supplierType]
,sup.[khh],sup.[khhzh],sup.[sh],sup.[jszq],sup.[zt],sup.[CreatorCode],sup.[CreateTime],sup.[LastModifyTime],sup.[LastModifierCode]
FROM dbo.gys_supplier(NOLOCK) sup
WHERE sup.OrganizeId=@OrganizeId
AND (sup.name LIKE '%'+@keyWord+'%' OR sup.py LIKE '%'+@keyWord+'%')
AND sup.zt=@zt
AND sup.supplierType=@supplierType
";
            var param = new SugarParameter[]
            {
                new SugarParameter("@keyWord", keyWord ?? ""),
                new SugarParameter("@OrganizeId", organizeId ?? ""),
                new SugarParameter("@supplierType", supplierType),
                new SugarParameter("@zt", zt)
            };
            return FindList<GysSupplierEntity>(sql, param);
        }
    }
}
