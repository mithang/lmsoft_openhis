using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Điều chỉnh giá vật tư
    /// </summary>
    public interface IWzPriceAdjustmentRepo : IRepositoryBase<WzPriceAdjustmentEntity>
    {
        /// <summary>
        /// Lấy danh sách chưa thực thi
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<WzPriceAdjustmentEntity> GetUnexecutedList(string productId, string organizeId);
    }
}