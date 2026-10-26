using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// kết chuyển
    /// </summary>
    public interface ISysMedicineStockCarryDownRepo : IRepositoryBase<SysMedicineStockCarryDownEntity>
    {
        /// <summary>
        /// lấy lịch sử thời gian kết chuyển
        /// </summary>
        /// <returns></returns>
        List<KeyValueVEntity> GetlsjzDateTime(string yfbmCode, string organizeId);

        /// <summary>
        /// Lấy thông tin kết chuyển gần nhất
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        SysMedicineStockCarryDownEntity GetLastJzData(string yfbmCode, string organizeId);
    }
}
