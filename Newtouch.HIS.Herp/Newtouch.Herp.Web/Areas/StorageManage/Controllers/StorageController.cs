using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// 库存管理 (quản lý tồn kho).
    /// Cross-area note (integration update): IWzCrkfsRepo (Product),
    /// IKfCrkdjDmnService and IApplyBillApp (Bill) and IGysSupplierRepo
    /// (Supplier) are all integrated and injected via the constructor.
    /// No lazy cross-area resolution remains in this controller.
    /// SaveDeliveryToDepartment is additionally blocked: its
    /// DeliveryToDepartmentProcess (ProcessorFun base) is not ported,
    /// so it returns an explicit stub error.
    /// WarehouseStorageQuery grid data comes from a normal JSON action, so it
    /// is ported fully; only its SSRS print button (tempCode 1269) is stubbed
    /// in the view.
    /// </summary>
    [Area("StorageManage")]
    public class StorageController : ControllerBase
    {
        private readonly IStorageManageDmnService _storageDmnService;
        private readonly IStorageApp _storageApp;
        private readonly IKfWarehouseRepo _kfWarehouseRepo;
        private readonly IKfKcxxRepo _kfKcxxRepo;
        private readonly IRelWarehouseDeptRepo _relWarehouseDeptRepo;
        private readonly IRelProductWarehouseRepo _relProductWarehouseRepo;
        private readonly IWzCrkfsRepo _wzCrkfsRepo;
        private readonly IKfCrkdjDmnService _kfCrkdjDmnService;
        private readonly IApplyBillApp _applyBillApp;
        private readonly IGysSupplierRepo _gysSupplierRepo;

        public StorageController(IStorageManageDmnService storageDmnService, IStorageApp storageApp,
            IKfWarehouseRepo kfWarehouseRepo, IKfKcxxRepo kfKcxxRepo,
            IRelWarehouseDeptRepo relWarehouseDeptRepo, IRelProductWarehouseRepo relProductWarehouseRepo,
            IWzCrkfsRepo wzCrkfsRepo, IKfCrkdjDmnService kfCrkdjDmnService, IApplyBillApp applyBillApp,
            IGysSupplierRepo gysSupplierRepo)
        {
            _storageDmnService = storageDmnService;
            _storageApp = storageApp;
            _kfWarehouseRepo = kfWarehouseRepo;
            _kfKcxxRepo = kfKcxxRepo;
            _relWarehouseDeptRepo = relWarehouseDeptRepo;
            _relProductWarehouseRepo = relProductWarehouseRepo;
            _wzCrkfsRepo = wzCrkfsRepo;
            _kfCrkdjDmnService = kfCrkdjDmnService;
            _applyBillApp = applyBillApp;
            _gysSupplierRepo = gysSupplierRepo;
        }

        #region 物资获取

        [HttpGet]
        public IActionResult DepartmentStockListQuery(string key, string gysId, string deliveryNo)
        {
            var param = new DepartmentStockListQueryParam
            {
                gysId = gysId,
                key = key,
                organizeId = OrganizeId,
                warehouseId = Constants.CurrentKf.currentKfId,
                deliveryNo = deliveryNo,
                zt = "1"
            };
            var result = _storageDmnService.DepartmentStockListQuery(param);
            return Content(result.ToJson());
        }

        [HttpGet]
        public IActionResult StockQueryByWarehouse(string key, string warehouseId)
        {
            var result = _storageDmnService.GetProductStorage(warehouseId, OrganizeId, key);
            return Content(result.ToJson());
        }

        [HttpGet]
        public IActionResult ProductJurisdiction(string targetDepartment, string productId)
        {
            var entity = _relProductWarehouseRepo.FindEntity(p => p.productId == productId && p.warehouseId == targetDepartment && p.OrganizeId == OrganizeId && p.zt == ((int)Enumzt.Enable).ToString());
            return entity != null && !string.IsNullOrWhiteSpace(entity.Id) ? Success() : Error("Kho đích không có quyền sử dụng vật tư này, vui lòng đảm bảo vật tư đã được đồng bộ");
        }

        public IActionResult DepartmentStockListQueryByPage(Pagination pagination, string key)
        {
            var list = new
            {
                rows = _storageDmnService.DepartmentStockListQuery(pagination, key, Constants.CurrentKf.currentKfId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        [HttpGet]
        public IActionResult QueryStockListByGys(string key, string deliveryNo, string gysId)
        {
            var result = _storageDmnService.DepartmentStockListQuery(key, gysId, deliveryNo, Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        #endregion

        #region 库存量查询

        public IActionResult WarehouseStorageQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public IActionResult GetWarehouseStorage(Pagination pagination,
            string keyWord,
            string lb,
            string kzbz,
            string wzzt,
            string xslkc,
            string ygq,
            string mxyx, string kccg = null)
        {
            var list = new
            {
                rows = _storageDmnService.GetProductStorage(pagination, Constants.CurrentKf.currentKfId, OrganizeId, (keyWord ?? "").Trim(), lb, kzbz, wzzt, xslkc, ygq, mxyx, kccg),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult GetWarehouseStorageDetail(string proId, string zt)
        {
            return Content(_storageDmnService.GetProductStorageDetail(Constants.CurrentKf.currentKfId, OrganizeId, proId, zt).ToJson());
        }

        public IActionResult UpdateKcxxZt(string proId, string ph, string pc, string zt)
        {
            return _kfKcxxRepo.UpdateZt(proId, ph, pc, zt) > 0 ? Success() : Error("Sửa trạng thái tồn kho thất bại");
        }

        [HttpGet]
        public IActionResult ProductBatchQuery(string proId, string gysId = "", string deliveryNo = "", string keyword = "")
        {
            return Content(_storageDmnService.ProductBatchQuery(proId, Constants.CurrentKf.currentKfId, OrganizeId, gysId, deliveryNo, keyword: keyword).ToJson());
        }

        public IActionResult GetSupplierList(string key)
        {
            var list = _gysSupplierRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && (p.py.Contains(key) || p.name.Contains(key) || string.IsNullOrEmpty(key)) && (p.supplierType == null || p.supplierType == 2)).ToList();
            return Content(list.ToJson());
        }
        #endregion

        #region 过期库存查询

        public IActionResult ExpiredStorageQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        [HttpGet]
        public IActionResult GetExpiredStorageDetail(Pagination pagination,
            string keyWord,
            string lb,
            string kzbz,
            string wzzt,
            string xslkc,
            string mxyx)
        {
            var result = _storageDmnService.SelectStorageDetail(pagination, Constants.CurrentKf.currentKfId, OrganizeId, keyWord, lb, kzbz, wzzt, xslkc, mxyx, true);
            return Content(result.ToJson());
        }

        #endregion

        #region 外部入库

        public IActionResult InStorage()
        {
            return View();
        }

        public IActionResult InStorageInlineEdit()
        {
            return View();
        }

        public IActionResult GenerateRkdh()
        {
            var rkdh = ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.Wbrk);
            return Success(rkdh);
        }

        public IActionResult GetStorageIOMode(string crkbz)
        {
            var list = _wzCrkfsRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && p.crkbz == crkbz).ToList();
            return Content(list.ToJson());
        }

        [HttpPost]
        public IActionResult SaveInStorageInfo(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            crkdj.auditState = ((int)EnumAuditState.Waiting).ToString();
            var result = _storageApp.InStorageSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        [HttpPost]
        public IActionResult TemporaryStorageInStorageInfo(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            crkdj.auditState = ((int)EnumAuditState.Temporary).ToString();
            var result = _storageApp.InStorageSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 外部出库

        public IActionResult OutStorage()
        {
            return View();
        }

        public IActionResult OutStorageInlineEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNewCkdh()
        {
            var result = ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.Wbck);
            return Content(result);
        }

        public IActionResult GetFphAndGys(string keyWord)
        {
            return Content(_kfCrkdjDmnService.GetFphAndGysInfo(keyWord, Constants.CurrentKf.currentKfId, OrganizeId).ToJson());
        }

        public IActionResult SaveOutStorageInfo(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = _storageApp.OutStorageSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 直接出库

        public IActionResult DirectDelivery()
        {
            return View();
        }

        public IActionResult DirectDeliveryInlineEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNewZjckdh()
        {
            var result = ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.Zjck);
            return Content(result);
        }

        [HttpGet]
        public IActionResult GetBrothersOrChildren(string keyword)
        {
            return Content(_kfWarehouseRepo.GetBrothersOrChildren(Constants.CurrentKf.currentKfId, OrganizeId, keyword).ToJson());
        }

        [HttpPost]
        public IActionResult SaveDirectDelivery(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = _storageApp.DirectDeliverySubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region 内部发货退回

        public IActionResult DeliveryOfReturn()
        {
            return View();
        }

        public IActionResult DeliveryOfReturnInlineEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNewNbthdh()
        {
            var result = ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.Nbth);
            return Content(result);
        }

        [HttpPost]
        public IActionResult SaveDeliveryOfReturn(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = _storageApp.DeliveryOfReturnSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        [HttpGet]
        public IActionResult GetParentOrBrothers(string keyword)
        {
            return Content(_kfWarehouseRepo.GetParentOrBrothers(Constants.CurrentKf.currentKfId, OrganizeId, keyword).ToJson());
        }
        #endregion

        #region 出库至科室

        public IActionResult DeliveryToDepartment()
        {
            return View();
        }

        public IActionResult DeliveryToDepartmentInlineEdit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNewCkzksdh()
        {
            var result = ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.chukuzhikeshi);
            return Content(result);
        }

        public IActionResult GetDeptByKf(string keyword)
        {
            var list = _relWarehouseDeptRepo.GetList(Constants.CurrentKf.currentKfId, OrganizeId, keyword ?? "");
            return Content(list.ToJson());
        }

        public IActionResult SaveDeliveryToDepartment(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            // Explicit stub: the original DeliveryToDepartmentProcess
            // (ProcessorFun base) is not ported, so this action cannot run
            // even though IKfCrkdjDmnService itself is now integrated.
            return Error("Chức năng xuất kho tới khoa chưa tích hợp: thiếu DeliveryToDepartmentProcess (lớp xử lý nền tảng ProcessorFun chưa được chuyển đổi).");
        }
        #endregion

        #region 科室申领

        public IActionResult DepartmentApply()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetNewKsSldh()
        {
            var result = ReceiptNoHelper.GetNewReceiptNo(EnumOutOrInStorageBillType.kssl);
            return Content(result);
        }

        public IActionResult SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx)
        {
            var result = _applyBillApp.SubmitDepartmentApply(sld, sldmx, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        #region 申领出库

        public IActionResult ApplyOutStock()
        {
            return View();
        }

        public IActionResult SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx)
        {
            var result = _applyBillApp.SubmitApplyOutStock(ckmx, OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion
    }

    /// <summary>
    /// Receipt-number helper replacing Infrastructure.Common.ReceiptNoManage
    /// (not present in the Core copy). The original built the prefix from the
    /// pinyin initials of the bill-type Description (which are PROTECTED and
    /// must not be translated); the initials for the seven types used by this
    /// area are mapped explicitly because GBK pinyin conversion is not
    /// available on this Linux runtime.
    /// </summary>
    public static class ReceiptNoHelper
    {
        private static readonly Dictionary<EnumOutOrInStorageBillType, string> PrefixByType =
            new Dictionary<EnumOutOrInStorageBillType, string>
            {
                { EnumOutOrInStorageBillType.Wbrk, "WBRK" },
                { EnumOutOrInStorageBillType.Wbck, "WBCK" },
                { EnumOutOrInStorageBillType.Zjck, "ZJCK" },
                { EnumOutOrInStorageBillType.Nbth, "NBTH" },
                { EnumOutOrInStorageBillType.Bsby, "BSBY" },
                { EnumOutOrInStorageBillType.chukuzhikeshi, "CKZKS" },
                { EnumOutOrInStorageBillType.kssl, "KSSL" },
            };

        public static string GetNewReceiptNo(EnumOutOrInStorageBillType type, string userId = "")
        {
            var prefix = PrefixByType.TryGetValue(type, out var p) ? p : type.ToString().ToUpperInvariant();
            return prefix + DateTime.Now.ToString("yyyyMMddHHmmss") + userId;
        }
    }
}
