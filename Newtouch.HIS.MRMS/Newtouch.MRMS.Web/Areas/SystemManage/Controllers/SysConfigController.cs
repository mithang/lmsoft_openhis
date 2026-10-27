using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    // Stub (theo mẫu EMR): controller gốc SysConfig chỉ có trong nhị phân
    // FrameworkBase.MultiOrg.Web, không có mã nguồn. Các action trang gốc
    // (Index/Form/TemplateIndex/TemplateForm) trả thông báo tiếng Việt rõ ràng
    // thay vì 404. Tham số hệ thống KHÔNG có trang tương đương trên cổng SSO,
    // nên không gắn liên kết SSO giả. Không dữ liệu giả, không ghi dữ liệu.
    // Index()/Form() phải dùng override vì BaseController khai báo virtual.
    [Area("SystemManage")]
    public class SysConfigController : OrgControllerBase
    {
        private IActionResult Notice(string pageName)
        {
            ViewBag.PageName = pageName;
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public override IActionResult Index()
        {
            return Notice("Tham số hệ thống");
        }

        public override IActionResult Form()
        {
            return Notice("Tham số hệ thống");
        }

        public IActionResult TemplateIndex()
        {
            return Notice("Mẫu tham số hệ thống");
        }

        public IActionResult TemplateForm()
        {
            return Notice("Mẫu tham số hệ thống");
        }
    }
}
