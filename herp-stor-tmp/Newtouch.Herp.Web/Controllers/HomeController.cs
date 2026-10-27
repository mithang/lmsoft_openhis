using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.Infrastructure;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Trang chủ (bản Core: trang đích tối giản + chuyển kho; các tác vụ
    /// "待办/统计/消息提醒" cần IHomeApp và StorageManage sẽ bổ sung cùng khu vực đó.)
    /// </summary>
    public class HomeController : OrgControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        /// <summary>Chuyển kho (trang chọn kho)</summary>
        public IActionResult KfChange()
        {
            ViewBag.KfList = Constants.CurrentKf.kfList;
            return View();
        }

        /// <summary>Chuyển kho hiện tại</summary>
        public IActionResult SwithKf(string kfId, string kfName, int kfLeve)
        {
            var opr = Constants.CurrentKf;
            opr.currentKfId = kfId;
            opr.currentKfName = kfName;
            opr.currentKfLevel = kfLeve;
            Constants.SetCurrentKf(opr.userId, opr);
            return Success("Chuyển kho thành công");
        }
    }
}
