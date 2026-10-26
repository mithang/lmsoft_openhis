using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysItemsRepo : IRepositoryBase<SysItemsEntity>
    {
        /// <summary>
        /// lấy từ điển phần loại (bao gồm không hiệu lực của)
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetList(string keyword = null);

    }
}
