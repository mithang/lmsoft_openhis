using Microsoft.AspNetCore.Mvc;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// Trang chủ (pilot landing page; the original portal frame views depended on
    /// Framework-only helpers and are replaced by a minimal landing view)
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MessageBox()
        {
            return View();
        }
    }
}
