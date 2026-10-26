using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Menu nhanh
    /// </summary>
    public interface ISysRoleShortcutMenuDmnService
    {
        /// <summary>
        /// Lấy menu nhanh mà vai trò được mở List
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <returns></returns>
        IList<SysShortcutMenuEntity> GetAuthedSCMList(IList<string> roleIdList, string orgId);

        /// <summary>
        /// Lấy menu nhanh Danh sách vai trò đã ủy quyền
        /// </summary>
        /// <param name="scmId"></param>
        /// <returns></returns>
        IList<string> GetAuthedRoleIdList(string scmId, string orgId);

        /// <summary>
        /// Menu nhanh Ủy quyền Cho chỉ định Vai trò
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="roleList"></param>
        void UpdateAuthRoleList(string keyValue, string roleList, string orgId);

    }
}
