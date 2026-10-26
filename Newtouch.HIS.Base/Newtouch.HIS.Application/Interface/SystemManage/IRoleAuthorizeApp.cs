using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// App quyền vai trò
    /// </summary>
    public interface IRoleAuthorizeApp
    {
        /// <summary>
        /// Lấy danh sách ủy quyền vai trò
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysRoleAuthorizeEntity> GetValidList(string roleId);

        /// <summary>
        /// Xác thực quyền
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleId, string moduleId, string action);
    }
}
