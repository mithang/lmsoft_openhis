using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// Quyền vai tròApp
    /// </summary>
    public interface IRoleAuthorizeApp
    {
        ///// <summary>
        ///// Lấy danh sách ủy quyền vai trò
        ///// </summary>
        ///// <param name="roleId"></param>
        ///// <returns></returns>
        //List<SysRoleAuthorizeEntity> GetValidList(string roleId);

        /// <summary>
        /// Kiểm tra quyền truy cập
        /// </summary>
        /// <param name="roleIdList"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleIdList, string action);

        /// <summary>
        /// Kiểm tra quyền
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleId"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        bool ActionValidate(IList<string> roleId, string moduleId, string action);


    }
}
