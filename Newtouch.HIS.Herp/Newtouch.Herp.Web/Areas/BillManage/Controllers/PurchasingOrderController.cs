using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Đơn mua sắm (Core port)
    /// </summary>
    [Area("BillManage")]
    public class PurchasingOrderController : ControllerBase
    {
        private readonly IPurchasingOrderApp _purchasingOrderApp;
        private readonly ICgOrderDmnService _cgOrderDmnService;

        public PurchasingOrderController(IPurchasingOrderApp purchasingOrderApp,
            ICgOrderDmnService cgOrderDmnService)
        {
            _purchasingOrderApp = purchasingOrderApp;
            _cgOrderDmnService = cgOrderDmnService;
        }

        #region Tạo đơn mua sắm

        /// <summary>
        /// Tạo đơn mua sắm Chế độ xem
        /// </summary>
        /// <returns></returns>
        public IActionResult GeneratingPurchaseOrder()
        {
            return View();
        }

        /// <summary>
        /// Gửi tạo đơn mua sắm
        /// </summary>
        /// <param name="cgmx">Chi tiết mua sắm</param>
        /// <returns></returns>
        public IActionResult SubmitGeneratingPurchaseOrder(List<VCgPurchaseOrderDetailEntity> cgmx, string remark)
        {
            var result = _purchasingOrderApp.SubmitGeneratingPurchaseOrder(cgmx, UserIdentity.UserCode, OrganizeId, remark);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Truy vấn đơn mua sắm

        /// <summary>
        /// Truy vấn đơn mua sắm Chế độ xem
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchaseOrderQuery()
        {
            return View();
        }

        /// <summary>
        /// Truy vấn thông tin đơn mua sắm
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchasingOrderInfoQuery(Pagination pagination, string orderNo, int orderType, int orderProcess, DateTime kssj, DateTime jssj)
        {
            var result = _cgOrderDmnService.SelectCgOrder(pagination, orderNo, orderType, orderProcess, OrganizeId, kssj, jssj);
            var data = new
            {
                rows = result,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Lấy đơn mua sắm kèm chi tiết （Chi tiết đến phiếu kế hoạch mua sắm）
        /// </summary>
        public IActionResult CgOrderDetailGroupByCgdhQuery(string orderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailGroupByCgdh(orderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy đơn mua sắm kèm chi tiết （Không phân biệt phiếu kế hoạch mua sắm）
        /// </summary>
        public IActionResult CgOrderDetailNoCgdhQuery(string orderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailNoCgdh(orderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Truy vấn thông tin đơn mua sắm nháp chờ duyệt
        /// </summary>
        public IActionResult TempPurchasingOrderInfoQuery(Pagination pagination, string orderNo, DateTime kssj, DateTime jssj)
        {
            var result = _cgOrderDmnService.SelectCgOrder(pagination, orderNo, (int)EnumOrderTypeHrp.TempOrder, (int)EnumOrderProcess.Waiting, OrganizeId, kssj, jssj);
            var data = new
            {
                rows = result,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        #endregion

        #region Duyệt đơn mua sắm

        /// <summary>
        /// Duyệt đơn mua sắm Chế độ xem
        /// </summary>
        /// <returns></returns>
        public IActionResult AuditPurchaseOrder()
        {
            return View();
        }

        /// <summary>
        /// Duyệt đơn mua sắm
        /// </summary>
        public IActionResult SubmitAuditPurchasingOrder(string orderNo, int orderType, string remarks)
        {
            var result = _purchasingOrderApp.SubmitAuditPurchasingOrder(orderNo, orderType, remarks, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Nhập kho ngoài tham chiếu đơn mua sắm
        public IActionResult PurchaseView()
        {
            return View();
        }
        public IActionResult PurchaseDetailView()
        {
            return View();
        }

        /// <summary>
        /// Nhập kho ngoài-Tham chiếu đơn mua sắmfromChế độ xem
        /// </summary>
        public IActionResult SelectCgOrderView(DateTime kssj, DateTime jssj, string fph, string orderNo, string organizeId)
        {
            var result = _cgOrderDmnService.SelectCgOrderView(kssj, jssj, fph, orderNo, OrganizeId);
            return Content(result.ToJson());
        }
        public IActionResult SelectCgOrderDetail(string subOrderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetail(subOrderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Chi tiết đơn mua sắm nhập vào danh sách nhập kho
        /// </summary>
        public IActionResult SelectCgOrderDetailImport(string subOrderNo, string organizeId)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailImport(subOrderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Bổ sung số hóa đơn đơn mua sắm
        /// </summary>
        public IActionResult UpdateFph(string subOrderNo, string fph)
        {
            _cgOrderDmnService.PurchaseDdbhUpdate(subOrderNo, fph, this.UserIdentity.UserCode);
            return Success();
        }
        #endregion
    }
}
