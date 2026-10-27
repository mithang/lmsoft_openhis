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

namespace Newtouch.Sett.Web.Areas.ReportManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("ReportManage")]

    public class EchartsController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IEchartsDmnService _echartsDmnService;
        public EchartsController(IEchartsDmnService echartsDmnService)
        {
            _echartsDmnService = echartsDmnService;
        }


        //Chỉ số hiệu quả
        public IActionResult PerformanceIndicator()
        {
            return View();
        }

        /// <summary>
        /// Tổng lượt người Lượt người bình quân
        /// </summary>
        /// <returns></returns>
        public IActionResult SelectPerformanceIndicator(string orgId, string year)
        {
            var list = _echartsDmnService.SelectPerformanceIndicator(orgId, year);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tổng lượt người Lượt người bình quân
        /// </summary>
        /// <returns></returns>
        public IActionResult PerformanceMonthIndicator(string orgId, string year,int month)
        {
            var list = _echartsDmnService.PerformanceMonthIndicator(orgId, year,month < 10 ? "0" + month : month + "");
            return Content(list.ToJson());
        }

    }
}
