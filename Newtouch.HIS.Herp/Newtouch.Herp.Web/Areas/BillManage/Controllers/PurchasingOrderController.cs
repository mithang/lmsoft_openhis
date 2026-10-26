using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Đơn mua hàng
    /// </summary>
    public class PurchasingOrderController : ControllerBase
    {
        private readonly IPurchasingOrderApp _purchasingOrderApp;
        private readonly ICgOrderDmnService _cgOrderDmnService;

        #region Tạo đơn mua hàng

        /// <summary>
        /// Tạo đơn mua hàng Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult GeneratingPurchaseOrder()
        {
            return View();
        }

        /// <summary>
        /// Gửi tạo đơn mua hàng
        /// </summary>
        /// <param name="cgmx">Chi tiết mua hàng</param>
        /// <returns></returns>
        public ActionResult SubmitGeneratingPurchaseOrder(List<VCgPurchaseOrderDetailEntity> cgmx, string remark)
        {
            var result = _purchasingOrderApp.SubmitGeneratingPurchaseOrder(cgmx, UserIdentity.UserCode, OrganizeId, remark);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Tra cứu đơn mua hàng

        /// <summary>
        /// Tra cứu đơn mua hàng Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchaseOrderQuery()
        {
            return View();
        }

        /// <summary>
        /// Tra cứu thông tin đơn mua hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult PurchasingOrderInfoQuery(Pagination pagination, string orderNo, int orderType, int orderProcess, DateTime kssj, DateTime jssj)
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
        /// Lấy đơn mua hàng kèm chi tiết (Chi tiết đến phiếu kế hoạch mua hàng)
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult CgOrderDetailGroupByCgdhQuery(string orderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailGroupByCgdh(orderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy đơn mua hàng kèm chi tiết (Không phân biệt phiếu kế hoạch mua hàng)
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult CgOrderDetailNoCgdhQuery(string orderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailNoCgdh(orderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Tra cứu thông tin đơn mua hàng lưu tạm chờ duyệt
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orderNo"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult TempPurchasingOrderInfoQuery(Pagination pagination, string orderNo, DateTime kssj, DateTime jssj)
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

        #region Duyệt đơn mua hàng

        /// <summary>
        /// Duyệt đơn mua hàng Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult AuditPurchaseOrder()
        {
            return View();
        }
        /// <summary>
        /// Duyệt đơn mua hàng
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orderType"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>
        public ActionResult SubmitAuditPurchasingOrder(string orderNo, int orderType, string remarks)
        {
            var result = _purchasingOrderApp.SubmitAuditPurchasingOrder(orderNo, orderType, remarks, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Nhập kho ngoài tham chiếu đơn mua hàng
        public ActionResult PurchaseView()
        {
            return View();
        }
        public ActionResult PurchaseDetailView()
        {
            return View();
        }
        /// <summary>
        /// Nhập kho ngoài-Tham chiếu đơn mua hàng from Khung nhìn
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="fph"></param>
        /// <param name="orderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult SelectCgOrderView(DateTime kssj, DateTime jssj, string fph, string orderNo, string organizeId)
        {
            var result = _cgOrderDmnService.SelectCgOrderView(kssj, jssj, fph, orderNo, OrganizeId);
            return Content(result.ToJson());
        }
        public ActionResult SelectCgOrderDetail(string subOrderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetail(subOrderNo, OrganizeId);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Nhập chi tiết đơn mua hàng vào danh sách nhập kho
        /// </summary>
        /// <param name="subOrderNo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public ActionResult SelectCgOrderDetailImport(string subOrderNo, string organizeId)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailImport(subOrderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Điền bổ sung Số hóa đơn đơn mua hàng
        /// </summary>
        /// <param name="crkmxId"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult UpdateFph(string subOrderNo, string fph)
        {
            _cgOrderDmnService.PurchaseDdbhUpdate(subOrderNo, fph,this.UserIdentity.UserCode);
            return Success();
        }
        #endregion
    }
}