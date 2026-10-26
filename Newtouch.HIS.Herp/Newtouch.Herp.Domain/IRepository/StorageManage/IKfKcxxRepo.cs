using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Thong tin ton kho
    /// </summary>
    public interface IKfKcxxRepo : IRepositoryBase<KfKcxxEntity>
    {

        /// <summary>
        /// Tra cuu thong tin ton kho
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<KfKcxxEntity> SelectData(string warehouseId, string productId);

        /// <summary>
        /// Sua trang thai ton kho
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        int UpdateZt(string productId, string ph, string pc, string zt);
    }
}