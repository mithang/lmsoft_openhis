using Newtouch.Core.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Báo cáo
    /// </summary>
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Trang in phiếu
        /// </summary>
        /// <param name="type">1:Phát thuốc nội trú(Chi tiết),2:Phát thuốc nội trú(Tổng hợp)
        /// 3:Trả thuốc nội trú(Chi tiết),4:Trả thuốc nội trú(Tổng hợp)</param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport(string type, string param)
        {
            string Url = "";
            var Params = new StringBuilder();
            if (!string.IsNullOrEmpty(param))
            {
                string[] paramArray = param.Split('|');
                foreach (string item in paramArray)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        Params.Append("&" + item);
                    }
                }
            }
            if (!string.IsNullOrEmpty(type))
            {
                Url = "/Pages/ReportViewer.aspx?/Newtouch.Herp.ReportService/" + type;
            }
            ViewBag.ReportServerHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST") + Url + (string.IsNullOrEmpty(Params.ToString()) ? "" : Params.ToString());
            return View();
        }
    }
}