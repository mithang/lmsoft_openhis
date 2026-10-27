using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.HospitalizationManage.Controllers
{

    /// <summary>
    /// quyết toán nhập viện
    /// </summary>
    [Area("HospitalizationManage")]
    public class SettlementController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        //quyết toán nhập viện
        private readonly IHospSettApp _outHospSettApp;
        //Hủyquyết toán nhập viện
        private readonly IHospSettCancelApp _hospSettCancelApp;
        //Truy vấnquyết toán nhập viện
        private readonly IHospSettDmnService _hospSettDmnService;
        public SettlementController(IHospSettApp outHospSettApp, IHospSettCancelApp hospSettCancelApp, IHospSettDmnService hospSettDmnService)
        {
            _outHospSettApp = outHospSettApp;
            _hospSettCancelApp = hospSettCancelApp;
            _hospSettDmnService = hospSettDmnService;
        }


        #region quyết toán nhập viện（Có thuật toán BHYT）

        /// <summary>
        /// Lấy trạng tháiquyết toán của bệnh nhân nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult GetStatusDetail(string zyh, string kh)
        {
            var dto = _outHospSettApp.GetPatHospStatusDetail(zyh, kh);
            return Success("", dto);
        }

        /// <summary>
        /// Xem trước tính phí phân loạiquyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje">Số tiềnquyết toán Ngăn thay đổi phí trong quá trình</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SettPreview(string zyh, DateTime expectedcyrq, decimal expectedjsje)
        {
            var dto = _outHospSettApp.GetHospSettPatClassifyChargePreview(zyh, expectedcyrq, expectedjsje);
            return View(dto);
        }

        /// <summary>
        /// GửiLưuquyết toán
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedyjjzhye">Tài khoảntạm ứngHiện tạiSố dư（cònChưaquyết toán） Ngăn thay đổi phí trong quá trình</param>
        /// <param name="expectedjsje">Số tiềnquyết toán Ngăn thay đổi phí trong quá trình</param>
        /// <param name="expectedzhaoling">Tiền thừa Ngăn thay đổi phí trong quá trình</param>
        /// <param name="xjzfListStr"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitSett(string zyh, DateTime expectedcyrq, decimal expectedyjjzhye, decimal expectedjsje, string fph, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan)
        {
            var successresult = _outHospSettApp.SaveSett(zyh, expectedcyrq, fph, expectedyjjzhye, expectedjsje, expectedzhaoling, xjzfListStr, shishoukuan);
            return Success("quyết toán thành công", successresult);
        }

        /// <summary>
        /// quyết toán thành côngcủaDialogNhắc nhở
        /// </summary>
        /// <param name="yingshoukuan"></param>
        /// <param name="ssk"></param>
        /// <param name="srce"></param>
        /// <param name="zhaoling"></param>
        /// <returns></returns>
        public IActionResult SettSuccessDialog(string yingshoukuan, string ssk, string srce, string zhaoling)
        {
            ViewBag.yingshoukuan = yingshoukuan;
            ViewBag.ssk = ssk;
            ViewBag.srce = srce;
            ViewBag.zhaoling = zhaoling;
            return View();
        }

        #endregion

        #region Hủyquyết toán nhập viện（Có thuật toán BHYT）

        /// <summary>
        /// Hủyquyết toán Chế độ xem
        /// </summary>
        /// <returns></returns>
        public IActionResult CancelIndex()
        {
            return View();
        }

        /// <summary>
        /// Hủyquyết toán Lấy trạng tháiquyết toán của bệnh nhân nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult CancelGetStatusDetail(string zyh, string kh)
        {
            var dto = _hospSettCancelApp.GetPatHospStatusDetail(zyh, kh);
            return Success(null, dto);
        }

        /// <summary>
        /// Hủyquyết toán Gửi
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="cancelReason"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public IActionResult SubmitCancel(string zyh, int expectedjsnm, string cancelReason)
        {
            _hospSettCancelApp.DoCancel(zyh, expectedjsnm, cancelReason);

            return Success("Hủyquyết toán thành công");
        }

        #endregion

        #region GRSTruy vấnquyết toán nhập viện（một bộ trang：quyết toán phí+Đăng ký xuất viện+Hủy đăng ký xuất viện）
        public IActionResult InPatientSettQuery()
        {
            return View();
        }

        public IActionResult GridInPatientGridJson(Pagination pagination, HospSettQueryReq req)
        {
            var data = new
            {
                rows = _hospSettDmnService.GridInPatientQueryGridJson(pagination, req, OrganizeId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        #endregion

        #region （thông thườngBệnh viện）HISTruy vấnquyết toán nhập viện

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult HospitalizationSettlementQuery()
        {
            ViewBag.ReportServerHOST = "";
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="fph"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        public IActionResult GetPaginationSettlementList(Pagination pagination, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq)
        {
            var data = new
            {
                rows = _hospSettDmnService.GetPaginationSettlementList(pagination, OrganizeId, keyword, fph, jsksrq, jsjsrq),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thông tin bệnh nhânquyết toán tự phí chờ tải lên
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        public IActionResult GetPaginationZFSettlementList(Pagination pagination, string keyword, DateTime? jsksrq, DateTime? jsjsrq)
        {
            var data = new
            {
                rows = _hospSettDmnService.GetPaginationZFSettlementList(pagination, OrganizeId, keyword, jsksrq, jsjsrq),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IActionResult GetSettlementDetails(int jsnm)
        {
            var list = _hospSettDmnService.SettlementDetailsQuery(OrganizeId, jsnm);
            return Content(list.ToJson());
        }
        public IActionResult GetSettlementItems()
        {
            return View();
        }
        /// <summary>
        /// Chi tiết phí truy vấnquyết toán xuất viện
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <param name="mc"></param>
        /// <returns></returns>
        public IActionResult GetSettleItemFrom(Pagination pagination, string zyh, string dlCode, string jsnms, string mc)
        {
            var dto = _hospSettDmnService.SettlementDetailsItemsQuery(pagination, zyh, OrganizeId, dlCode, jsnms, mc);
            var data = new
            {
                rows = dto,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion

        /// <summary>
        /// cập nhậtquyết toánBản ghicủaSố hóa đơn
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public IActionResult UpdateSettInvoiceNo(int jsnm, string fph)
        {
            _hospSettDmnService.UpdateSettInvoiceNo(OrganizeId, jsnm, fph);
            return Success();
        }
    }
}
