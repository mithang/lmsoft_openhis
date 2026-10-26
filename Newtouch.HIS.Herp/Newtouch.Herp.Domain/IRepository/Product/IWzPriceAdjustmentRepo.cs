using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Điều chỉnh giá vật tư
    /// </summary>
    public interface IWzPriceAdjustmentRepo : IRepositoryBase<WzPriceAdjustmentEntity>
    {
        /// <summary>
        /// Lấy danh sách chưa thực hiện
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<WzPriceAdjustmentEntity> GetUnexecutedList(string productId, string organizeId);
    }
}