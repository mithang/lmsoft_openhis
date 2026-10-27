using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    // Stub (theo mẫu EMR): controller gốc SysUser chỉ có trong nhị phân
    // FrameworkBase.MultiOrg.Web, không có mã nguồn. Index/Roles/Selector có
    // trang tương đương trên cổng SSO (area SysManage); PersonalInfo (thông tin
    // cá nhân/đổi mật khẩu) KHÔNG có trang tương đương nên ghi rõ là chưa có.
    // Không dữ liệu giả, không ghi dữ liệu.
    // Index() phải dùng override vì BaseController khai báo virtual.
    [Area("SystemManage")]
    public class SysUserController : OrgControllerBase
    {
        private IActionResult Notice(string pageName, string ssoLink = null, string ssoLabel = null)
        {
            ViewBag.PageName = pageName;
            ViewBag.SsoLink = ssoLink;
            ViewBag.SsoLabel = ssoLabel;
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public override IActionResult Index()
        {
            return Notice("Người dùng hệ thống",
                "http://127.0.0.1:5208/SysManage/User/Index", "Người dùng trên cổng SSO");
        }

        public IActionResult PersonalInfo()
        {
            return Notice("Thông tin cá nhân");
        }

        public IActionResult Roles()
        {
            return Notice("Vai trò người dùng",
                "http://127.0.0.1:5208/SysManage/User/UserRoles", "Vai trò người dùng trên cổng SSO");
        }

        public IActionResult Selector()
        {
            return Notice("Chọn người dùng",
                "http://127.0.0.1:5208/SysManage/User/Selector", "Chọn người dùng trên cổng SSO");
        }
    }
}
