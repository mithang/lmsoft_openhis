using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Newtouch.OR.ManageSystem.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class ReportController : OrgControllerBase
	{
		private readonly IConfiguration _config;
		public ReportController(IConfiguration config)
		{
			_config = config;
		}
        // GET: ReportManage/Report
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult OperationNoticeReport(ReportReqDto req)
        {
            ViewBag.ReportServerHOST = _config["ReportServer.HOST"];
            ViewBag.OrgId = OrganizeId;
            ViewBag.zyh = req.zyh;
            ViewBag.applyno = req.applyno;
            return View();
        }
    }
}