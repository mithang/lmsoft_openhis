using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Base.Domain.Organize
{
    public interface ISysRoleDmnService : IScopedDependency
    {
        /// <summary>
        /// lấy danh sách vai trò người dùng đã ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId);
        /// <summary>
        /// lấy danh sách vai trò đã ủy quyền của người dùng hệ thống chỉ định
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId, string db);
        /// <summary>
        /// kiểm tra người dùng có phải là quản trị viên hay không
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<bool> IsHospAdmin(string userId, string orgId);
        /// <summary>
        /// xóa vai trò hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        Task DeleteForm(string keyValue);

        /// <summary>
        /// nộp mới、cập nhật thực thể vai trò、quyền vai trò
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        Task SubmitForm(SysRoleEntity roleEntity, List<Entity.SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue,string UserCode, string orgId);
        /// <summary>
        /// tổ chức danh sách vai trò mang theo phân trang
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetPagintionList(string orgId, string? keyword = null);

        /// <summary>
        /// lấy thực thể vai trò
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<SysRoleEntity> GetForm(string keyword);
        /// <summary>
        /// danh sách vai trò hệ thống
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetRoleList(string orgId);
    }
}
