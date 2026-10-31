using Microsoft.AspNetCore.Mvc;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
namespace Newtouch.CIS.Web.Controllers
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
