using NewtouchHIS.Framework.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    [Area("Operation")]
    public class OpFeeReportController : OrgControllerBase
	{
		private readonly IConfiguration _config;
		public OpFeeReportController(IConfiguration config)
		{
			_config = config;
		}
        // GET: Operation/OpFeeReport
  //      public IActionResult Index()
  //      {
  //          return View();
		//}
		public override IActionResult Index()
		{
			ViewBag.ReportServerHOST = _config["ReportServer.HOST"];
			ViewBag.OrgId = OrganizeId;
			return View();
		}

	}
}