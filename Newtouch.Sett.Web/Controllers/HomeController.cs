using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// đầutrang (pilot landing page; the original portal frame views depended on
    /// Framework-only helpers and are replaced by a minimal landing view)
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MessageBox()
        {
            return View();
        }

        /// <summary>
        /// Port of the original HomeController.GetNewFieldUniqueValue (parity audit
        /// 2026-10-27). Called live by SystemManage/SysParameterConfig/
        /// ChargeTemplate_EditForm. Deliberately NO [HandlerLogin] here: that
        /// attribute means "skip login verification" in this framework, and the
        /// original action was login-checked — the global filter must enforce
        /// login, as it does for Home/Index (unauthenticated -> 302).
        /// WARNING: the underlying spFieldGetNewID stored procedure ADVANCES a
        /// serial-number counter (INSERT/UPDATE EntityFieldSerialNo) — calling this
        /// endpoint is a write, so it is ported but NOT live-invoked in this audit.
        /// </summary>
        public IActionResult GetNewFieldUniqueValue(string fieldName, string orgId)
        {
            var data = SettKeyHelper.GetNewFieldUniqueValue(fieldName, orgId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "", data = data }.ToJson());
        }
    }
}
