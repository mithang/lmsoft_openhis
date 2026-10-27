using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Thông tin tồn kho
    /// </summary>
    public interface IKfKcxxRepo : IRepositoryBase<KfKcxxEntity>
    {

        /// <summary>
        /// Truy vấn thông tin tồn kho
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<KfKcxxEntity> SelectData(string warehouseId, string productId);

        /// <summary>
        /// Sửa trạng thái tồn kho
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateZt(string productId, string ph, string pc, string zt);
    }
}