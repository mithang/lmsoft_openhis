using System;
using System.Collections.Generic;
using System.Linq;
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

        public PriceAdjustmentController(IWzPriceAdjustmentRepo wzPriceAdjustmentRepo,
            IWzPriceAdjustmentDmnService wzPriceAdjustmentDmnService, IAdjustPriceApp adjustPriceApp)
        {
            _wzPriceAdjustmentRepo = wzPriceAdjustmentRepo;
            _wzPriceAdjustmentDmnService = wzPriceAdjustmentDmnService;
            this.adjustPriceApp = adjustPriceApp;
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
