using System;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Init;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Infrastructure;
using Newtouch.MRMS.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.MRMS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class WardReportController : OrgControllerBase
	{
		// GET: ReportManage/WardReport
		//public override IActionResult Index()
		//{
		//    return View();
		//}
		public override IActionResult Index()
		{
			ViewBag.ReportServerHOST = AppSettings.GetConfig("ReportServer.HOST");
			ViewBag.OrgId = OrganizeId;
			return View();
		}
	}
}