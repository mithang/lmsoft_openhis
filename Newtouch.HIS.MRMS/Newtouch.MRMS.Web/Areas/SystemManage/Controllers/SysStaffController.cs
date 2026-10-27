using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.MRMS.Web.Areas.SystemManage.Controllers
{
    // Stub (theo mẫu EMR): controller gốc SysStaff chỉ có trong nhị phân
    // FrameworkBase.MultiOrg.Web, không có mã nguồn. Trang chọn nhân viên KHÔNG
    // có trang tương đương trên cổng SSO nên không gắn liên kết SSO giả.
    // Không dữ liệu giả, không ghi dữ liệu.
    [Area("SystemManage")]
    public class SysStaffController : OrgControllerBase
    {
        public IActionResult Selector()
        {
            ViewBag.PageName = "Chọn nhân viên";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }
    }
}
