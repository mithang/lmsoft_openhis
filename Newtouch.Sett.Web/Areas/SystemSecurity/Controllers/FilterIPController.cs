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

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemSecurity")]

    public class FilterIPController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IFilterIPApp _filterIPApp;
        public FilterIPController(IFilterIPApp filterIPApp)
        {
            _filterIPApp = filterIPApp;
        }


        [HttpGet]

        // Details.cshtml exists in source and Index JS opens /FilterIP/Details,
        // but neither the source controller nor the net6 Lib base defines a
        // Details action (Framework Web.Core base did). Serve the view shell;
        // the page fills itself via GetFormJson, like Form views do.
        public IActionResult Details(string keyValue)
        {
            return View();
        }

        public IActionResult GetGridJson(string keyword)
        {
            var data = _filterIPApp.GetList(keyword);
            return Content(data.ToJson());
        }
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var data = _filterIPApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(FilterIPEntity filterIPEntity, string keyValue)
        {
            _filterIPApp.SubmitForm(filterIPEntity, keyValue);
            return Success("Thao tác thành công。");
        }
        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _filterIPApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

    }
}
