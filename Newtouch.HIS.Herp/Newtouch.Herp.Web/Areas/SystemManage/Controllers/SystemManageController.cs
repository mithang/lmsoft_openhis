using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// SystemManage notice controller (Herp .NET 6 port).
    ///
    /// INVESTIGATION RESULT — the original Herp SystemManage area contains NO
    /// Herp controllers at all: only SystemManageAreaRegistration.cs plus 15
    /// views. The registration maps SystemManage/{controller}/{action} to the
    /// namespaces "Newtouch.Herp.Web.Areas.SystemManage.Controllers" AND
    /// "FrameworkBase.MultiOrg.Web.Areas.SystemManage.Controllers". In the
    /// original deployment the second namespace — FrameworkBase BINARY
    /// controllers (his-dll-common, no source) — served all 15 views:
    /// SysConfig, SysLog, SysModule, SysRole, SysStaff, SysUser. The views
    /// themselves contain only generic system-management CRUD (menu tree,
    /// role/user grids, config Name/Code/Value, log grid, staff/user
    /// selectors, password change) and no Herp-specific logic (no warehouse,
    /// product, supplier, purchase or bill fields).
    ///
    /// The .NET 6 SSO portal already owns system management (SysManage area:
    /// SysModule, SysRole, User, Notice + Framework.Web base controllers).
    /// Duplicating generic sys-manage CRUD here would diverge from SSO, so
    /// this controller intentionally returns a Vietnamese notice instead of
    /// a 404 or fake data. The 15 original views are NOT ported as
    /// functional pages, by design.
    ///
    /// Live Herp menu rows that hit these URLs (read-only check of demo DB
    /// NewtouchHIS_herp.Sys_Module, parent "Hệ thống quản lý", all active):
    /// /SystemManage/SysModule/Index, /SystemManage/SysLog/Index,
    /// /SystemManage/SysRole/Index, /SystemManage/SysUser/Index,
    /// /SystemManage/SysConfig/Index. SysStaff/SysUser Selector and
    /// PersonalInfo have no menu rows (dialog helpers only).
    /// </summary>
    [Area("SystemManage")]
    public class SystemManageController : ControllerBase
    {
        private IActionResult Notice(string title, string originalUrl, string description)
        {
            ViewBag.Title = title;
            ViewBag.OriginalUrl = originalUrl;
            ViewBag.Description = description;
            return View("Notice");
        }

        /// <summary>
        /// Quản lý hệ thống — trang tổng quan, liệt kê các nhóm chức năng.
        /// </summary>
        public new IActionResult Index()
        {
            ViewBag.Title = "Quản lý hệ thống";
            return View();
        }

        /// <summary>
        /// Cấu hình hệ thống (trang gốc: /SystemManage/SysConfig/Index).
        /// </summary>
        public IActionResult SysConfig()
        {
            return Notice(
                "Cấu hình hệ thống",
                "/SystemManage/SysConfig/Index",
                "Cấu hình động của hệ thống (tên, mã, giá trị, ghi chú, mẫu cấu hình).");
        }

        /// <summary>
        /// Nhật ký hệ thống (trang gốc: /SystemManage/SysLog/Index).
        /// </summary>
        public IActionResult SysLog()
        {
            return Notice(
                "Nhật ký hệ thống",
                "/SystemManage/SysLog/Index",
                "Nhật ký hoạt động của hệ thống, tra cứu theo tài khoản và khoảng thời gian.");
        }

        /// <summary>
        /// Menu hệ thống (trang gốc: /SystemManage/SysModule/Index).
        /// </summary>
        public IActionResult SysModule()
        {
            return Notice(
                "Menu hệ thống",
                "/SystemManage/SysModule/Index",
                "Cây menu hệ thống (thêm, sửa, xóa menu và phân quyền hiển thị).");
        }

        /// <summary>
        /// Quản lý vai trò (trang gốc: /SystemManage/SysRole/Index).
        /// </summary>
        public IActionResult SysRole()
        {
            return Notice(
                "Quản lý vai trò",
                "/SystemManage/SysRole/Index",
                "Vai trò người dùng và phân quyền vai trò (bao gồm gán người dùng cho vai trò).");
        }

        /// <summary>
        /// Chọn nhân viên (trang gốc: /SystemManage/SysStaff/Selector).
        /// </summary>
        public IActionResult SysStaff()
        {
            return Notice(
                "Chọn nhân viên",
                "/SystemManage/SysStaff/Selector",
                "Cây chọn nhân viên dùng trong các hộp thoại chọn người (không phải trang menu độc lập).");
        }

        /// <summary>
        /// Người dùng hệ thống (trang gốc: /SystemManage/SysUser/Index).
        /// </summary>
        public IActionResult SysUser()
        {
            return Notice(
                "Người dùng hệ thống",
                "/SystemManage/SysUser/Index",
                "Danh sách người dùng hệ thống, gán vai trò cho người dùng và đổi mật khẩu cá nhân.");
        }
    }
}
