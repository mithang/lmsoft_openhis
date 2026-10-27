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

namespace Newtouch.Sett.Web.Areas.SystemSecurity.Controllers
{

    [Area("SystemSecurity")]

    public class LogController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ILogApp _logApp;

        public LogController(ILogApp logApp)
        {
            this._logApp = logApp;
        }

        [HttpGet]
        public IActionResult RemoveLog()
        {
            return View();
        }
        [HttpGet]

        public IActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = _logApp.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitRemoveLog(string keepTime)
        {
            _logApp.RemoveLog(keepTime);
            return Success("Xóa trống Thành công。");
        }
    }
}
