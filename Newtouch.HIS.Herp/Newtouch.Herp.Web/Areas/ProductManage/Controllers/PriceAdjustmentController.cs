using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.ProductManage.Controllers
{
    [Area("ProductManage")]
    public class PriceAdjustmentController : ControllerBase
    {
        private readonly IWzPriceAdjustmentRepo _wzPriceAdjustmentRepo;
        private readonly IWzPriceAdjustmentDmnService _wzPriceAdjustmentDmnService;
        private readonly IAdjustPriceApp adjustPriceApp;
        private readonly IWzProductRepo _wzProductRepo;

        public PriceAdjustmentController(IWzPriceAdjustmentRepo wzPriceAdjustmentRepo,
            IWzPriceAdjustmentDmnService wzPriceAdjustmentDmnService, IAdjustPriceApp adjustPriceApp,
            IWzProductRepo wzProductRepo)
        {
            _wzPriceAdjustmentRepo = wzPriceAdjustmentRepo;
            _wzPriceAdjustmentDmnService = wzPriceAdjustmentDmnService;
            this.adjustPriceApp = adjustPriceApp;
            _wzProductRepo = wzProductRepo;
        }

        public IActionResult PriceAdjustmentRequest()
        {
            return View();
        }

        public IActionResult PriceAdjustmentApproval()
        {
            return View();
        }

        public IActionResult PriceAdjustmentHistory()
        {
            return View();
        }

        public IActionResult PriceAdjustmentProfitAndLossQuery()
        {
            return View();
        }

        public IActionResult SubmitPriceAdjustmentRequest(SubmitPriceAdjustmentRequestDTO param)
        {
            // G14: guard truoc khi deref param / insert — base probe: body rong
            // -> loi SQL tho "Cannot insert NULL into 'productId'"; productId
            // bogus -> Success + dong mo coi; dwmc 11 ky tu / tzwj 31 ky tu ->
            // "String or binary data would be truncated"; zxsj rong -> luu
            // 1900-01-01 (dong chet, khong bao gio thuc hien duoc); lsj=0 /
            // lsj==ylsj -> Success (UI PriceAdjustmentRequest.cshtml CheckData
            // chan ca 2). Message dung message UI.
            if (param == null || string.IsNullOrWhiteSpace(param.productId)) return Error("Vui lòng chọn vật tư!");
            if (_wzProductRepo.FindEntity(p => p.Id == param.productId && p.OrganizeId == OrganizeId) == null)
                return Error("Không tìm thấy vật tư đã chỉ định");
            if (param.lsj <= 0) return Error("Giá bán lẻ không được để trống!");
            if (param.lsj == param.ylsj) return Error("Giá bán buôn không được giống giá gốc!");
            if (string.IsNullOrWhiteSpace(param.tzwj)) return Error("Văn bản thực hiện không được để trống!");
            if (!string.IsNullOrWhiteSpace(param.dwmc) && Encoding.Default.GetBytes(param.dwmc).Length > 10)
                return Error("Đơn vị không được vượt quá 10 ký tự");
            if (Encoding.Default.GetBytes(param.tzwj).Length > 30)
                return Error("Tên tệp không được vượt quá 30 ký tự");
            if (param.zxsj < Constants.MinDateTime) return Error("Thời gian thực hiện không được để trống!");
            if (_wzPriceAdjustmentRepo.GetUnexecutedList(param.productId, OrganizeId).Any())
            {
                return Error("Vật tư này đã có đơn xin điều chỉnh giá (chưa thực hiện), vui lòng thực hiện đơn đã gửi trước!");
            }

            var entity = new WzPriceAdjustmentEntity
            {
                lsj = param.lsj,
                OrganizeId = OrganizeId,
                productId = param.productId,
                zhyz = param.zhyz,
                dwmc = param.dwmc,
                shzt = ((int)EnumTjShzt.Waiting).ToString(),
                tzczy = OperatorProvider.GetCurrent().UserCode,
                shczy = null,
                tzsj = DateTime.Now,
                tzwj = param.tzwj,
                warehouseId = Constants.CurrentKf.currentKfId,
                xglx = "tj",
                zt = ((int)Enumzt.Enable).ToString(),
                zxbz = ((int)EnumTjZxbz.Not).ToString(),
                ylsj = param.ylsj,
                zxsj = param.zxsj
            };
            entity.Create(true);
            // EntityExtensions.Create only stamps an "Id" property; this
            // entity's key is wztjId, so generate it explicitly.
            if (string.IsNullOrWhiteSpace(entity.wztjId)) entity.wztjId = Guid.NewGuid().ToString();
            return _wzPriceAdjustmentRepo.Insert(entity) > 0 ? Success() : Error("Lưu thông tin điều chỉnh giá thất bại");
        }

        public IActionResult SelectAdjustPriceApprovalList(Pagination pagination, string inputCode, string shzt)
        {
            var list = new
            {
                rows = _wzPriceAdjustmentDmnService.GetPriceAdjustmentList(pagination, inputCode, shzt, OrganizeId, ((int)EnumTjZxbz.Not).ToString()),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult AdjustPriceApproval(string ids, string operationType)
        {
            // G14: base probe — ids rong/null -> NRE "Object reference not set
            // to an instance of an object" (ids.TrimEnd khong guard);
            // operationType "9" -> Success + ghi shzt=9 (trang thai khong hop
            // le); operationType "0" tren don da Audited -> Success + dao
            // shzt 1->0; ids bogus -> Success gia (service UPDATE 0 dong).
            // UI PriceAdjustmentApproval.cshtml chi gui 1 (Audited) / 2
            // (Refuse) / 3 (Revoke).
            if (string.IsNullOrWhiteSpace(ids)) return Error("Chưa lấy được dữ liệu cần thao tác");
            if (operationType != ((int)EnumTjShzt.Audited).ToString()
                && operationType != ((int)EnumTjShzt.Refuse).ToString()
                && operationType != ((int)EnumTjShzt.Revoke).ToString())
                return Error("Thao tác không hợp lệ");
            var idList = ids.TrimEnd(',').Split(',').ToList();
            var result = _wzPriceAdjustmentDmnService.Approval(idList, operationType, OperatorProvider.GetCurrent().UserCode, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        public IActionResult AdjustPriceExecute(string ids)
        {
            var result = adjustPriceApp.AdjustPriceExecute(ids);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        public IActionResult SelectAdjustPriceHistoryList(Pagination pagination, string inputCode, DateTime startTime, DateTime endTime)
        {
            var list = new
            {
                rows = _wzPriceAdjustmentDmnService.GetPriceAdjustmentHistoryList(pagination, inputCode, OrganizeId, startTime, endTime, Constants.CurrentKf.currentKfId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        public IActionResult GetPriceAdjustmentProfitLoss(Pagination pagination, PriceAdjustmentProfitLossDTO param)
        {
            param.kfList = new List<string>();
            param.organizeId = OrganizeId;
            if (Constants.CurrentKf.kfList != null && Constants.CurrentKf.kfList.Count > 0)
            {
                Constants.CurrentKf.kfList.ForEach(p =>
                {
                    param.kfList.Add("'" + (p.kfId ?? "").Replace("'", "''") + "'");
                });
            }
            var list = new
            {
                rows = _wzPriceAdjustmentDmnService.GetPriceAdjustmentProfitLoss(pagination, param),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        [HttpGet]
        public IActionResult GetCurrentUserKfList()
        {
            var list = Constants.CurrentKf.kfList;
            return Content(list.ToJson());
        }
    }
}
