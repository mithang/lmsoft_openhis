using Newtouch.Herp.Domain.Entity;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Kế hoạch mua hàng
    /// </summary>
    public interface ICgPurchaseOrderRepo : IRepositoryBase<CgPurchaseOrderEntity>
    {
        /// <summary>
        /// Hủy kế hoạch mua hàng
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int CancelPurchasingPlan(string cgdh, string organizeId, string userCode);

        /// <summary>
        /// Tra cứu kế hoạch mua hàng
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CgPurchaseOrderEntity SelectData(string cgdh, string organizeId);
    }
}