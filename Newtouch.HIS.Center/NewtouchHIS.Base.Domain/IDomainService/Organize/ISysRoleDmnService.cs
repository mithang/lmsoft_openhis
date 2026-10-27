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
        /// Lấy danh sách vai trò người dùng đã được ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId);
        /// <summary>
        /// Lấy danh sách vai trò của người dùng hệ thống chỉ định đã được ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetUserRoleList(string userId, string orgId, string db);
        /// <summary>
        /// Kiểm tra người dùng có phải quản trị viên hay không
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<bool> IsHospAdmin(string userId, string orgId);
        /// <summary>
        /// Xóa vai trò hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        Task DeleteForm(string keyValue);

        /// <summary>
        /// Nộp mới,Cập nhật Thực thể Vai trò,Quyền vai trò
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        Task SubmitForm(SysRoleEntity roleEntity, List<Entity.SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue,string UserCode, string orgId);
        /// <summary>
        /// Tổ chức Danh sách vai trò kèm Phân trang
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetPagintionList(string orgId, string? keyword = null);

        /// <summary>
        /// Lấy thực thể vai trò
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<SysRoleEntity> GetForm(string keyword);
        /// <summary>
        /// Danh sách vai trò hệ thống
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        Task<List<SysRoleEntity>> GetRoleList(string orgId);
    }
}
