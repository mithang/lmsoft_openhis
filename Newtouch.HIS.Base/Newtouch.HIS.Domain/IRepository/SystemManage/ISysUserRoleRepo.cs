using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserRoleRepo : IRepositoryBase<SysUserRoleEntity>
    {
        /// <summary>
        /// Lấy RoleId liên kết với UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetRoleIdListByUserId(string userId);
    }
}
