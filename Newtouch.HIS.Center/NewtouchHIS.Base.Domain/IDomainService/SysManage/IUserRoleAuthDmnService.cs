using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base;

namespace NewtouchHIS.Base.Domain.IDomainService
{
    /// <summary>
    /// Menu hệ thống (Giao diện thêm/xóa/sửa)
    /// </summary>
    public interface IUserRoleAuthDmnService : IScopedDependency
    {
        /// <summary>
        /// Lấy vai trò Liên kết Người dùng(org)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<FirstSecond>> GetCurUserIdListByRoleId(string roleId);
        /// <summary>
        /// Lưu Vai trò Danh sách người dùng
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        void submitRoleUser(string roleId, List<FirstSecond> userList, string usercode);
        /// <summary>
        /// Cập nhật người dùng Quan hệ gắn vai trò
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="roleIdList"></param>
        /// <param name="usercode"></param>
        /// <returns></returns>
        Task<bool> UpdateUserRole(string userId, string orgId, List<string> roleIdList, string usercode);
    }
}
