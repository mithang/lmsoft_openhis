using System.Collections.Generic;
using Newtouch.Domain.Entity.DeptStorageManage;
using Newtouch.Domain.ValueObjects.Storage;
using Newtouch.CIS.Repository;

namespace Newtouch.Domain.IRepository.DeptStorageManage
{
    /// <summary>
    /// Tồn khokết chuyển
    /// </summary>
    public interface IKcKcjzRepo : IRepositoryBase<KcKcjzEntity>
    {
        /// <summary>
        /// Lấylịch sửkết chuyểnThời gian
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<SelectVo> GetlsjzDateTime(string warehouseId, string organizeId);

        /// <summary>
        /// Lấynhấtgầnmột lầnkết chuyểnthông tin
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        KcKcjzEntity GetLastJzData(string warehouseId, string organizeId);
    }
}
