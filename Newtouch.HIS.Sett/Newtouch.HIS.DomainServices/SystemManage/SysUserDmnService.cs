using Newtouch.HIS.Domain.IDomainServices;
using System.Data.SqlClient;
using System.Collections.Generic;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// ngư ời dùng liên quan DmnService
    /// </summary>
    public class SysUserExDmnService : DmnServiceBase, ISysUserExDmnService
    {
        /// <summary>
        /// xây dựng hàm 
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysUserExDmnService(IDefaultDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// theo Userid lấy hệ thống ngư ời dùng có thể thao tác của nhà thuốc bộ phậnCode List
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<string> GetYfbmCodeListByUserId(string userId, string orgId)
        {
            var sql = "select distinct yfbmCode from [NewtouchHIS_Base]..V_S_Sys_UserYfbm where UserId = @userId and zt = '1' and OrganizeId = @orgId";
            return this.FindList<string>(sql, new[] { new SqlParameter("@userId", userId), new SqlParameter("@orgId", orgId) });
        }

    }
}
