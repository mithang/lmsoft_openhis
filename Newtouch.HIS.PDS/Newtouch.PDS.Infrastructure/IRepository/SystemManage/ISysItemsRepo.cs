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
        /// Lấy phân loại từ điển(Gồm cả không hiệu lực)
        /// </summary>
        /// <returns></returns>
        IList<SysItemsEntity> GetList(string keyword = null);

    }
}
