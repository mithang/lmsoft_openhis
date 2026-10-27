using NewtouchHIS.Framework.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class PurchaseController : OrgControllerBase
    {
        // GET: ReportManage/Purchase
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PurchaseBillDetailQuery() {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
    }
}