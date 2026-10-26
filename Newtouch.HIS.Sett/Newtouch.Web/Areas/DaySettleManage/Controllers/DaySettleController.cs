using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.DaySettleManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DaySettleManage.Controllers
{
    public class DaySettleController : ControllerBase
    {
        // GET: DaySettleManage/DaySettle
        private readonly IDaySettleDmnService _DaySettleDmnService;

        #region 视图
        public ActionResult OutpatientDaySett()
        {
            return View();
        }

        public ActionResult InpatientDaySett()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// Lấy ngoại trú（Nhập viện）Danh sách quyết toán ngày
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="mzzybz"></param>
        /// <returns></returns>
        public ActionResult GetDaySettListJson(Pagination pagination, DateTime? kssj, DateTime? jssj, string mzzybz)
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
        /// Lấy thời gian quyết toán ngày lần trên
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLastDaySettTime(string mzzybz)
        {
            return Success(null, _DaySettleDmnService.getLastSettDate(mzzybz));
        }
        #endregion

        #region 数据保存
        /// <summary>
        /// Quyết toán ngày ngoại trú
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveOutpatientDaySettle()
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
        /// Nhập viện quyết toán ngày
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveInpatientDaySettle()
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
        /// Hủy quyết toán ngày ngoại trú
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CancleDaySettleInfo(string Id)
        {
            _DaySettleDmnService.CancleDaySettleInfo(Id, this.UserIdentity.UserCode);
            return Success();
        }
		#endregion


		/// <summary>
		/// 清算对总帐
		/// </summary>
		public ActionResult ApplicationIndex()
		{
			return View();
		}

		/// <summary>
		/// 清算明细
		/// </summary>
		/// <returns></returns>
		public ActionResult ApplicationDetailed()
		{
			return View();
		}


		/// <summary>
		/// 清算申请
		/// </summary>
		/// <returns></returns>
		public ActionResult ApplicationApply()
		{
			return View();
		}

		/// <summary>
		/// 清算回退
		/// </summary>
		/// <returns></returns>
		public ActionResult ApplicationFallbackIndex()
		{
			return View();
		}

		public ActionResult Getclr_type()
		{
			var list = _DaySettleDmnService.Getclr_type();
			return Content(list.ToJson());
		}

		public ActionResult Getinsutype()
		{
			var list = _DaySettleDmnService.Getinsutype();
			return Content(list.ToJson());
		}

		/// <summary>
		/// 获取清算对总帐
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <returns></returns>
		public ActionResult GetQszdd(string ksrq, string jsrq,string qslx,string xz)
		{
			var list = _DaySettleDmnService.GetQsdzzs(this.OrganizeId, ksrq, jsrq, qslx, xz);
			return Content(list.ToJson());
		}

		public ActionResult inserqssq(ybqssq ybqssq)
		{
			string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(ybqssq);
			System.Xml.XmlDocument xmlstr = (System.Xml.XmlDocument)Newtonsoft.Json.JsonConvert.DeserializeXmlNode(jsonstr, "root");
			var list = _DaySettleDmnService.inserqssq(xmlstr, this.OrganizeId);
			return Content(list.ToJson());
		}

		/// <summary>
		/// 获取清算明细（Một phần nửa trên）
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="jslx">Loại quyết toán</param>
		/// <returns></returns>
		public ActionResult GetQsmx(string ksrq, string jsrq, string jslx, string xzlx)
		{
			var list = _DaySettleDmnService.GetQsmx(this.OrganizeId, ksrq, jsrq, jslx, xzlx);
			return Content(list.ToJson());
		}

		/// <summary>
		/// 获取清算明细（Một phần nửa dưới）
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="jslx">Loại quyết toán</param>
		/// <returns></returns>
		public ActionResult GetQsmx_1(string ksrq, string jsrq, string jslx, string xzlx)
		{
			var list = _DaySettleDmnService.GetQsmx_1(this.OrganizeId, ksrq, jsrq, jslx, xzlx);
			return Content(list.ToJson());
		}

		/// <summary>
		/// 清算申请
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <param name="sfyd">Có phải nơi khác không</param>
		/// <returns></returns>
		public ActionResult GetQssq(string ksrq, string jsrq, string sfyd)
		{
			var list = _DaySettleDmnService.GetQssq(this.OrganizeId, ksrq, jsrq, sfyd);
			return Content(list.ToJson());
		}
		/// <summary>
		/// 清算回退
		/// </summary>
		/// <param name="ksrq">Ngày bắt đầu</param>
		/// <param name="jsrq">Ngày kết thúc</param>
		/// <returns></returns>
		public ActionResult GetQsht(string ksrq, string jsrq)
		{
			var list = _DaySettleDmnService.GetQsht(this.OrganizeId, ksrq, jsrq);
			return Content(list.ToJson());
		}

		public ActionResult DzrIndex()
		{
			return View();
		}
		public ActionResult DzrForm()
		{
			return View();
		}
        public ActionResult DzrCheck()
        {
            return View();
        }
        /// <summary>
        /// 历史对账记录
        /// </summary>
        /// <returns></returns>
        public ActionResult DzrHistory()
        {
            return View();
        }
        public ActionResult lsdzList(string ksrq, string jsrq)
		{
			var list = _DaySettleDmnService.lsdzList(this.OrganizeId, ksrq, jsrq);
			return Content(list.ToJson());
		}

		public ActionResult Newdzfysj(string rq)
		{
			var list = _DaySettleDmnService.Newdzfysj(this.OrganizeId, rq);
			return Content(list.ToJson());
		}
        /// <summary>
        /// 获取本地对账数据明细
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <returns></returns>
        public ActionResult GetHistoryCheckList(string ksrq, string jsrq)
        {
            var list = _DaySettleDmnService.GetHistoryCheckList(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }
    }
}