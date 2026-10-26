using Newtouch.HIS.Domain.Entity;
using System.Collections;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysUserRepo: IRepositoryBase<SysUserEntity>
    {
        /// <summary>
        /// Trả về một thực thể
        /// </summary>
        /// <param name="topOrgId"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        SysUserEntity GetEntity(string topOrgId, string account);
    }
}
