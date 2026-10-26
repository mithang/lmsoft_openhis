using System;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Kế hoạch mua hàng
    /// </summary>
    public interface ICgPurchaseOrderDmnService
    {
        /// <summary>
        /// Lấy thông tin bảng chính kế hoạch mua hàng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="auditState"></param>
        /// <param name="userCode">Tài khoản nhân viên thao tác</param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectPurchaseOrder(Pagination pagination, string cgdh, DateTime kssj, DateTime jssj, int auditState,
           string userCode, string organizeId);

        /// <summary>
        /// Tra cứu phiếu kế hoạch mua hàng đã duyệt đạt
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="deptCode"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectAdoptPurchasingPlanInfo(Pagination pagination, string cgdh,
           string deptCode, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// Lấy thông tin bảng chính kế hoạch mua hàng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="auditState"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectPurchaseOrder(Pagination pagination, string cgdh, DateTime kssj, DateTime jssj, int auditState, string organizeId);

        /// <summary>
        /// Lấy thông tin chi tiết kế hoạch mua hàng
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgPurchaseOrderDetailEntity> SelectPurchaseOrderDetail(string cgdh, string organizeId);

        /// <summary>
        /// Tra cứu phiếu kế hoạch mua hàng đã duyệt đạt
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="cgdh"></param>
        /// <param name="deptCode"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<VCgPurchaseOrderEntity> SelectAdoptWaitingPurchasePlanInfo(Pagination pagination, string cgdh, string deptCode, DateTime kssj, DateTime jssj, string organizeId);

        /// <summary>
        /// Lấy chi tiết kế hoạch mua hàng đang chờ mua
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgPurchaseOrderDetailEntity> SelectWaitingPurchasePlanDetail(string cgdh, string organizeId);
    }
}