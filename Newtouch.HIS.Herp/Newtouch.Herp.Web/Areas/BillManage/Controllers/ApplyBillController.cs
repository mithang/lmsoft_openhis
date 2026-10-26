using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Quản lý phiếu lĩnh
    /// </summary>
    public class ApplyBillController : ControllerBase
    {
        private readonly IKfApplyOrderDmnService _applyOrderDmnService;

        /// <summary>
        /// Tra cứu phiếu lĩnh
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Tra cứu thông tin chính phiếu lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="pdh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <returns></returns>
        public ActionResult ApplyBillInfoQuery(Pagination pagination, DateTime kssj, DateTime jssj, string pdh, int applyType, int applyProcess)
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
        /// Tra cứu thông tin chính phiếu lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="pdh"></param>
        /// <param name="applyType"></param>
        /// <param name="applyProcess"></param>
        /// <param name="applyProcesses">Trạng thái lĩnh có thể chọn</param>
        /// <returns></returns>
        public ActionResult ApplyBillInfoQueryV2(Pagination pagination, DateTime kssj, DateTime jssj, string pdh, int applyType, int applyProcess, string slbm, string applyProcesses)
        {
            if (string.IsNullOrWhiteSpace(applyProcesses)) throw new FailedException("可选申领状态不能为空");
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
        /// Tra cứu thông tin chi tiết phiếu lĩnh
        /// </summary>
        /// <param name="sldId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplyBillDetailBySldhQuery(string sldId)
        {
            var result = _applyOrderDmnService.SelectApplyBillDetail(sldId, OrganizeId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// Tra cứu thông tin chi tiết phiếu lĩnh
        /// </summary>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplyBillDetailQuery(string sldIds)
        {
            if (string.IsNullOrWhiteSpace(sldIds)) return Content(new List<VApplyBillDetailEntity>().ToJson());
            var result = _applyOrderDmnService.SelectApplyBillDetailBySldhs(sldIds, OrganizeId);
            return Content(result.ToJson());
        }
    }
}