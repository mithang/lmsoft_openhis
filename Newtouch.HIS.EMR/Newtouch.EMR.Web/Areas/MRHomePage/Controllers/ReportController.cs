using NewtouchHIS.Framework.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.EMR.Web.Areas.MRHomePage.Controllers
{
    [Area("MRHomePage")]
    public class ReportController : OrgControllerBase
    {
        // GET: MRHomePage/Report
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult BasyReport()
        {
            ReportingServiceCom();
            return View();
        }

        #region private methods

        /// <summary>
        /// 
        /// </summary>
        private void ReportingServiceCom()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = EmrConfig.TopOrganizeId;
            ViewBag.ReportServerHOST = EmrConfig.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải quản trị viên bệnh viện hay không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion
    }
}