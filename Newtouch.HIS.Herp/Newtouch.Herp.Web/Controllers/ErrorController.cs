using System.Web.Mvc;

namespace Newtouch.Herp.Web.Controllers
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
        public ActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// 404 Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403 Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}