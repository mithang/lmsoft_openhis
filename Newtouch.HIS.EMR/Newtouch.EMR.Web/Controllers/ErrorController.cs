using Microsoft.AspNetCore.Mvc;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Trải nghiệm thân thiện khi lỗi hệ thống
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
