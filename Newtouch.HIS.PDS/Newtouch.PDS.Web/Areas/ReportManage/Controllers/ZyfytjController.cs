using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class ZyfytjController : ControllerBase
    {
        // GET: ReportManage/Zyfytj
        public IActionResult QxIndex()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }
    }
}