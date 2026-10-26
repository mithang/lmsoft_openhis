using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// vai trò DmnService
    /// </summary>
    public interface ISysRoleDmnService
    {
        /// <summary>
        /// xóa hệ thống vai trò
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// gửi tạo mới, cập nhật thực thể vai trò, vai trò quyền
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="roleAuthorizeEntitys"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, List<SysRoleAuthorizeEntity> roleAuthorizeEntitys, string keyValue);

    }
}
