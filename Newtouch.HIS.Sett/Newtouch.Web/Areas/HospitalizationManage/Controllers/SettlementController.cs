using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HospitalizationManage.Controllers
{
    /// <summary>
    /// Quyết toán nhập viện
    /// </summary>
    public class SettlementController : ControllerBase
    {
        //Quyết toán nhập viện
        private readonly IHospSettApp _outHospSettApp;
        //Hủy quyết toán nhập viện
        private readonly IHospSettCancelApp _hospSettCancelApp;
        //Nhập viện tra cứu quyết toán
        private readonly IHospSettDmnService _hospSettDmnService;

        #region 住院结算（有医保算法）

        /// <summary>
        /// Lấy trạng thái quyết toán bệnh nhân nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult GetStatusDetail(string zyh, string kh)
        {
            var dto = _outHospSettApp.GetPatHospStatusDetail(zyh, kh);
            return Success("", dto);
        }

        /// <summary>
        /// 结算分类计费预览
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje">Số tiền quyết toán thay đổi chi phí của trong quá trình ngăn</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SettPreview(string zyh, DateTime expectedcyrq, decimal expectedjsje)
        {
            var dto = _outHospSettApp.GetHospSettPatClassifyChargePreview(zyh, expectedcyrq, expectedjsje);
            return View(dto);
        }

        /// <summary>
        /// Gửi quyết toán lưu
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedyjjzhye">Số dư hiện tại tài khoản tiền tạm ứng（Chưa quyết toán） thay đổi chi phí của trong quá trình ngăn</param>
        /// <param name="expectedjsje">Số tiền quyết toán thay đổi chi phí của trong quá trình ngăn</param>
        /// <param name="expectedzhaoling">Tiền thừa thay đổi chi phí của trong quá trình ngăn</param>
        /// <param name="xjzfListStr"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitSett(string zyh, DateTime expectedcyrq, decimal expectedyjjzhye, decimal expectedjsje, string fph, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan)
        {
            var successresult = _outHospSettApp.SaveSett(zyh, expectedcyrq, fph, expectedyjjzhye, expectedjsje, expectedzhaoling, xjzfListStr, shishoukuan);
            return Success("结算成功", successresult);
        }

        /// <summary>
        /// quyết toán thành côngDialog提示
        /// </summary>
        /// <param name="yingshoukuan"></param>
        /// <param name="ssk"></param>
        /// <param name="srce"></param>
        /// <param name="zhaoling"></param>
        /// <returns></returns>
        public ActionResult SettSuccessDialog(string yingshoukuan, string ssk, string srce, string zhaoling)
        {
            ViewBag.yingshoukuan = yingshoukuan;
            ViewBag.ssk = ssk;
            ViewBag.srce = srce;
            ViewBag.zhaoling = zhaoling;
            return View();
        }

        #endregion

        #region 取消住院结算（有医保算法）

        /// <summary>
        /// Hủy quyết toán Chế độ xem
        /// </summary>
        /// <returns></returns>
        public ActionResult CancelIndex()
        {
            return View();
        }

        /// <summary>
        /// Hủy quyết toán Lấy trạng thái quyết toán bệnh nhân nhập viện
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CancelGetStatusDetail(string zyh, string kh)
        {
            var dto = _hospSettCancelApp.GetPatHospStatusDetail(zyh, kh);
            return Success(null, dto);
        }

        /// <summary>
        /// Hủy quyết toán Gửi
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="cancelReason"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitCancel(string zyh, int expectedjsnm, string cancelReason)
        {
            _hospSettCancelApp.DoCancel(zyh, expectedjsnm, cancelReason);

            return Success("取消结算成功");
        }

        #endregion

        #region GRS住院结算查询（一套页面：费用结算+出院登记+取消出院登记）
        public ActionResult InPatientSettQuery()
        {
            return View();
        }

        public ActionResult GridInPatientGridJson(Pagination pagination, HospSettQueryReq req)
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

        #region （常规医院）HIS住院结算查询

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult HospitalizationSettlementQuery()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
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
        public ActionResult GetPaginationSettlementList(Pagination pagination, string keyword, string fph, DateTime? jsksrq, DateTime? jsjsrq)
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
        /// 待上传的自费结算病人信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="jsksrq"></param>
        /// <param name="jsjsrq"></param>
        /// <returns></returns>
        public ActionResult GetPaginationZFSettlementList(Pagination pagination, string keyword, DateTime? jsksrq, DateTime? jsjsrq)
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
        public ActionResult GetSettlementDetails(int jsnm)
        {
            var list = _hospSettDmnService.SettlementDetailsQuery(OrganizeId, jsnm);
            return Content(list.ToJson());
        }
        public ActionResult GetSettlementItems()
        {
            return View();
        }
        /// <summary>
        /// Xuất viện chi tiết chi phí tra cứu quyết toán
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="zyh"></param>
        /// <param name="dlCode"></param>
        /// <param name="mc"></param>
        /// <returns></returns>
        public ActionResult GetSettleItemFrom(Pagination pagination, string zyh, string dlCode, string jsnms, string mc)
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
        /// số hóa đơn cập nhật bản ghi quyết toán
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <returns></returns>
        public ActionResult UpdateSettInvoiceNo(int jsnm, string fph)
        {
            _hospSettDmnService.UpdateSettInvoiceNo(OrganizeId, jsnm, fph);
            return Success();
        }
    }
}