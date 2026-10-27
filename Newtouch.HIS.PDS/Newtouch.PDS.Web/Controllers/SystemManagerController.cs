using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Quản lý hệ thốngController
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