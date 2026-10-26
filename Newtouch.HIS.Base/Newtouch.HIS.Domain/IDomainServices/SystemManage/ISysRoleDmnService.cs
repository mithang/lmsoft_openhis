using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// DmnService vai trò
    /// </summary>
    public interface ISysRoleDmnService
    {
        /// <summary>
        /// Xóa vai trò hệ thống
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// Gửi tạo mới, cập nhật thực thể vai trò, quyền vai trò
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, List<SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);

    }
}
