using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.ValueObjects;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Kết chuyển tồn kho
    /// </summary>
    public interface IKcKcjzRepo : IRepositoryBase<KcKcjzEntity>
    {
        /// <summary>
        /// Lấy thời gian kết chuyển lịch sử
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SelectVo> GetlsjzDateTime(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy thông tin kết chuyển gần nhất
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcKcjzEntity GetLastJzData(string warehouseId, string organizeId);
    }
}