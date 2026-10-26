using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderRefundInfoRepo : IRepositoryBase<OrderRefundInfoEntity>
    {
        void SubmitInfo(OrderRefundInfoEntity entity, string keyValue);

        /// <summary>
        /// Lấy số tiền đã trả lại(Bao gồm trạng thái chưa biết)
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        decimal GetRefundedSumByOutTradeNo(string outTradeNo);
    }
}
