using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// người dùng vai trò quyền dùng chung DmnService
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// lấy người dùng đã ủy quyền của vai trò danh sách
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId);

        /// <summary>
        /// lấy UserId theo RoleId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// lưu vai trò người dùng danh sách
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, string userIds, string parentOrgId);


    }
}