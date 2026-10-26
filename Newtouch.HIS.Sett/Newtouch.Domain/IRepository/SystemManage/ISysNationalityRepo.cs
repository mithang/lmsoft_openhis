using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysNationalityRepo : IRepositoryBase<SysNationalityVEntity>
    {
        /// <summary>
        /// Lấy quốc tịch hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        IList<SysNationalityVEntity> GetgjList();
    }
}
