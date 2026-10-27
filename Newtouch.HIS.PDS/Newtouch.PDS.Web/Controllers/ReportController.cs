using System.Linq;
using Newtouch.Core.Common.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Report Controller
    /// </summary>
    public class ReportController : Controller
    {
        /// <summary>
        /// Trang in đơn
        /// </summary>
        /// <param name="type">1:Phát thuốc nội trú（Chi tiết）,2:Phát thuốc nội trú（Tổng hợp）
        /// 3：Trả thuốc nội trú（Chi tiết），4：Trả thuốc nội trú（Tổng hợp）</param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IActionResult PrintReport(string type, string param)
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
            ViewBag.ReportUnavailable = string.IsNullOrWhiteSpace(ConfigurationHelper.GetAppConfigValue("ReportServer.HOST"));
            return View();
        }
    }
}