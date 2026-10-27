using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// 系统错误 友好体验
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
