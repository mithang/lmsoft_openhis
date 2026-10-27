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
    public class SysConfigController : ControllerBase
    {
        public override IActionResult Index() => RedirectToAction("SysConfig", "SystemManage");
        public override IActionResult Form() => RedirectToAction("SysConfig", "SystemManage");
        public IActionResult TemplateIndex() => RedirectToAction("SysConfig", "SystemManage");
        public IActionResult TemplateForm() => RedirectToAction("SysConfig", "SystemManage");
    }
}
