using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.DaySettleManage.Controllers
{

    [Area("DaySettleManage")]

    public class DaySettleController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: DaySettleManage/DaySettle
        private readonly IDaySettleDmnService _DaySettleDmnService;
        public DaySettleController(IDaySettleDmnService daySettleDmnService)
        {
            _DaySettleDmnService = daySettleDmnService;
        }


        #region khung nhìn
        // No Index.cshtml exists in the shipped source (and no menu URL points
        // to DaySettle/Index — menus link to ApplicationIndex/Detailed/DzrIndex).
        // The base Index would fail with "view not found", so route it to the
        // area's main page instead of erroring.
        public override IActionResult Index()
        {
            // RedirectToAction drops the area here (generates /DaySettle/...
            // with area as a query string), so use a literal area URL.
            return Redirect("/DaySettleManage/DaySettle/ApplicationIndex");
        }

        public IActionResult OutpatientDaySett()
        {
            return View();
        }

        public IActionResult InpatientDaySett()
        {
            return View();
        }
        #endregion

        #region Lấy dữ liệu

        /// <summary>
        /// đượclấyngoại trúkhám（Nội trú）ngàykếttínhcộtbảng
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        public IActionResult GetDaySettListJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string mzzybz)
        {
            var list = new
            {
                rows = _DaySettleDmnService.GetOutPatientSettListJson(pagination, kssj, jssj, this.OrganizeId, mzzybz),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy thời gian quyết toán ngày lần trước
        /// </summary>
        /// <returns></returns>
        public IActionResult GetLastDaySettTime(string mzzybz)
        {
            return Success(null, _DaySettleDmnService.getLastSettDate(mzzybz));
        }
        #endregion

        #region dữ liệu Lưu
        /// <summary>
        /// ngoại trúkhámngàykếttính
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult SaveOutpatientDaySettle()
        {
            DateTime? lastSettTime;
            string lsJsTime = _DaySettleDmnService.getLastSettDate("0");
            if (string.IsNullOrEmpty(lsJsTime))
            {
                lastSettTime = null;
            }
            else
            {
                lastSettTime = DateTime.Parse(lsJsTime);
            }
            _DaySettleDmnService.SaveOutpatientDaySettleInfo(lastSettTime, DateTime.Now, this.UserIdentity.UserCode, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// nhập việnviệnngàykếttính
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult SaveInpatientDaySettle()
        {
            DateTime? lastSettTime;
            string lsJsTime = _DaySettleDmnService.getLastSettDate("1");
            if (string.IsNullOrEmpty(lsJsTime))
            {
                lastSettTime = null;
            }
            else
            {
                lastSettTime = DateTime.Parse(lsJsTime);
            }
            _DaySettleDmnService.SaveInpatientDaySettleInfo(lastSettTime, DateTime.Now, this.UserIdentity.UserCode, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// lấyhủyngoại trúkhámngàykếttính
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult CancleDaySettleInfo(string Id)
        {
            _DaySettleDmnService.CancleDaySettleInfo(Id, this.UserIdentity.UserCode);
            return Success();
        }
		#endregion


		/// <summary>
		/// xóatínhđốitổngtài khoản
		/// </summary>
		public IActionResult ApplicationIndex()
		{
			return View();
		}

		/// <summary>
		/// xóatínhrõchi tiết
		/// </summary>
		/// <returns></returns>
		public IActionResult ApplicationDetailed()
		{
			return View();
		}


		/// <summary>
		/// Đơn đề nghị thanh toán
		/// </summary>
		/// <returns></returns>
		public IActionResult ApplicationApply()
		{
			return View();
		}

		/// <summary>
		/// Hoàn lại thanh toán
		/// </summary>
		/// <returns></returns>
		public IActionResult ApplicationFallbackIndex()
		{
			return View();
		}

		public IActionResult Getclr_type()
		{
			var list = _DaySettleDmnService.Getclr_type();
			return Content(list.ToJson());
		}

		public IActionResult Getinsutype()
		{
			var list = _DaySettleDmnService.Getinsutype();
			return Content(list.ToJson());
		}

		/// <summary>
		/// đượclấyxóatínhđốitổngtài khoản
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <returns></returns>
		public IActionResult GetQszdd(string ksrq, string jsrq,string qslx,string xz)
		{
			var list = _DaySettleDmnService.GetQsdzzs(this.OrganizeId, ksrq, jsrq, qslx, xz);
			return Content(list.ToJson());
		}

		public IActionResult inserqssq(ybqssq ybqssq)
		{
			string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(ybqssq);
			System.Xml.XmlDocument xmlstr = (System.Xml.XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonstr, "root");
			var list = _DaySettleDmnService.inserqssq(xmlstr, this.OrganizeId);
			return Content(list.ToJson());
		}

		/// <summary>
		/// Lấy chi tiết thanh toán（trênnửabộphân）
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="jslx">Loại quyết toán</param>
		/// <returns></returns>
		public IActionResult GetQsmx(string ksrq, string jsrq, string jslx, string xzlx)
		{
			var list = _DaySettleDmnService.GetQsmx(this.OrganizeId, ksrq, jsrq, jslx, xzlx);
			return Content(list.ToJson());
		}

		/// <summary>
		/// Lấy chi tiết thanh toán（dướinửabộphân）
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="jslx">Loại quyết toán</param>
		/// <returns></returns>
		public IActionResult GetQsmx_1(string ksrq, string jsrq, string jslx, string xzlx)
		{
			var list = _DaySettleDmnService.GetQsmx_1(this.OrganizeId, ksrq, jsrq, jslx, xzlx);
			return Content(list.ToJson());
		}

		/// <summary>
		/// Đơn đề nghị thanh toán
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="sfyd">làkhôngkhácđịa</param>
		/// <returns></returns>
		public IActionResult GetQssq(string ksrq, string jsrq, string sfyd)
		{
			var list = _DaySettleDmnService.GetQssq(this.OrganizeId, ksrq, jsrq, sfyd);
			return Content(list.ToJson());
		}
		/// <summary>
		/// Hoàn lại thanh toán
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <returns></returns>
		public IActionResult GetQsht(string ksrq, string jsrq)
		{
			var list = _DaySettleDmnService.GetQsht(this.OrganizeId, ksrq, jsrq);
			return Content(list.ToJson());
		}

		public IActionResult DzrIndex()
		{
			return View();
		}
		public IActionResult DzrForm()
		{
			return View();
		}
        public IActionResult DzrCheck()
        {
            return View();
        }
        /// <summary>
        /// lịchsửđốitài khoảnghighi
        /// </summary>
        /// <returns></returns>
        public IActionResult DzrHistory()
        {
            return View();
        }
        public IActionResult lsdzList(string ksrq, string jsrq)
		{
			var list = _DaySettleDmnService.lsdzList(this.OrganizeId, ksrq, jsrq);
			return Content(list.ToJson());
		}

		public IActionResult Newdzfysj(string rq)
		{
			var list = _DaySettleDmnService.Newdzfysj(this.OrganizeId, rq);
			return Content(list.ToJson());
		}
        /// <summary>
        /// đượclấynàyđịađốitài khoảnsốdữ liệurõchi tiết
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        public IActionResult GetHistoryCheckList(string ksrq, string jsrq)
        {
            var list = _DaySettleDmnService.GetHistoryCheckList(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }
    }
}
