using NewtouchHIS.Framework.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class yfController : OrgControllerBase
    {
        // GET: ReportManage/yf
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PharmacyMonthlyQuery()
        {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
    }
}