using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// hệ thống quản lý Controller
    /// </summary>
    public class SystemManagerController : Controller
    {
        // GET: SystemManager
        public ActionResult Index()
        {
            return View();
        }
    }
}