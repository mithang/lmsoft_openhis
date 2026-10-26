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
        /// Lấy danh sách có hiệu lực
        /// </summary>
        /// <returns></returns>
        IList<SysModuleEntity> GetValidList();
    }
}
