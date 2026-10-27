using System;
using System.Collections.Generic;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity.VEntity;

namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// Kế hoạch mua sắm
    /// </summary>
    public interface ICgPurchaseOrderDmnService
    {
        /// <summary>
        /// Lấy thông tin bảng chính kế hoạch mua sắm
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
        /// Truy vấn phiếu kế hoạch mua sắm đã duyệt
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
        /// Lấy thông tin bảng chính kế hoạch mua sắm
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
        /// Lấy thông tin chi tiết kế hoạch mua sắm
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgPurchaseOrderDetailEntity> SelectPurchaseOrderDetail(string cgdh, string organizeId);

        /// <summary>
        /// Truy vấn phiếu kế hoạch mua sắm đã duyệt
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
        /// Lấy chi tiết kế hoạch mua sắm đang chờ mua
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        List<VCgPurchaseOrderDetailEntity> SelectWaitingPurchasePlanDetail(string cgdh, string organizeId);
    }
}