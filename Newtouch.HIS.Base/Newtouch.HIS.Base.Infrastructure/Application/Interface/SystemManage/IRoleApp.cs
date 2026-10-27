using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// vai tròApp
    /// </summary>
    public interface IRoleApp
    {
        /// <summary>
        /// Tổ chức Danh sách vai trò kèm Phân trang
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        List<SysRoleEntity> GetPagintionList(string orgId, Pagination pagination, string keyword = null);

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        SysRoleEntity GetForm(string keyValue);

        /// <summary>
        /// Xóa thực thể
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);

        /// <summary>
        /// Gửi lưu
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue);
    }
}
