using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Quyền vai trò người dùng Dùng chungDmnService
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// Lấy danh sách vai trò người dùng đã được ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId, string orgId);

        /// <summary>
        /// Lấy vai trò Liên kết Người dùng（org）
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<FirstSecond> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// Lưu vai trò Danh sách người dùng
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
