using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    public class YpfyController : ControllerBase
    {
        private readonly IMedicineDmnService _medicineDmnService;
        // GET: ReportManage/Ypfy
        //thuốc phát thuốc thống kê
        public ActionResult Index()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public ActionResult ypfyQuery()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// theo từ khóa tra cứu phân loại thuốc
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetMedicineClassification(string keyword)
        {
            var list = _medicineDmnService.GetMedicineClassificationList(keyword);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}