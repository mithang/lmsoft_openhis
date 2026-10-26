using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Exceptions;
using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// ngư ời dùng vai trò quyền hạn 公 dùng DmnService
    /// </summary>
    public class UserRoleAuthDmnService : DmnServiceBase, IUserRoleAuthDmnService
    {
        public UserRoleAuthDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// Lấy ngư ời dùng Đ ã ủy quyền củavai trò danh sách(không phải root không phải Administrator)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgid"> y tế cơ quanid</param>
        /// <returns></returns>
        public IList<SysRoleEntity> GetUserRoleList(string userId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }

            var sql = @"select distinct c.* from Sys_UserRole a
left join [NewtouchHIS_Base].[dbo].V_C_Sys_User b
on a.UserId = b.Id
left join Sys_Role c
on a.RoleId = c.Id
where a.UserId = @userId and a.zt = '1' and c.zt = '1'
and a.OrganizeId = @orgId";

            return this.FindList<SysRoleEntity>(sql, new SqlParameter[] {
                    new SqlParameter("@userId", userId)
                    ,new SqlParameter("@orgId", orgId)
                });
        }

        /// <summary>
        /// Lấyvai trò liên kết ngư ời dùng (org)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList<FirstSecond> GetCurUserIdListByRoleId(string roleId)
        {
            var sql = "select distinct UserId First, OrganizeId Second from [dbo].[Sys_UserRole] where zt = '1' and RoleId = @roleId";
            return this.FindList<FirstSecond>(sql, new SqlParameter[] {
                new SqlParameter("@roleId",roleId)
            });
        }

    }
}
