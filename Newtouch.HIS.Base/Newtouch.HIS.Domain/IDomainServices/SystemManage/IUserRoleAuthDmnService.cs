using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// DmnService dùng chung quyền vai trò người dùng
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// Lấy danh sách vai trò đã được ủy quyền cho người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId, string orgId);

        /// <summary>
        /// Lấy người dùng (org) liên kết với vai trò
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<FirstSecond> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// Lưu danh sách người dùng vai trò
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, List<FirstSecond> userList);

        /// <summary>
        /// Cập nhật vai trò người dùng
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        void UpdateUserRole(string userId, string orgId, string[] roleIdList);
    }
}
