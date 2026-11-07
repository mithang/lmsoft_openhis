using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Quản lý hệ thống Controller
    /// </summary>
    public class SystemManagerController : Controller
    {
        // GET: SystemManager
        public IActionResult Index()
        {
            return View();
        }
    }
}