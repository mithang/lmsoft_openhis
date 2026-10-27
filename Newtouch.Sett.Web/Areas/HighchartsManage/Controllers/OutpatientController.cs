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

    public class OutpatientController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;
        public OutpatientController(IHighchartsDmnService highchartsDmnService)
        {
            _highchartsDmnService = highchartsDmnService;
        }


        #region trang khởi tạo
        /// <summary>
        /// 门诊就诊人次人数分析
        /// </summary>
        /// <returns></returns>
        public IActionResult NumberofOutpatientVisits()
        {
            return View();
        }
        /// <summary>
        /// 门诊收入分析
        /// </summary>
        /// <returns></returns>
        public IActionResult NumberofOutpatientSalary()
        {
            return View();
        }
        #endregion

        #region Ngoại trú Chi phí báo cáo
        public IActionResult PatientVisitPerTherapist()
        {
            return View();
        }

        /// <summary>
        /// 获取就诊费用
        /// </summary>
        public IActionResult GetVisitNum(string year, string datatype, string orgId)
        {
            var PatientVisitPerThe = new PatientVisitPerThe();
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
                PatientVisitPerThe.discharge = _highchartsDmnService.GetTherapistavgDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetTherapistavgVisit(year, orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetTherapistDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetTherapistVisit(year, orgId);
            }


            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }

        public IActionResult GetMonthVisitNum(string year, string datatype, string orgId,int month) {
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
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthTherapistavgVisit(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistavgDischarge(year, orgId, month < 10 ? "0" + month : month + "");
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthTherapistVisit(year, orgId, month < 10 ? "0" + month : month + "");
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }
        #endregion


        /// <summary>
        ///获取门诊就诊人数
        /// </summary>
        /// <returns></returns>
        public IActionResult GetOutpatientVisitNum()
        {
            var data = _highchartsDmnService.GetOutpatientVisitNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        /// <summary>
        /// 获取门诊就诊人次
        /// </summary>
        /// <returns></returns>
        public IActionResult GetOutpatientVisitPerNum()
        {
            var data = _highchartsDmnService.GetOutpatientVisitPerNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }


        /// <summary>
        /// 获取门诊收入统计
        /// </summary>
        /// <returns></returns>
        public IActionResult GetOutpatientSalaryNum()
        {
            var data = _highchartsDmnService.GetOutpatientSalaryNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

    }
}
