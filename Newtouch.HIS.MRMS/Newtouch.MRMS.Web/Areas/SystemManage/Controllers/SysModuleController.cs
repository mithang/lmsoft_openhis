using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    // Stub (theo mẫu EMR): controller gốc SysModule chỉ có trong nhị phân
    // FrameworkBase.MultiOrg.Web, không có mã nguồn. Trang tương đương trên
    // cổng SSO: /SysManage/SysModule/Index. Không dữ liệu giả, không ghi dữ liệu.
    // Index()/Form() phải dùng override vì BaseController khai báo virtual.
    [Area("SystemManage")]
    public class SysModuleController : OrgControllerBase
    {
        private IActionResult Notice(string pageName)
        {
            ViewBag.PageName = pageName;
            ViewBag.SsoLink = "http://127.0.0.1:5208/SysManage/SysModule/Index";
            ViewBag.SsoLabel = "Module trên cổng SSO";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public override IActionResult Index()
        {
            return Notice("Module hệ thống");
        }

        public override IActionResult Form()
        {
            return Notice("Module hệ thống");
        }
    }
}
