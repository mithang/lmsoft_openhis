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

namespace Newtouch.MRMS.Web.Areas.RecordManage.Controllers
{
    [Area("RecordManage")]
    public class MrHospitalReportController : OrgControllerBase
	{
        // GET: RecordManage/MrHospitalReport
   //     public override IActionResult Index()
   //     {
			//return View();
   //     }

		public override IActionResult Index()
		{
			ViewBag.ReportServerHOST = AppSettings.GetConfig("ReportServer.HOST");
			return View();
		}

		public IActionResult getOrgId()
		{
			var data = new
			{
				orgId = OrganizeId
			};
			return Content(data.ToJson());
		}
	}
}