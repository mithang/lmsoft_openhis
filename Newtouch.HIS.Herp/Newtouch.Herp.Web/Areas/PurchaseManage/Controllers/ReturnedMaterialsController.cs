using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.IRepository.Purchase;

namespace Newtouch.Herp.Web.Areas.PurchaseManage.Controllers
{
    /// <summary>
    /// Trả hàng mua sắm (port .NET 6 của ReturnedMaterialsController gốc).
    /// Các trang ReIndex/ReForm thuộc PurchaseController; controller này chỉ
    /// có API JSON/submit, giống bản gốc.
    /// </summary>
    [Area("PurchaseManage")]
    public class ReturnedMaterialsController : ControllerBase
    {
        private readonly IReturnedRepo _returnedRepo;
        private readonly IReturnedDetailRepo _returnedDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;

        public ReturnedMaterialsController(IReturnedRepo returnedRepo, IReturnedDetailRepo returnedDetailRepo,
            IPurchaseDmnService purchaseDmnService, ISunPurchaseDmnService sunPurchaseDmnService)
        {
            _returnedRepo = returnedRepo;
            _returnedDetailRepo = returnedDetailRepo;
            _purchaseDmnService = purchaseDmnService;
            _sunPurchaseDmnService = sunPurchaseDmnService;
        }

        public IActionResult SubmitPurchase(ReturnedEntity thEntity, List<ReturnedDetailEntity> thmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;
            // G12: guard phòng thủ — thEntity null (nếu binder trả null) sẽ NRE
            // ở `thEntity.OrganizeId`; probe live cho thấy binder hiện tạo
            // instance rỗng cả với form trống lẫn JSON {} nên nhánh này chưa
            // chạm được qua HTTP (ghi rõ trong PROBE-G12, không claim PASS).
            // thmxList rỗng -> tạo header JLS=0 (phiếu mồ côi, UI ReForm đã chặn
            // bằng "Vui lòng chọn dữ liệu cần chỉnh sửa!" nên server chặn giống UI).
            if (thEntity == null) return Error("Thông tin trả hàng không được để trống");
            thmxList = thmxList ?? new List<ReturnedDetailEntity>();
            if (thmxList.Count == 0) return Error("Vui lòng chọn dữ liệu cần chỉnh sửa!");
            thEntity.OrganizeId = this.OrganizeId;
            thEntity.JLS = thmxList.Count;
            var thId = _returnedRepo.SubmitForm(thEntity, keyValue);
            if (keyValue != null)
            {
                _returnedDetailRepo.DeleteItem(thId, this.OrganizeId);
            }
            foreach (var cgmx in thmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _returnedDetailRepo.InsertItem(cgmx, thId);
            }
            return Success();
        }

        public IActionResult SubmitAndUploadPurchase(ReturnedEntity thEntity, List<ReturnedDetailEntity> thmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;
            // G12: cùng guard như SubmitPurchase (phòng thủ null thEntity +
            // list rỗng -> header mồ côi); phần YY giữ nguyên, không đụng.
            if (thEntity == null) return Error("Thông tin trả hàng không được để trống");
            thmxList = thmxList ?? new List<ReturnedDetailEntity>();
            if (thmxList.Count == 0) return Error("Vui lòng chọn dữ liệu cần chỉnh sửa!");
            thEntity.OrganizeId = this.OrganizeId;
            thEntity.JLS = thmxList.Count;
            var thId = _returnedRepo.SubmitForm(thEntity, keyValue);
            if (keyValue != null)
            {
                _returnedDetailRepo.DeleteItem(thId, this.OrganizeId);
            }
            foreach (var cgmx in thmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _returnedDetailRepo.InsertItem(cgmx, thId);
            }
            return PurchaseUpload(thId, 2);
        }

        public IActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj)
        {
            var tt = _returnedRepo.GetReturnedGridJson(pagination, kssj, jssj, OrganizeId);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }

        public IActionResult QueryPurchaseReturnbyId(string thId)
        {
            var result = _purchaseDmnService.QueryPurchaseReturnbyId(thId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult QueryPurchaseReturnDetailbyId(string thId)
        {
            var result = _purchaseDmnService.QueryPurchaseReturnDetailbyId(thId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult PurchaseReturnDelete(string thId)
        {
            _returnedRepo.PurchaseReturnDelete(thId, this.OrganizeId);
            return Success();
        }

        public IActionResult PurchaseStateUpdate(string thId, int thdzt)
        {
            _returnedRepo.PurchaseReturnStateUpdate(thId, thdzt, this.OrganizeId);
            return Success();
        }

        /// <summary>Đơn trả hàng truyền báo</summary>
        public IActionResult PurchaseUpload(string thId, int ddzt)
        {
            var ddbh = _sunPurchaseDmnService.Purchase_YY113(this.OrganizeId, thId);
            if (ddbh == null) return Error("Dịch vụ mua sắm tập trung hiện không khả dụng, vui lòng thử lại sau.");
            _returnedRepo.PurchaseDdbhUpdate(thId, ddbh, this.OrganizeId);
            var confirm = _sunPurchaseDmnService.Purchase_YY114(this.OrganizeId, thId);
            if (!string.IsNullOrEmpty(confirm)) return Error("Xác nhận truyền báo đơn trả hàng thất bại: " + confirm);
            _returnedRepo.PurchaseReturnStateUpdate(thId, ddzt, this.OrganizeId);
            return Success();
        }
    }
}
