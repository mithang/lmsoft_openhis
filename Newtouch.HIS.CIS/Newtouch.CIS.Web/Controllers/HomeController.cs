using Microsoft.AspNetCore.Mvc;

using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// CIS landing page (the original portal frame views depended on Framework-only
    /// helpers; this is a minimal landing page linking to each Area's main page).
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
