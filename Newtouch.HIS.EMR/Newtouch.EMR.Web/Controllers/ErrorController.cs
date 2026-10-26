using System.Web.Mvc;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Lỗi hệ thống - trải nghiệm thân thiện
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Ngoại lệ gửi đến trang lỗi
        /// </summary>
        /// <returns></returns>
        public ActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// Trang nhắc 404
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// Trang nhắc 403
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}