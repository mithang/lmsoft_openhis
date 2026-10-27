using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserRoleRepo : IRepositoryBase<SysUserRoleEntity>
    {
        /// <summary>
        /// LấyUserIdLiên kếtRoleId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<string> GetRoleIdListByUserId(string userId);
    }
}
