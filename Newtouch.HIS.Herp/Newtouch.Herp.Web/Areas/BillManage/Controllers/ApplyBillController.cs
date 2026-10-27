using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Quản lý phiếu lĩnh (Core port)
    /// </summary>
    [Area("BillManage")]
    public class ApplyBillController : ControllerBase
    {
        private readonly IKfApplyOrderDmnService _applyOrderDmnService;

        public ApplyBillController(IKfApplyOrderDmnService applyOrderDmnService)
        {
            _applyOrderDmnService = applyOrderDmnService;
        }

        /// <summary>
        /// Truy vấn phiếu lĩnh
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Truy vấn thông tin chính phiếu lĩnh
        /// </summary>
        public IActionResult ApplyBillInfoQuery(Pagination pagination, DateTime kssj, DateTime jssj, string pdh, int applyType, int applyProcess)
        {
            var result = _applyOrderDmnService.SelectApplyBillInfo(pagination, pdh, applyType, applyProcess, kssj, jssj, OrganizeId);
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
        /// Truy vấn thông tin chính phiếu lĩnh
        /// </summary>
        public IActionResult ApplyBillInfoQueryV2(Pagination pagination, DateTime kssj, DateTime jssj, string pdh, int applyType, int applyProcess, string slbm, string applyProcesses)
        {
            // No FailedException in the Core Lib; a plain exception surfaces
            // through the same error middleware.
            if (string.IsNullOrWhiteSpace(applyProcesses)) throw new Exception("Trạng thái lĩnh khả chọn không được để trống");
            var applyProcessList = new List<int>();
            foreach (var ap in applyProcesses.Split(','))
            {
                int apInt;
                int.TryParse(ap, out apInt);
                if (apInt > 0) applyProcessList.Add(apInt);
            }
            var result = _applyOrderDmnService.SelectApplyBillInfo(pagination, pdh, applyType, applyProcess, kssj, jssj, OrganizeId, Constants.CurrentKf.currentKfId, applyProcessList);
            if (result != null && !string.IsNullOrWhiteSpace(slbm)) result = result.ToList().FindAll(p => p.rkbmId == slbm);
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
        /// Truy vấn chi tiết phiếu lĩnh
        /// </summary>
        [HttpGet]
        public IActionResult ApplyBillDetailBySldhQuery(string sldId)
        {
            var result = _applyOrderDmnService.SelectApplyBillDetail(sldId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Truy vấn chi tiết phiếu lĩnh
        /// </summary>
        [HttpGet]
        public IActionResult ApplyBillDetailQuery(string sldIds)
        {
            if (string.IsNullOrWhiteSpace(sldIds)) return Content(new List<VApplyBillDetailEntity>().ToJson());
            var result = _applyOrderDmnService.SelectApplyBillDetailBySldhs(sldIds, OrganizeId);
            return Content(result.ToJson());
        }
    }
}
