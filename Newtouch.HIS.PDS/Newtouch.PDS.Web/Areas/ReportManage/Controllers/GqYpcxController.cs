using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;


namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
	[Area("ReportManage")]
	public class GqYpcxController : ControllerBase
	{
		// GET: ReportManage/GqYpcx
		public IActionResult GqYpcx()
		{
			ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
			ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
		}
        public IActionResult IteamSumQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            return View();
        }

    }
}