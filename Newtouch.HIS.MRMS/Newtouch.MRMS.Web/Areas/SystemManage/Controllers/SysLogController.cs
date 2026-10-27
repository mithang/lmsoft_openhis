using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    // Stub (theo mẫu EMR): controller gốc SysLog chỉ có trong nhị phân
    // FrameworkBase.MultiOrg.Web, không có mã nguồn. Nhật ký hệ thống KHÔNG có
    // trang tương đương trên cổng SSO nên không gắn liên kết SSO giả.
    // Không dữ liệu giả, không ghi dữ liệu.
    // Index() phải dùng override vì BaseController khai báo virtual.
    [Area("SystemManage")]
    public class SysLogController : OrgControllerBase
    {
        public override IActionResult Index()
        {
            ViewBag.PageName = "Nhật ký hệ thống";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }
    }
}
