using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// Legacy URL shim (integration addition). The live Herp menu points at
    /// /SystemManage/SysUser/Index; the .NET 6 port serves this function only
    /// as a notice (see SystemManageController). Redirect instead of 404ing.
    /// PersonalInfo/Roles/Selector were dialog helpers in the original, not
    /// menu pages; they redirect to the same notice.
    /// </summary>
    [Area("SystemManage")]
    public class SysUserController : ControllerBase
    {
        public override IActionResult Index() => RedirectToAction("SysUser", "SystemManage");
        public IActionResult PersonalInfo() => RedirectToAction("SysUser", "SystemManage");
        public IActionResult Roles() => RedirectToAction("SysUser", "SystemManage");
        public IActionResult Selector() => RedirectToAction("SysUser", "SystemManage");
    }
}
