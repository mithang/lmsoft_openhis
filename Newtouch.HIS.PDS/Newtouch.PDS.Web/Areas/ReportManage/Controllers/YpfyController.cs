using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class YpfyController : ControllerBase
    {
        private readonly IMedicineDmnService _medicineDmnService;
        // GET: ReportManage/Ypfy
        //药品发药统计
        public IActionResult Index()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        public IActionResult ypfyQuery()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// 根据关键字查询药品分类
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public JsonResult GetMedicineClassification(string keyword)
        {
            var list = _medicineDmnService.GetMedicineClassificationList(keyword);
            return Json(list);
        }
    }
}