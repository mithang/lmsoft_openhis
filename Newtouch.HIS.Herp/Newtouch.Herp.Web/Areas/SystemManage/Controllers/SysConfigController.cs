using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Legacy URL shim (integration addition). The live Herp menu points at
    /// /SystemManage/SysConfig/Index; the .NET 6 port serves this function
    /// only as a notice (see SystemManageController). Redirect instead of
    /// 404ing. TemplateIndex/TemplateForm were config-template dialogs.
    /// </summary>
    [Area("SystemManage")]
    // NOTE: literal Redirect, NOT RedirectToAction: the app registers the
    // non-area default route before the areas route, so RedirectToAction
    // drops the area value and generates /SystemManage/SysX (this controller
    // itself) -> infinite 302 loop. The literal path is unambiguous.
    public class SysConfigController : ControllerBase
    {
        public override IActionResult Index() => Redirect("/SystemManage/SystemManage/SysConfig");
        public override IActionResult Form() => Redirect("/SystemManage/SystemManage/SysConfig");
        public IActionResult TemplateIndex() => Redirect("/SystemManage/SystemManage/SysConfig");
        public IActionResult TemplateForm() => Redirect("/SystemManage/SystemManage/SysConfig");
    }
}
