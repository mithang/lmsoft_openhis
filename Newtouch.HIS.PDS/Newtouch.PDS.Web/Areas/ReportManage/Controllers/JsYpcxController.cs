using Newtouch.Core.Common.Utils;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class JsYpcxController : ControllerBase
    {
        private readonly IfyDmnService _ifyDmnService;

        // GET: ReportManage/JsYpcx
        public IActionResult QxIndex()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }
        public IActionResult YPCKMXIndex()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public IActionResult GetDlList()
        {
            var data = _ifyDmnService.getYpdl(OrganizeId);
            return Json(data);
        }
    }
}