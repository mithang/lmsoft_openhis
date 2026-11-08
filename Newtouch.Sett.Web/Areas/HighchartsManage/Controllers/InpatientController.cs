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

namespace Newtouch.Sett.Web.Areas.HighchartsManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("HighchartsManage")]

    public class InpatientController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;
        public InpatientController(IHighchartsDmnService highchartsDmnService)
        {
            _highchartsDmnService = highchartsDmnService;
        }



        #region trang khởi tạo
        /// <summary>
        /// nhập viện viện thì khám người lần người số phân phân tích
        /// </summary>
        /// <returns></returns>
        public IActionResult NumberofInpatientVisits()
        {
            return View();
        }
        /// <summary>
        /// nhập viện viện thu vào phân phân tích
        /// </summary>
        /// <returns></returns>
        public IActionResult NumberofInpatientSalary()
        {
            return View();
        }

        public IActionResult InPatientVisitPerTherapist()
        {
            return View();
        }
        #endregion

        /// <summary>
        ///được lấy nhập viện viện thì khám người số
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInpatientVisitNum()
        {
            //được lấy nhập viện viện mới thêm người số
            List<OutpatientVisitNumVO> outpatientlist = _highchartsDmnService.GetInpatientVisitNum(OrganizeId);
            //được lấy ra viện người số
            List<InpatientVisitNumVO> inpatientList = _highchartsDmnService.GetInpatientDischargeNum(OrganizeId);
            //đặt tại một cái đối tượng,trả quay đến trang mặt
            var data = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        /// <summary>
        /// được lấy nhập viện viện thu vào thống tính
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInpatientSalaryNum()
        {
            var data = _highchartsDmnService.GetInpatientSalaryNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        #region Nhập viện Chi phí báo cáo
        /// <summary>
        /// Lấy chi phí khám
        /// </summary>
        public IActionResult GetVisitNum(string year, string datatype, string orgId)
        {
            var PatientVisitPerThe = new PatientVisitPerThe();
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("Vui lòng chọn Năm！");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("Vui lòng chọn Tổ chức！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetInTherapistavgDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetInTherapistavgVisit(year, orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetInTherapistDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetInTherapistVisit(year, orgId);
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }

        public IActionResult GetMonthVisitNum(string year, string datatype, string orgId, int month)
        {
            var PatientVisitPerThe = new PatientMonthVisitPerThe();
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("Vui lòng chọn Năm！");
            }
            if (string.IsNullOrWhiteSpace(datatype))
            {
                throw new FailedException("Vui lòng chọn Loại！");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("Vui lòng chọn Tổ chức！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistavgDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthInTherapistavgVisit(year,month < 10 ? "0" + month : month + "", orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthInTherapistDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthInTherapistVisit(year, month < 10 ? "0" + month : month + "", orgId);
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }
        #endregion
    }
}
