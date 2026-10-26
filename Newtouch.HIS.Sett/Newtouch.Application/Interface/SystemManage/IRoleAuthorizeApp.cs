using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// Vai trò quyền hạn App
    /// </summary>
    public interface IRoleAuthorizeApp
    {
        /// <summary>
        /// Lấyvai trò ủy quyền danh sách
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SysRoleAuthorizeEntity> GetValidList(string roleId);

        /// <summary>
        /// quyền hạn xác minh
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleId, string moduleId, string action);

    }
}
