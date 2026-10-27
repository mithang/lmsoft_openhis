using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Legacy URL shim (integration addition). The live Herp menu points at
    /// /SystemManage/SysLog/Index; the .NET 6 port serves this function only
    /// as a notice (see SystemManageController). Redirect instead of 404ing.
    /// </summary>
    [Area("SystemManage")]
    public class SysLogController : ControllerBase
    {
        public override IActionResult Index() => RedirectToAction("SysLog", "SystemManage");
    }
}
