using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Repository;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysFailedCodeMessageMappRepo : IRepositoryBase<SysFailedCodeMessageMappEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<SysFailedCodeMessageMappEntity> GetList(string orgId = null);
    }
}
