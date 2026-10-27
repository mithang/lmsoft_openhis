using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Domain.IRepository
{
    /// <summary>
    /// Đơn mua sắm
    /// </summary>
    public interface ICgOrderRepo : IRepositoryBase<CgOrderEntity>
    {

        /// <summary>
        /// Lấy thông tin đơn mua sắm
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        CgOrderEntity SelectData(string orderNo, string organizeId);

        /// <summary>
        /// Duyệt đơn mua sắm
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        int AuditPurchasingOrder(string orderNo, int orderType, string userCode,string organizeId, string remark);

    }
}