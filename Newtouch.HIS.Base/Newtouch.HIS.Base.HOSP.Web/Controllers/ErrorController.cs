using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
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
        /// 404Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public IActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public IActionResult Forbidden()
        {
            return Content("Forbidden");
            //return View();
        }
    }
}