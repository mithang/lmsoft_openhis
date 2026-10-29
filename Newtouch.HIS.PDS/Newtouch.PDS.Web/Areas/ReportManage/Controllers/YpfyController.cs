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
        //Thống kê phát thuốc
        // FIX gstack T-29: khong co Views/Ypfy/Index.cshtml (chi co
        // ypfyQuery.cshtml, cung set 2 ViewBag nay). Menu DB PDS tro toi
        // ypfyQuery, khong tro toi Index tran. Chuyen huong toi thieu ve
        // ypfyQuery thay vi tao view gia. Dung Redirect literal (xem T-23:
        // route "default" dang ky truoc "areas").
        public override IActionResult Index()
        {
            return Redirect("/ReportManage/Ypfy/ypfyQuery");
        }

        public IActionResult ypfyQuery()
        {
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

        /// <summary>
        /// Truy vấn phân loại thuốc theo từ khóa
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