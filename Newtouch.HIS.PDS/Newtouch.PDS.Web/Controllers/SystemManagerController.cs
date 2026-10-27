using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 系统管理Controller
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