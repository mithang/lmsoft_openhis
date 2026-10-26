using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysModuleRepo : IRepositoryBase<SysModuleEntity>
    {
        /// <summary>
        /// lấy còn hiệu lực danh sách
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetValidList();
    }
}
