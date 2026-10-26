using System.Web.Mvc;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// Trang chủ
    /// </summary>
    public class HomeController : FrameworkBase.MultiOrg.Web.Controllers.HomeController
    {
        public ActionResult MessageBox()
        {
            return View();
        }
    }
}