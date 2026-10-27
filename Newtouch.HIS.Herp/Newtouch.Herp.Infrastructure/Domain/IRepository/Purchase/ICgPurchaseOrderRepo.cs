using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Kế hoạch mua sắm
    /// </summary>
    public interface ICgPurchaseOrderRepo : IRepositoryBase<CgPurchaseOrderEntity>
    {
        /// <summary>
        /// Hủy kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        int CancelPurchasingPlan(string cgdh, string organizeId, string userCode);

        /// <summary>
        /// Truy vấn kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CgPurchaseOrderEntity SelectData(string cgdh, string organizeId);
    }
}