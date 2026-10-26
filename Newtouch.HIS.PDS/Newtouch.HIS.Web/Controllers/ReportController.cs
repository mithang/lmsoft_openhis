using System.Linq;
using Newtouch.Core.Common.Utils;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Report Controller
    /// </summary>
    public class ReportController : Controller
    {
        /// <summary>
        /// in đơn trang
        /// </summary>
        /// <param name="type">1:phát thuốc nội trú (chi tiết),2:phát thuốc nội trú (tổng hợp)
        /// 3: trả thuốc nội trú (chi tiết), 4: trả thuốc nội trú (tổng hợp)</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public ActionResult PrintReport(string type, string param)
        {
            var url = "";
            var Params = string.Empty;
            if (!string.IsNullOrEmpty(param))
            {
                var paramArray = param.Split('|');
                Params = paramArray.Where(item => !string.IsNullOrEmpty(item)).Aggregate(Params, (current, item) => current + ("&" + item));
            }
            if (!string.IsNullOrEmpty(type))
            {
                url = "/Pages/ReportViewer.aspx?/Newtouch.HIS.Bill/" + type;
            }
            ViewBag.ReportServerHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST") + url + (string.IsNullOrEmpty(Params) ? "" : Params);
            return View();
        }
    }
}