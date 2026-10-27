using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Đơn hàng mua sắm
    /// </summary>
    public interface IPurchasingOrderApp
    {
        /// <summary>
        /// Gửi đơn hàng mua sắm
        /// </summary>
        /// <param name="cgmx"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="remark"></param>
        string SubmitGeneratingPurchaseOrder(List<VCgPurchaseOrderDetailEntity> cgmx, string userCode, string organizeId, string remark);

        /// <summary>
        /// Duyệt đơn mua sắm
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="auditState"></param>
        /// <param name="remarks"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitAuditPurchasingOrder(string orderNo, int auditState, string remarks, string userCode,
           string organizeId);
    }
}