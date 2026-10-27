using Microsoft.AspNetCore.Mvc;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// Lỗi hệ thống, trải nghiệm thân thiện
    /// </summary>
    public class ErrorController : Controller
    {
        public IActionResult HttpError()
        {
            return View("Error");
        }

        public IActionResult NotFound()
        {
            return Content("Not Found");
        }

        public IActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}
