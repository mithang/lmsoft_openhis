using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    // Stub (theo mẫu EMR): controller gốc SysRole chỉ có trong nhị phân
    // FrameworkBase.MultiOrg.Web, không có mã nguồn. Trang tương đương trên
    // cổng SSO: /SysManage/SysRole/Index. Không dữ liệu giả, không ghi dữ liệu.
    // Index()/Form() phải dùng override vì BaseController khai báo virtual.
    [Area("SystemManage")]
    public class SysRoleController : OrgControllerBase
    {
        private IActionResult Notice(string pageName)
        {
            ViewBag.PageName = pageName;
            ViewBag.SsoLink = "http://127.0.0.1:5208/SysManage/SysRole/Index";
            ViewBag.SsoLabel = "Vai trò trên cổng SSO";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public override IActionResult Index()
        {
            return Notice("Vai trò hệ thống");
        }

        public override IActionResult Form()
        {
            return Notice("Vai trò hệ thống");
        }
    }
}
