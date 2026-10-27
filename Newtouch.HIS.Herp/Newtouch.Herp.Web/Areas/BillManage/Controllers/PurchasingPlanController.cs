using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure.Common;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Kế hoạch mua sắm (Core port)
    /// </summary>
    [Area("BillManage")]
    public class PurchasingPlanController : ControllerBase
    {
        private readonly IPurchasingPlanApp _purchasingPlanApp;
        private readonly ICgPurchaseOrderDmnService _cgPurchaseOrderDmnService;

        public PurchasingPlanController(IPurchasingPlanApp purchasingPlanApp,
            ICgPurchaseOrderDmnService cgPurchaseOrderDmnService)
        {
            _purchasingPlanApp = purchasingPlanApp;
            _cgPurchaseOrderDmnService = cgPurchaseOrderDmnService;
        }

        // Same reflection helper as OutOrInStorageBillController: the copy has
        // no GetDescription() extension. The value returned here for
        // purchasingPlan is the PROTECTED enum description (kept in the original language),
        // which is a receipt-number key, not display text — never translated.
        private static string Desc(System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }

        #region Nhập kế hoạch mua sắm

        /// <summary>
        /// Chế độ xem nhập kế hoạch mua sắm
        /// </summary>
        /// <returns></returns>
        public IActionResult FillPurchasingPlan()
        {
            return View();
        }

        /// <summary>
        /// Lấy số phiếu kế hoạch mua sắm mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNewPurchasingPlanBillNo()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(Desc(EnumOutOrInStorageBillType.purchasingPlan));
            return Content(result);
        }

        /// <summary>
        /// Gửi kế hoạch mua sắm
        /// </summary>
        public IActionResult SubmitFillPurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx)
        {
            var userCode = UserIdentity.UserCode;
            pp.rkbmCode = pp.rkbmCode ?? this.UserIdentity.DepartmentCode;
            pp.cgdh = pp.cgdh ?? ReceiptNoManage.GetNewReceiptNo(Desc(EnumOutOrInStorageBillType.purchasingPlan));
            pp.zt = ((int)Enumzt.Enable).ToString();
            var result = _purchasingPlanApp.SubmitFillPurchasingPlan(pp, ppmx, userCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Kiểm tra có phải phiếu nháp không
        /// </summary>
        public IActionResult CheckIsTemporaryBill(string cgdh)
        {
            var entity = new CgPurchaseOrderEntity();
            var result = _purchasingPlanApp.CheckIsAuditRefuseOrTemporaryBill(cgdh, OrganizeId, ref entity);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Duyệt kế hoạch mua sắm

        /// <summary>
        /// Duyệt kế hoạch mua sắm
        /// </summary>
        /// <returns></returns>
        public IActionResult AuditPurchasingPlan()
        {
            return View();
        }

        /// <summary>
        /// Duyệt kế hoạch mua sắm
        /// </summary>
        public IActionResult SubmitAuditPurchasingPlan(string cgdh, int auditState, string remarks)
        {
            var result = _purchasingPlanApp.SubmitAuditPurchasingPlan(cgdh, auditState, remarks, OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        #region Mua sắm của tôi

        /// <summary>
        /// Truy vấn kế hoạch mua sắm
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchasingPlanQuery()
        {
            return View();
        }

        /// <summary>
        /// Lấy kế hoạch mua sắm của tôi
        /// </summary>
        public IActionResult GetPurchasingPlanInfo(Pagination pagination, string cgdh, DateTime kssj, DateTime jssj, int auditState)
        {
            var userCode = UserIdentity.UserCode;
            var result = _cgPurchaseOrderDmnService.SelectPurchaseOrder(pagination, cgdh, kssj, jssj, auditState, userCode, OrganizeId);
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
        /// Lấy kế hoạch mua sắm đã duyệt
        /// </summary>
        public IActionResult GetAdoptPurchasingPlanInfo(Pagination pagination, string cgdh, string deptCode, DateTime kssj, DateTime jssj)
        {
            var result = _cgPurchaseOrderDmnService.SelectAdoptPurchasingPlanInfo(pagination, cgdh, deptCode, kssj, jssj, OrganizeId);
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
        /// Lấy kế hoạch mua sắm chờ duyệt
        /// </summary>
        public IActionResult GetAuditPurchasingPlanInfo(Pagination pagination, string cgdh, DateTime kssj, DateTime jssj, int auditState)
        {
            var userCode = UserIdentity.UserCode;
            var result = _cgPurchaseOrderDmnService.SelectPurchaseOrder(pagination, cgdh, kssj, jssj, auditState, userCode, OrganizeId);
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
        /// Lấy chi tiết kế hoạch mua sắm của tôi
        /// </summary>
        public IActionResult GetPurchasingPlanDetail(string cgdh)
        {
            if (string.IsNullOrWhiteSpace(cgdh)) return Content(new List<VCgPurchaseOrderDetailEntity>().ToJson());
            var cgdhArr = cgdh.Split(',');
            if (cgdhArr.Length == 0) return Content(new List<VCgPurchaseOrderDetailEntity>().ToJson());
            var result = new List<VCgPurchaseOrderDetailEntity>();
            foreach (var t in cgdhArr)
            {
                if (string.IsNullOrWhiteSpace(t)) continue;
                result.AddRange(_cgPurchaseOrderDmnService.SelectPurchaseOrderDetail(t, OrganizeId));
            }
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy chi tiết kế hoạch mua sắm chờ mua
        /// </summary>
        public IActionResult GetWaitingPurchasePlanDetail(string cgdh)
        {
            if (string.IsNullOrWhiteSpace(cgdh)) return Content(new List<VCgPurchaseOrderDetailEntity>().ToJson());
            var cgdhArr = cgdh.Split(',');
            if (cgdhArr.Length == 0) return Content(new List<VCgPurchaseOrderDetailEntity>().ToJson());
            var result = new List<VCgPurchaseOrderDetailEntity>();
            foreach (var t in cgdhArr)
            {
                if (string.IsNullOrWhiteSpace(t)) continue;
                result.AddRange(_cgPurchaseOrderDmnService.SelectWaitingPurchasePlanDetail(t, OrganizeId));
            }
            return Content(result.ToJson());
        }

        /// <summary>
        /// Lấy kế hoạch mua sắm đã duyệt
        /// </summary>
        public IActionResult GetAdoptWaitingPurchasePlanInfo(Pagination pagination, string cgdh, string deptCode, DateTime kssj, DateTime jssj)
        {
            var result = _cgPurchaseOrderDmnService.SelectAdoptWaitingPurchasePlanInfo(pagination, cgdh, deptCode, kssj, jssj, OrganizeId);
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
        /// Hủy kế hoạch mua sắm
        /// </summary>
        public IActionResult CancelPurchasingPlan(string cgdh)
        {
            if (string.IsNullOrWhiteSpace(cgdh)) return Error("Số kế hoạch mua sắm không được để trống");
            var userCode = UserIdentity.UserCode;
            var result = _purchasingPlanApp.CancelPurchasingPlan(cgdh, OrganizeId, userCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Gửi kế hoạch mua sắm nháp đi duyệt
        /// </summary>
        public IActionResult SubmitPurchasingPlan(string cgdh)
        {
            var result = _purchasingPlanApp.SubmitPurchasingPlan(cgdh, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        #region Cảnh báo tồn kho tạo phiếu kế hoạch mua sắm
        public IActionResult PurchasingPlan()
        {
            return View();
        }

        #endregion
    }
}
