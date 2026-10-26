using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Herp.Application.Implementation.DeliveryAutoProcss;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Common;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.StorageManage.Controllers
{
    /// <summary>
    /// Quản lý tồn kho
    /// </summary>
    public class StorageController : ControllerBase
    {
        private readonly IStorageManageDmnService _storageDmnService;
        private readonly IGysSupplierRepo _gysSupplierRepo;
        private readonly IWzCrkfsRepo _CrkFs;
        private readonly IStorageApp _storageApp;
        private readonly IKfCrkdjDmnService _kfCrkdjDmnService;
        private readonly IKfWarehouseRepo _kfWarehouseRepo;
        private readonly IKfKcxxRepo _kfKcxxRepo;
        private readonly IRelWarehouseDeptRepo iRelWarehouseDeptRepo;
        private readonly IRelProductWarehouseRepo _relProductWarehouseRepo;
        private readonly IApplyBillApp _applyBillApp;

        #region Lấy vật tư


        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống
        /// </summary>
        /// <param name="key">Tên vật tư/Phiên âm</param>
        /// <param name="gysId"></param>
        /// <param name="deliveryNo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DepartmentStockListQuery(string key, string gysId, string deliveryNo)
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

        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống
        /// </summary>
        /// <param name="key">Tên vật tư/Phiên âm</param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StockQueryByWarehouse(string key, string warehouseId)
        {
            var result = _storageDmnService.GetProductStorage(warehouseId, OrganizeId, key);
            return Content(result.ToJson());
        }
        /// <summary>
        /// Đánh giá quyền vật tư
        /// </summary>
        /// <param name="targetDepartment"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductJurisdiction(string targetDepartment, string productId)
        {
            var entity = _relProductWarehouseRepo.FindEntity(p => p.productId == productId && p.warehouseId == targetDepartment && p.OrganizeId == OrganizeId && p.zt == ((int)Enumzt.Enable).ToString());
            return entity != null && !string.IsNullOrWhiteSpace(entity.Id) ? Success() : Error("目标库房没有权限使用该物资，请确保物资已同步");
        }

        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="key">Tên vật tư/Phiên âm</param>
        /// <returns></returns>
        public ActionResult DepartmentStockListQueryByPage(Pagination pagination, string key)
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

        /// <summary>
        /// Thông tin vật tư trong danh sách thả xuống  Xuất kho ngoài
        /// </summary>
        /// <param name="key">Tên vật tư/Phiên âm</param>
        /// <param name="gysId"></param>
        /// <param name="deliveryNo">Số phiếu giao hàng</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryStockListByGys(string key, string deliveryNo, string gysId)
        {
            var result = _storageDmnService.DepartmentStockListQuery(key, gysId, deliveryNo, Constants.CurrentKf.currentKfId, OrganizeId);
            return Content(result.ToJson());
        }

        #endregion

        #region Tra cứu lượng tồn kho

        /// <summary>
        /// Khung nhìn tra cứu lượng tồn kho
        /// </summary>
        /// <returns></returns>
        public ActionResult WarehouseStorageQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy tồn kho vật tư
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord">Tên vật tư/Phiên âm</param>
        /// <param name="lb">Loại ID</param>
        /// <param name="kzbz">Cờ điều khiển</param>
        /// <param name="wzzt">Trạng thái vật tư</param>
        /// <param name="xslkc">Hiển thị tồn kho bằng 0  1:Hiển thị  0:Không hiển thị</param>
        /// <param name="ygq">Đã hết hạn</param>
        /// <param name="mxyx">Chi tiết tạm thời còn hiệu lực  true:Có  false:Không</param>
        /// <returns></returns>
        public ActionResult GetWarehouseStorage(Pagination pagination,
            string keyWord,
            string lb,
            string kzbz,
            string wzzt,
            string xslkc,
            string ygq,
            string mxyx,string kccg=null)
        {
            var list = new
            {
                rows = _storageDmnService.GetProductStorage(pagination, Constants.CurrentKf.currentKfId, OrganizeId, (keyWord ?? "").Trim(), lb, kzbz, wzzt, xslkc, ygq, mxyx,kccg),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Theo vật tư ID Lấy tồn kho từng lô
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="zt">Chi tiết tạm thời còn hiệu lực  true:Có  false:Không</param>
        /// <returns></returns>
        public ActionResult GetWarehouseStorageDetail(string proId, string zt)
        {
            return Content(_storageDmnService.GetProductStorageDetail(Constants.CurrentKf.currentKfId, OrganizeId, proId, zt).ToJson());
        }

        /// <summary>
        /// Sửa trạng thái tồn kho
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="pc"></param>
        /// <param name="zt"></param>
        /// <returns></returns>
        public ActionResult UpdateKcxxZt(string proId, string ph, string pc, string zt)
        {
            return _kfKcxxRepo.UpdateZt(proId, ph, pc, zt) > 0 ? Success() : Error("修改查库存状态失败");
        }

        /// <summary>
        /// Thông tin Số lô/lô vật tư trong danh sách thả xuống
        /// </summary>
        /// <param name="proId">Vật tư ID</param>
        /// <param name="gysId">Nhà cung cấp ID</param>
        /// <param name="deliveryNo">Số phiếu giao hàng</param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductBatchQuery(string proId, string gysId = "", string deliveryNo = "", string keyword = "")
        {
            return Content(_storageDmnService.ProductBatchQuery(proId, Constants.CurrentKf.currentKfId, OrganizeId, gysId, deliveryNo, keyword: keyword).ToJson());
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSupplierList(string key)
        {
            var list = _gysSupplierRepo.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && (p.py.Contains(key) || p.name.Contains(key) || string.IsNullOrEmpty(key)) && (p.supplierType == null || p.supplierType == 2)).ToList();
            return Content(list.ToJson());
        }
        #endregion

        #region Tra cứu tồn kho hết hạn

        /// <summary>
        /// Tồn kho hết hạn Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult ExpiredStorageQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Lấy vật tư hết hạn
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetExpiredStorageDetail(Pagination pagination,
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

        #region Nhập kho ngoài

        /// <summary>
        /// Nhập kho ngoài Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult InStorage()
        {
            return View();
        }

        /// <summary>
        /// Nhập kho ngoài Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult InStorageInlineEdit()
        {
            return View();
        }

        /// <summary>
        /// Lấy Số phiếu nhập kho mới
        /// </summary>
        /// <returns></returns>
        public ActionResult GenerateRkdh()
        {
            var rkdh = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Wbrk.GetDescription());
            return Success(rkdh);
        }

        /// <summary>
        /// Lấy hình thức nhập/xuất kho
        /// </summary>
        /// <returns></returns>
        public ActionResult GetStorageIOMode(string crkbz)
        {
            var list = _CrkFs.IQueryable(p => p.zt == ((int)Enumzt.Enable).ToString() && p.crkbz == crkbz).ToList();
            return Content(list.ToJson());
        }

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <param name="crkdj">Chứng từ nhập/xuất kho</param>
        /// <param name="crkdjmx">Chi tiết chứng từ nhập/xuất kho</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveInStorageInfo(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            crkdj.auditState = ((int)EnumAuditState.Waiting).ToString();
            var result = _storageApp.InStorageSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Nhập kho
        /// </summary>
        /// <param name="crkdj">Chứng từ nhập/xuất kho</param>
        /// <param name="crkdjmx">Chi tiết chứng từ nhập/xuất kho</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TemporaryStorageInStorageInfo(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            crkdj.auditState = ((int)EnumAuditState.Temporary).ToString();
            var result = _storageApp.InStorageSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Xuất kho ngoài

        /// <summary>
        /// Xuất kho ngoài Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult OutStorage()
        {
            return View();
        }

        /// <summary>
        /// Xuất kho ngoài Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult OutStorageInlineEdit()
        {
            return View();
        }

        /// <summary>
        /// Lấy Số phiếu nhập kho mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNewCkdh()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Wbck.GetDescription());
            return Content(result);
        }

        /// <summary>
        /// Lấy thông tin hóa đơn
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFphAndGys(string keyWord)
        {
            return Content(_kfCrkdjDmnService.GetFphAndGysInfo(keyWord, Constants.CurrentKf.currentKfId, OrganizeId).ToJson());
        }

        /// <summary>
        /// Xuất kho ngoài
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        public ActionResult SaveOutStorageInfo(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = _storageApp.OutStorageSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Xuất kho trực tiếp 

        /// <summary>
        /// Xuất kho trực tiếp Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DirectDelivery()
        {
            return View();
        }

        /// <summary>
        /// Xuất kho trực tiếp Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DirectDeliveryInlineEdit()
        {
            return View();
        }

        /// <summary>
        /// Lấy Số phiếu xuất kho trực tiếp mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNewZjckdh()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Zjck.GetDescription());
            return Content(result);
        }

        /// <summary>
        /// Lấy khoa nhập kho(Nút anh em và nút con)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetBrothersOrChildren(string keyword)
        {
            return Content(_kfWarehouseRepo.GetBrothersOrChildren(Constants.CurrentKf.currentKfId, OrganizeId, keyword).ToJson());
        }

        /// <summary>
        /// Gửi xuất kho trực tiếp
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDirectDelivery(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = _storageApp.DirectDeliverySubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        #endregion

        #region Trả lại hàng giao nội bộ

        /// <summary>
        /// Trả lại hàng giao nội bộ Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryOfReturn()
        {
            return View();
        }

        /// <summary>
        /// Trả lại hàng giao nội bộ Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryOfReturnInlineEdit()
        {
            return View();
        }

        /// <summary>
        /// Lấy Số phiếu trả lại hàng giao nội bộ mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNewNbthdh()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.Nbth.GetDescription());
            return Content(result);
        }

        /// <summary>
        /// Gửi trả lại hàng giao nội bộ
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveDeliveryOfReturn(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = _storageApp.DeliveryOfReturnSubmit(crkdj, crkdjmx);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Lấy khoa phòng(Nút cha và nút anh em)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetParentOrBrothers(string keyword)
        {
            return Content(_kfWarehouseRepo.GetParentOrBrothers(Constants.CurrentKf.currentKfId, OrganizeId, keyword).ToJson());
        }
        #endregion

        #region  Xuất kho đến khoa

        /// <summary>
        /// Xuất kho đến khoa Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryToDepartment()
        {
            return View();
        }

        /// <summary>
        /// Xuất kho đến khoa Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryToDepartmentInlineEdit()
        {
            return View();
        }

        /// <summary>
        /// Lấy Số chứng từ xuất kho đến khoa mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNewCkzksdh()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.chukuzhikeshi.GetDescription());
            return Content(result);
        }

        /// <summary>
        /// Lấy khoa tương ứng kho
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDeptByKf(string keyword)
        {
            var list = iRelWarehouseDeptRepo.GetList(Constants.CurrentKf.currentKfId, OrganizeId, keyword ?? "");
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lưu xuất kho đến khoa
        /// </summary>
        /// <param name="crkdj"></param>
        /// <param name="crkdjmx"></param>
        /// <returns></returns>
        public ActionResult SaveDeliveryToDepartment(KfCrkdjEntity crkdj, KfCrkmxEntity[] crkdjmx)
        {
            var result = new DeliveryToDepartmentProcess(new DjInfDTO { crkdj = crkdj, crkdjmx = crkdjmx.ToList() }).Process();
            if (result.IsSucceed)//Đồng bộ vật tư khoa-CIS
                _storageDmnService.SynchWz(crkdj.Pdh,this.UserIdentity.UserCode,this.OrganizeId);
            return result.IsSucceed ? Success() : Error(result.ResultMsg);
        }
        #endregion

        #region Lĩnh vật tư khoa

        /// <summary>
        /// Khung nhìn
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentApply()
        {
            return View();
        }

        /// <summary>
        /// Lấy Số phiếu lĩnh khoa mới
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetNewKsSldh()
        {
            var result = ReceiptNoManage.GetNewReceiptNo(EnumOutOrInStorageBillType.kssl.GetDescription());
            return Content(result);
        }

        /// <summary>
        /// Gửi phiếu lĩnh khoa
        /// </summary>
        /// <param name="sld"></param>
        /// <param name="sldmx"></param>
        /// <returns></returns>
        public ActionResult SubmitDepartmentApply(KfApplyOrderEntity sld, List<KfApplyOrderDetailEntity> sldmx)
        {
            var result = _applyBillApp.SubmitDepartmentApply(sld, sldmx, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion

        #region Xuất kho lĩnh

        /// <summary>
        /// Xuất kho lĩnh
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyOutStock()
        {
            return View();
        }

        /// <summary>
        /// Gửi lĩnh hàng giao
        /// </summary>
        /// <param name="ckmx"></param>
        /// <returns></returns>
        public ActionResult SubmitApplyOutStock(List<VApplyBillDetailEntity> ckmx)
        {
            var result = _applyBillApp.SubmitApplyOutStock(ckmx, OrganizeId, UserIdentity.UserCode);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }
        #endregion
    }
}
