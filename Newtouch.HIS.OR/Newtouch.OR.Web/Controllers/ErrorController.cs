using Microsoft.AspNetCore.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Lỗi hệ thống Trải nghiệm thân thiện
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Gửi ngoại lệ Trang lỗi
        /// </summary>
        /// <returns></returns>
        public IActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// 404Trang nhắc
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403Trang nhắc
        /// </summary>
        /// <returns></returns>
        public IActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}