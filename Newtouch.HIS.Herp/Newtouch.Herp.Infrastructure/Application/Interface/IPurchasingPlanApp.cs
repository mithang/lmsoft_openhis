using System.Collections.Generic;
using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// Kế hoạch mua sắm
    /// </summary>
    public interface IPurchasingPlanApp
    {
        /// <summary>
        /// Gửi kế hoạch mua sắm
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="ppmx"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitFillPurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx, string userCode, string organizeId);

        /// <summary>
        /// Kiểm tra có phải phiếu lưu tạm không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CheckIsTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity);

        /// <summary>
        /// Kiểm tra có phải không duyệt không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CheckIsAuditRefuseBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity);

        /// <summary>
        /// Kiểm tra có phải phiếu kế hoạch mua sắm không duyệt hoặc lưu tạm không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        string CheckIsAuditRefuseOrTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity);

        /// <summary>
        /// Kiểm tra có phải phiếu lưu tạm không
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelPurchasingPlan(string cgdh, string organizeId, string userCode);

        /// <summary>
        /// Gửi kế hoạch mua sắm lưu tạm đi duyệt
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitPurchasingPlan(string cgdh, string organizeId);

        /// <summary>
        /// Duyệt kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="auditState"></param>
        /// <param name="remarks">Lời phê</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string SubmitAuditPurchasingPlan(string cgdh, int auditState, string remarks, string organizeId, string userCode);
    }
}