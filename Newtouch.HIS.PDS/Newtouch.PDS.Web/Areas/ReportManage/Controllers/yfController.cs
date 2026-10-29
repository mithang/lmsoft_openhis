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
        // FIX gstack T-30: khong co Views/yf/Index.cshtml (chi co
        // PharmacyMonthlyQuery.cshtml). Menu DB PDS khong co dong yf nao;
        // trang that gan nhat cung controller la PharmacyMonthlyQuery.
        // Chuyen huong toi thieu ve do thay vi tao view gia. Dung Redirect
        // literal (xem T-23: route "default" dang ky truoc "areas").
        public override IActionResult Index()
        {
            return Redirect("/ReportManage/yf/PharmacyMonthlyQuery");
        }

        public IActionResult PharmacyMonthlyQuery()
        {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
    }
}