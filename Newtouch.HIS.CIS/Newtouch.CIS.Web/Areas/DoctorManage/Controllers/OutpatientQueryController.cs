using NewtouchHIS.Framework.Web.Controllers;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.CIS.Compat;
using Newtouch.Domain.IDomainServices.Outpatient;
using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.DoctorManage.Controllers
{
    [Area("DoctorManage")]
    public class OutpatientQueryController : OrgControllerBase
    {
        // GET: DoctorManage/OutpatientQuery

        private readonly IOutpatientQueryDmnService _outpatientQueryDmnService;

        public OutpatientQueryController(IOutpatientQueryDmnService outpatientQueryDmnService)
        {
            this._outpatientQueryDmnService = outpatientQueryDmnService;
        }
        #region Trang
        /// <summary>
        /// 门诊就诊记录
        /// </summary>
        /// <returns></returns>
        public IActionResult ConsultRecord()
        {
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }

        /// <summary>
        /// 门诊就诊详情
        /// </summary>
        /// <returns></returns>
        public IActionResult ConsultDetail()
        {
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        /// <summary>
        /// 查询门诊详细资料
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryOutpatientDetail()
        {
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        /// <summary>
        /// 查询门诊预约记录
        /// </summary>
        /// <returns></returns>
        public IActionResult QueryOutpatientReservation()
        {
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        #endregion

        #region Ngoại trúKhám bệnhGhi chép
        public IActionResult GetConsultRecordGridJson(Pagination pagination, string kssj, string jssj)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetConsultRecordGridJson(pagination, OrganizeId, kssj,jssj, this.UserIdentity.UserCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion

        #region Ngoại trúKhám bệnhChi tiết
        public IActionResult GetConsultDetailGridJson(Pagination pagination, string kssj, string jssj,string keyword)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetConsultDetailGridJson(pagination, OrganizeId, kssj, jssj, this.UserIdentity.UserCode, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion
        #region Tra cứuNgoại trú详细Tài liệu
        public IActionResult GetOutpatientDetailGridJson(Pagination pagination, string kssj, string jssj)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetOutpatientDetailGridJson(pagination, OrganizeId, kssj, jssj,this.UserIdentity.UserCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        public IActionResult GetOutpatientDetailMX(string mzh)
        {
            var rows = _outpatientQueryDmnService.GetOutpatientDetailMXGridJson(mzh, OrganizeId);
            return Content(rows.ToJson());
        }
        #endregion
        #region Tra cứuNgoại trúĐặt hẹnGhi chép
        public IActionResult GetReservationGridJson(Pagination pagination, string kssj, string jssj,string xm)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetReservationGridJson(pagination, OrganizeId, kssj, jssj, this.UserIdentity.UserCode,xm),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion


		public IActionResult MZcasehistory()
		{
			ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
			ViewBag.OrganizeId = this.OrganizeId;
			return View();
		}
        #region Ngoại trúBệnh ánGhi chép
        public IActionResult GetMedicalRecordGridJson(Pagination pagination, string kssj, string jssj)
        {
            var data = new
            {
                rows = _outpatientQueryDmnService.GetConsultRecordGridJson(pagination, OrganizeId, kssj, jssj, null),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        #endregion
        public IActionResult MZBLlsinfo(Pagination pagination, string blh)
		{
			var data = new
			{
				rows = _outpatientQueryDmnService.GetlsblInfoJson(pagination, OrganizeId, blh, this.UserIdentity.UserCode),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records,
			};
			return Content(data.ToJson());
		}
		public IActionResult MZlsjzCfInfo(Pagination pagination, string jzid)
		{
			var data = new
			{
				rows = _outpatientQueryDmnService.GetlsjzcfblInfoJson(pagination, OrganizeId, jzid, this.UserIdentity.UserCode),
				total = pagination.total,
				page = pagination.page,
				records = pagination.records,
			};
			return Content(data.ToJson());
		}
	}
}