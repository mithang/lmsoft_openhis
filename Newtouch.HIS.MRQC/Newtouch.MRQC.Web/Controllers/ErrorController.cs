using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// lỗi hệ thống trải nghiệm thân thiện
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// gửi ngoại lệ trang lỗi
        /// </summary>
        /// <returns></returns>
        public ActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// 404 trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403 trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}