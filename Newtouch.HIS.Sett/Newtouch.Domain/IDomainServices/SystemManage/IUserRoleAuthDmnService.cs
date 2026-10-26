using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Common.Model;
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IDomainServices
{
    /// <summary>
    /// Quyền vai trò người dùng Dùng chungDmnService
    /// </summary>
    public interface IUserRoleAuthDmnService
    {
        /// <summary>
        /// Lấy danh sách vai trò người dùng đã ủy quyền
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="orgId">Cơ sở y tếId</param>
        /// <returns></returns>
        IList<SysRoleEntity> GetUserRoleList(string userId, string orgId);

        /// <summary>
        /// Lấy vai trò Liên kết Người dùng(org)
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        IList<FirstSecond> GetCurUserIdListByRoleId(string roleId);


    }
}
