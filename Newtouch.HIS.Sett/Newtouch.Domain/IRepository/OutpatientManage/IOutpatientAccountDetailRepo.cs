using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOutpatientAccountDetailRepo : IRepositoryBase<OutpatientAccountDetailEntity>
    {
        /// <summary>
        /// Theo chi tiết kế hoạch ghi sổIdLấylist
        /// </summary>
        /// <returns></returns>
        IList<OutpatientAccountDetailEntity> GetListbyjzjhids(string orgId, IList<string> mxIdList);
    }
}
