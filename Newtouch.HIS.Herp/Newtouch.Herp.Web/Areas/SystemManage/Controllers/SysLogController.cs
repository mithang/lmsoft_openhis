using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Legacy URL shim (integration addition). The live Herp menu points at
    /// /SystemManage/SysLog/Index; the .NET 6 port serves this function only
    /// as a notice (see SystemManageController). Redirect instead of 404ing.
    /// </summary>
    [Area("SystemManage")]
    // NOTE: literal Redirect, NOT RedirectToAction: the app registers the
    // non-area default route before the areas route, so RedirectToAction
    // drops the area value and generates /SystemManage/SysX (this controller
    // itself) -> infinite 302 loop. The literal path is unambiguous.
    public class SysLogController : ControllerBase
    {
        public override IActionResult Index() => Redirect("/SystemManage/SystemManage/SysLog");
    }
}
