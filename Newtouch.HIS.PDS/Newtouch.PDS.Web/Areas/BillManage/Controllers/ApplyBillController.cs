using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Tools;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Application.Implementation.Process;

namespace Newtouch.HIS.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// Đơn lĩnh nội bộ
    /// </summary>
    [Area("BillManage")]
    public class ApplyBillController : ControllerBase
    {
        private readonly IApplyDmnService applyDmnService;

        #region Truy vấn đơn lĩnh nội bộ
        /// <summary>
        /// Truy vấn đơn lĩnh nội bộ
        /// </summary>
        /// <returns></returns>
        public IActionResult Query()
        {
            return View();
        }

        /// <summary>
        /// Lấy thông tin chính đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        //[Core.Attributes.HandlerAuthorizeIgnore]
        public IActionResult ApplyMainInfoSearch(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string slbm = "")
        {
            var receiptMaininfoList = new
            {
                rows = applyDmnService.GetApplyMainInfo(pagination, ffzt, djh, startTime, endTime, Constants.CurrentYfbm.yfbmCode, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptMaininfoList.ToJson());
        }

        /// <summary>
        /// Lấy thông tin chính đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ApplyDetailSearch(Pagination pagination, string sldId)
        {
            var receiptdetailList = new
            {
                rows = applyDmnService.GetApplyDetails(pagination, sldId, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptdetailList.ToJson());
        }
        #endregion

        #region Xuất kho theo đơn lĩnh

        /// <summary>
        /// Xuất kho theo đơn lĩnh
        /// </summary>
        /// <returns></returns>
        public IActionResult ApplyOutStock()
        {
            return View();
        }

        /// <summary>
        /// Lấy chi tiết xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        public IActionResult ApplyOutStockDetailSearch(Pagination pagination, string sldIds)
        {
            IList<ApplyOutStockVEntity> data = new List<ApplyOutStockVEntity>();
            if (!string.IsNullOrWhiteSpace(sldIds))
            {
                data = applyDmnService.GetApplyOutStockDetail(pagination, sldIds, Constants.CurrentYfbm.yfbmCode, OrganizeId);
            }
            var receiptdetailList = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(receiptdetailList.ToJson());
        }

        /// <summary>
        /// Lấy chi tiết xuất kho theo đơn lĩnh
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        public IActionResult Abandonzt(string sldId)
        {
            var result = applyDmnService.UpgradeStatus(sldId, (int)EnumSLDDeliveryStatus.Abandon, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// Gửi phát thuốc
        /// </summary>
        /// <param name="fymx"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SubmitApplyOutStock(List<ApplyOutStockVEntity> fymx, string slbm)
        {
            var processResult = new SubmitApplyOutStockProcess(fymx).Process();
            return processResult.IsSucceed ? Success() : Error(processResult.ResultMsg);
        }

        #endregion
    }
}