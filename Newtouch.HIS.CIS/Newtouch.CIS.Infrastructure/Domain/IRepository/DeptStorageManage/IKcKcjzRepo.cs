using System.Collections.Generic;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository.DeptStorageManage
{
    /// <summary>
    /// Tồn kho kết chuyển
    /// </summary>
    public interface IKcKcjzRepo : IRepositoryBase<KcKcjzEntity>
    {
        /// <summary>
        /// Lấy lịch sử kết chuyển Thời gian
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SelectVo> GetlsjzDateTime(string warehouseId, string organizeId);

        /// <summary>
        /// Lấy nhất gần một lần kết chuyển thông tin
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcKcjzEntity GetLastJzData(string warehouseId, string organizeId);
    }
}
