using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.EMR.Web.Areas.SystemManage.Controllers
{
    // Stub: the original Sys* controllers shipped only as Framework binaries
    // (no source). Their views are kept on disk for reference, but the live
    // system-management functions are served by the SSO portal, so these
    // actions return an explicit Vietnamese notice instead of 404.
    // No fake data is ever rendered here.
    [Area("SystemManage")]
    public class SysStaffController : OrgControllerBase
    {
        public IActionResult Selector()
        {
            ViewBag.PageName = "Chọn nhân viên";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

    }
}
