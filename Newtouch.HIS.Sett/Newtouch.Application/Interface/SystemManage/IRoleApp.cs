using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// Vai trò App
    /// </summary>
    public interface IRoleApp
    {
        /// <summary>
        /// Tổ chức vai trò danh sách 带 分trang
        /// </summary>
        /// <param name="topOrganizeId"></param>
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
        /// GửiLưu
        /// </summary>
        /// <param name="roleEntity"></param>
        /// <param name="permissionIds"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysRoleEntity roleEntity, string[] permissionIds, string keyValue);


    }
}
