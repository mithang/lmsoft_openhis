using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Bất thường gửi gửi Lỗi trang
        /// </summary>
        /// <returns></returns>
        public ActionResult HttpError()
        {
            return View("Error");
        }

        /// <summary>
        /// 404提醒页面
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403提醒页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return Content("Forbidden");
            //return View();
        }
    }
}