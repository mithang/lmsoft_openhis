using Microsoft.AspNetCore.Mvc;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
namespace Newtouch.CIS.Web.Controllers
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

        public new IActionResult NotFound()
        {
            return Content("Not Found");
        }

        public IActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}
