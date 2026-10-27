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

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    [Area("SystemManage")]

    public class DutyController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IDutyApp _dutyApp;

        public DutyController(IDutyApp dutyApp)
        {
            this._dutyApp = dutyApp;
        }

        [HttpGet]

        public IActionResult GetListJson()
        {
            var data = _dutyApp.GetList();
            return Content(data.ToJson());
        }

        [HttpGet]

        public IActionResult GetGridJson(Pagination pagination,string keyword)
        {
            var data = new {
                rows = _dutyApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var data = _dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(DutyEntity roleEntity, string keyValue)
        {
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            _dutyApp.SubmitForm(roleEntity, keyValue);
            return Success("Thao tác thành công。");
        }
        [HttpPost]

        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _dutyApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }
    }
}
