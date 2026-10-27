using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Quyền vai trò người dùng Dùng chungDmnService
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// Lấy danh sách vai trò người dùng đã ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId);

        /// <summary>
        /// LấyUserId TheoRoleId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<string> GetCurUserIdListByRoleId(string roleId);

        /// <summary>
        /// Lưu Vai trò Danh sách người dùng
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, string userIds, string parentOrgId);


    }
}