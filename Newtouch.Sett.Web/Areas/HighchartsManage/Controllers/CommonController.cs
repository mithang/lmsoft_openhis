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

    public class CommonController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;
        public CommonController(IHighchartsDmnService highchartsDmnService)
        {
            _highchartsDmnService = highchartsDmnService;
        }


        #region trang khởi tạo
        public IActionResult NumberofSalaryCompared()
        {
            return View();
        }
        #endregion

        public IActionResult GetSalaryNumCompared()
        {
            var data = _highchartsDmnService.GetSalaryNumCompared(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }
    }
}
