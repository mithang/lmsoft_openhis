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
    /// 采购计划 (Core port)
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
        // purchasingPlan is the PROTECTED Chinese description ("采购计划"),
        // which is a receipt-number key, not display text — never translated.
        private static string Desc(System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }

        #region 采购计划填写

        /// <summary>
        /// 填写采购计划视图
        /// </summary>
        /// <returns></returns>
        public IActionResult FillPurchasingPlan()
        {
            return View();
        }

        /// <summary>
        /// 获取新的采购计划单号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetNewPurchasingPlanBillNo()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(Desc(EnumOutOrInStorageBillType.purchasingPlan));
            return Content(result);
        }

        /// <summary>
        /// 提交采购计划
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
        /// 检查是否是暂存单
        /// </summary>
        public IActionResult CheckIsTemporaryBill(string cgdh)
        {
            var entity = new CgPurchaseOrderEntity();
            var result = _purchasingPlanApp.CheckIsAuditRefuseOrTemporaryBill(cgdh, OrganizeId, ref entity);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 审核采购计划

        /// <summary>
        /// 采购计划审核
        /// </summary>
        /// <returns></returns>
        public IActionResult AuditPurchasingPlan()
        {
            return View();
        }

        /// <summary>
        /// 审核采购计划
        /// </summary>
        public IActionResult SubmitAuditPurchasingPlan(string cgdh, int auditState, string remarks)
        {
            var result = _purchasingPlanApp.SubmitAuditPurchasingPlan(cgdh, auditState, remarks, OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        #region 我的采购

        /// <summary>
        /// 采购计划查询
        /// </summary>
        /// <returns></returns>
        public IActionResult PurchasingPlanQuery()
        {
            return View();
        }

        /// <summary>
        /// 获取我的采购计划
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
        /// 获取审核通过的采购计划
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
        /// 获取待审核采购计划
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
        /// 获取我的采购计划明细
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
        /// 获取待采购的采购计划明细
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
        /// 获取审核通过的采购计划
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
        /// 作废采购计划
        /// </summary>
        public IActionResult CancelPurchasingPlan(string cgdh)
        {
            if (string.IsNullOrWhiteSpace(cgdh)) return Error("Số kế hoạch mua sắm không được để trống");
            var userCode = UserIdentity.UserCode;
            var result = _purchasingPlanApp.CancelPurchasingPlan(cgdh, OrganizeId, userCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// 将暂存采购计划提交审核
        /// </summary>
        public IActionResult SubmitPurchasingPlan(string cgdh)
        {
            var result = _purchasingPlanApp.SubmitPurchasingPlan(cgdh, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        #region 库存预警生成采购计划单
        public IActionResult PurchasingPlan()
        {
            return View();
        }

        #endregion
    }
}
