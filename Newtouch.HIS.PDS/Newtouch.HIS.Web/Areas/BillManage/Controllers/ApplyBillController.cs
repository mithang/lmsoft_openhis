using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Tools;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Application.Implementation.Process;

namespace Newtouch.HIS.Web.Areas.BillManage.Controllers
{
    /// <summary>
    /// lĩnh nội bộ đơn
    /// </summary>
    public class ApplyBillController : ControllerBase
    {
        private readonly IApplyDmnService applyDmnService;

        #region 内部申领单查询
        /// <summary>
        /// lĩnh nội bộ đơn tra cứu
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            return View();
        }

        /// <summary>
        /// lấy phiếu lĩnh chính thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        //[Core.Attributes.HandlerAuthorizeIgnore]
        public ActionResult ApplyMainInfoSearch(Pagination pagination, int ffzt, string djh, DateTime startTime, DateTime endTime, string slbm = "")
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
        /// lấy phiếu lĩnh chính thông tin
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ffzt"></param>
        /// <param name="djh"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ApplyDetailSearch(Pagination pagination, string sldId)
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

        #region 申领出库

        /// <summary>
        /// xuất kho theo phiếu lĩnh
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyOutStock()
        {
            return View();
        }

        /// <summary>
        /// lấy xuất kho theo phiếu lĩnh chi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        public ActionResult ApplyOutStockDetailSearch(Pagination pagination, string sldIds)
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
        /// lấy xuất kho theo phiếu lĩnh chi tiết
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="sldIds"></param>
        /// <returns></returns>
        public ActionResult Abandonzt(string sldId)
        {
            var result = applyDmnService.UpgradeStatus(sldId, (int)EnumSLDDeliveryStatus.Abandon, OrganizeId);
            return string.IsNullOrWhiteSpace(result) ? Success() : Error(result);
        }

        /// <summary>
        /// gửi phát thuốc
        /// </summary>
        /// <param name="fymx"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitApplyOutStock(List<ApplyOutStockVEntity> fymx, string slbm)
        {
            var processResult = new SubmitApplyOutStockProcess(fymx).Process();
            return processResult.IsSucceed ? Success() : Error(processResult.ResultMsg);
        }

        #endregion
    }
}