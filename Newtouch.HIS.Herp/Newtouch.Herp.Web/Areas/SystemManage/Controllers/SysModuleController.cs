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
    // NOTE: literal Redirect, NOT RedirectToAction: the app registers the
    // non-area default route before the areas route, so RedirectToAction
    // drops the area value and generates /SystemManage/SysX (this controller
    // itself) -> infinite 302 loop. The literal path is unambiguous.
    public class SysModuleController : ControllerBase
    {
        public override IActionResult Index() => Redirect("/SystemManage/SystemManage/SysModule");
        public override IActionResult Form() => Redirect("/SystemManage/SystemManage/SysModule");
        public IActionResult SyncForm() => Redirect("/SystemManage/SystemManage/SysModule");
    }
}
