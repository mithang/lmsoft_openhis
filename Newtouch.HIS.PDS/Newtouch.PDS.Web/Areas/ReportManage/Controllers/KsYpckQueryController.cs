using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class KsYpckQueryController : ControllerBase
    {
        // GET: ReportManage/KsYpckQuery
        public IActionResult KsYpckQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
    }
}