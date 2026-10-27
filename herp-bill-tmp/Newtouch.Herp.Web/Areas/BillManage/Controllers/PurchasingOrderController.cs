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
    /// 采购单 (Core port)
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

        #region 生成采购单

        /// <summary>
        /// 生成采购单 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult GeneratingPurchaseOrder()
        {
            return View();
        }

        /// <summary>
        /// 提交生成采购单
        /// </summary>
        /// <param name="cgmx">采购明细</param>
        /// <returns></returns>
        public IActionResult SubmitGeneratingPurchaseOrder(List<VCgPurchaseOrderDetailEntity> cgmx, string remark)
        {
            var result = _purchasingOrderApp.SubmitGeneratingPurchaseOrder(cgmx, UserIdentity.UserCode, OrganizeId, remark);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 查询采购单

        /// <summary>
        /// 查询采购单 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchaseOrderQuery()
        {
            return View();
        }

        /// <summary>
        /// 查询采购单信息
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
        /// 获取采购带明细 （细化到采购计划单）
        /// </summary>
        public IActionResult CgOrderDetailGroupByCgdhQuery(string orderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailGroupByCgdh(orderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 获取采购带明细 （不区分采购计划单）
        /// </summary>
        public IActionResult CgOrderDetailNoCgdhQuery(string orderNo)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailNoCgdh(orderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 查询待审核的暂存采购单信息
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

        #region 审核采购单

        /// <summary>
        /// 审核采购单 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult AuditPurchaseOrder()
        {
            return View();
        }

        /// <summary>
        /// 审核采购单
        /// </summary>
        public IActionResult SubmitAuditPurchasingOrder(string orderNo, int orderType, string remarks)
        {
            var result = _purchasingOrderApp.SubmitAuditPurchasingOrder(orderNo, orderType, remarks, UserIdentity.UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 外部入库引用采购单
        public IActionResult PurchaseView()
        {
            return View();
        }
        public IActionResult PurchaseDetailView()
        {
            return View();
        }

        /// <summary>
        /// 外部入库-采购单引用from视图
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
        /// 采购单明细导入入库列表
        /// </summary>
        public IActionResult SelectCgOrderDetailImport(string subOrderNo, string organizeId)
        {
            var result = _cgOrderDmnService.SelectCgOrderDetailImport(subOrderNo, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 补填采购单发票号
        /// </summary>
        public IActionResult UpdateFph(string subOrderNo, string fph)
        {
            _cgOrderDmnService.PurchaseDdbhUpdate(subOrderNo, fph, this.UserIdentity.UserCode);
            return Success();
        }
        #endregion
    }
}
