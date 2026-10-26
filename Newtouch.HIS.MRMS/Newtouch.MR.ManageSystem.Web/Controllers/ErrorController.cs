using System.Web.Mvc;

namespace Newtouch.MR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Lỗi hệ thống, trải nghiệm thân thiện
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
        /// 404Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return Content("Not Found");
            //return View();
        }

        /// <summary>
        /// 403Trang nhắc nhở
        /// </summary>
        /// <returns></returns>
        public ActionResult Forbidden()
        {
            return Content("Forbidden");
        }
    }
}