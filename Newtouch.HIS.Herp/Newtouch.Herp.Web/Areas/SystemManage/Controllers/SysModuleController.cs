using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Legacy URL shim (integration addition). The live Herp menu points at
    /// /SystemManage/SysModule/Index, but the .NET 6 port serves this function
    /// only as a notice (see SystemManageController): the original SysModule
    /// pages were served by FrameworkBase binary controllers, and SSO owns
    /// system management. Redirect to the notice instead of 404ing.
    /// </summary>
    [Area("SystemManage")]
    public class SysModuleController : ControllerBase
    {
        public override IActionResult Index() => RedirectToAction("SysModule", "SystemManage");
        public override IActionResult Form() => RedirectToAction("SysModule", "SystemManage");
        public IActionResult SyncForm() => RedirectToAction("SysModule", "SystemManage");
    }
}
