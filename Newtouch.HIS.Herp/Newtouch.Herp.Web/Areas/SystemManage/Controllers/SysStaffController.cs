using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Legacy URL shim (integration addition). SysStaff/Selector was a dialog
    /// helper in the original, not a menu page; the .NET 6 port serves it
    /// only as a notice (see SystemManageController). Redirect instead of
    /// 404ing.
    /// </summary>
    [Area("SystemManage")]
    public class SysStaffController : ControllerBase
    {
        public IActionResult Selector() => RedirectToAction("SysStaff", "SystemManage");
    }
}
