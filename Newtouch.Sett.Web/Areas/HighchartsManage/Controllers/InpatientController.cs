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
        /// 住院就诊人次人数分析
        /// </summary>
        /// <returns></returns>
        public IActionResult NumberofInpatientVisits()
        {
            return View();
        }
        /// <summary>
        /// 住院收入分析
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
        ///获取住院就诊人数
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInpatientVisitNum()
        {
            //获取住院新增人数
            List<OutpatientVisitNumVO> outpatientlist = _highchartsDmnService.GetInpatientVisitNum(OrganizeId);
            //获取出院人数
            List<InpatientVisitNumVO> inpatientList = _highchartsDmnService.GetInpatientDischargeNum(OrganizeId);
            //放在一个对象，返回到页面
            var data = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        /// <summary>
        /// 获取住院收入统计
        /// </summary>
        /// <returns></returns>
        public IActionResult GetInpatientSalaryNum()
        {
            var data = _highchartsDmnService.GetInpatientSalaryNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        #region Nhập viện Chi phí báo cáo
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
