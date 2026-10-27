using Microsoft.AspNetCore.Mvc;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// hệthốngsaisai bạntốtthểkiểm
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
