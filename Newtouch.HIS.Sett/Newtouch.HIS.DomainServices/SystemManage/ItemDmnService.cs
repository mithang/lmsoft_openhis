using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// từ điển, từ điển mục DmnService
    /// </summary>
    public class ItemDmnService : DmnServiceBase, IItemDmnService
    {

        public ItemDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy hiệu lực từ điển mục danh sách chỉ đ ịnh tổ chức
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrgAndItemCode(string topOrgId, string code)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail
where 1 = 1 and zt = '1'
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')
and ItemId in (
select Id from [NewtouchHIS_Base]..V_S_Sys_Items where Code = @code and zt = '1'
)";
            return this.FindList<SysItemsDetailVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@code", code),
                new SqlParameter("@topOrgId", topOrgId)
            });
        }

        /// <summary>
        /// Lấy tất cả các từ điển phân loại
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public IList<SysItemsVEntity> GetValidItemsList()
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_Items";
            return this.FindList<SysItemsVEntity>(sql);
        }

        /// <summary>
        /// Lấy tất cả các từ điển mục
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <returns></returns>
        public IList<SysItemsDetailVEntity> GetValidItemsDetailListByTopOrg(string topOrgId)
        {
            var sql = @"select * from [NewtouchHIS_Base]..V_S_Sys_ItemsDetail
where 1 = 1 and zt = '1'
and(TopOrganizeId = @topOrgId or TopOrganizeId = '*')";
            return this.FindList<SysItemsDetailVEntity>(sql, new SqlParameter[] {
                new SqlParameter("@topOrgId", topOrgId)
            });
        }

    }
}