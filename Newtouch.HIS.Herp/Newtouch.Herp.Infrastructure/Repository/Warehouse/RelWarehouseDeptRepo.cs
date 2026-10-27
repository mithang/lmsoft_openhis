using System.Collections.Generic;
using SqlSugar;
using System.Linq;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// Khoa kho
    /// </summary>
    public class RelWarehouseDeptRepo : RepositoryBase<RelWarehouseDeptEntity>, IRelWarehouseDeptRepo
    {
        public RelWarehouseDeptRepo()
        {
        }

        /// <summary>
        /// get RelWarehouseDeptEntity list
        /// </summary>
        /// <param name="id"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public List<RelWarehouseDeptEntity> GetListById(string id, string organizeId, string zt = "1")
        {
            return IQueryable(p => p.Id == id && p.OrganizeId == organizeId && p.zt == zt).ToList();
        }

        /// <summary>
        /// get RelWarehouseDeptEntity list
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public List<RelWarehouseDeptEntity> GetListByWarehouseId(string warehouseId, string organizeId, string zt = "1")
        {
            return IQueryable(p => p.warehouseId == warehouseId && p.OrganizeId == organizeId && p.zt == zt).ToList();
        }

        /// <summary>
        /// get RelWarehouseDeptEntity list
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<RelWarehouseDeptEntity> GetList(string warehouseId, string organizeId, string keyword)
        {
            const string sql = @"
SELECT [Id]
      ,[OrganizeId]
      ,[warehouseId]
      ,[deptId]
      ,[deptName]
      ,[zt]
      ,[CreatorCode]
      ,[CreateTime]
      ,[LastModifyTime]
      ,[LastModifierCode]
FROM [NewtouchHIS_herp].[dbo].[rel_warehouseDept](NOLOCK) rwd
WHERE rwd.OrganizeId=@OrganizeId
AND rwd.warehouseId=@warehouseId
AND (rwd.deptName LIKE '%'+@keyword+'%' OR rwd.deptId LIKE '%'+@keyword+'%')
AND rwd.zt='1'
";
            var param = new SugarParameter[] {
                new SugarParameter("@OrganizeId", organizeId),
                new SugarParameter("@warehouseId", warehouseId),
                new SugarParameter("@keyword", keyword),
            };
            return FindList<RelWarehouseDeptEntity>(sql, param);
        }
    }
}
