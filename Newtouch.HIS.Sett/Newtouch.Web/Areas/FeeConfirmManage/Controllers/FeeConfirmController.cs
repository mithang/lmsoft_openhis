using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.FeeConfirmManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FeeConfirmController : Controller
    {
        // GET: FeeConfirmManage/FeeConfirm
        /// <summary>
        /// Xác nhận thu phí
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Hoàn phí đơn thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult Refund()
        {
            return View();
        }

        /// <summary>
        /// Trang danh sách đơn thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult PrescriptionForm()
        {
            return View();
        }
    }
}