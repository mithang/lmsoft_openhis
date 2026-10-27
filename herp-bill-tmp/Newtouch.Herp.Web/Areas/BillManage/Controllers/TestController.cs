using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Areas.BillManage.Controllers
{
    // Mirrors the original, which inherited plain Controller (not the login
    // base). Kept as-is; the view is a one-line stub in the source.
    [Area("BillManage")]
    public class TestController : Controller
    {
        // GET: BillManage/Test
        public IActionResult Index()
        {
            return View();
        }
    }
}
