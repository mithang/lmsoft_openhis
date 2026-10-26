using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.MRHomePage.Controllers
{
    public class ReportController : OrgControllerBase
    {
        // GET: MRHomePage/Report
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult BasyReport()
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
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải quản trị viên bệnh viện hay không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion
    }
}