using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : OrgControllerBase
    {
        /// <summary>
        /// Phiếu đơn thuốc phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public ActionResult RehabPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phiếu đơn thuốc hạng mục thông thường
        /// </summary>
        /// <returns></returns>
        public ActionResult RegularItemPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phiếu điều trị phục hồi chức năng
        /// </summary>
        /// <returns></returns>
        public ActionResult RehabPresTreatmentReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phiếu đơn thuốc Tây y
        /// </summary>
        /// <returns></returns>
        public ActionResult WMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phiếu đơn thuốc Đông y
        /// </summary>
        /// <returns></returns>
        public ActionResult TCMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phiếu bệnh án
        /// </summary>
        /// <returns></returns>
        public ActionResult MRReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// In y lệnh dài hạn
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintCqyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// In y lệnh tạm thời
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintLsyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// Sau khi thực hiện y lệnh, in phiếu thuốc
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintYzYpReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phiếu yêu cầu xét nghiệm
        /// </summary>
        /// <returns></returns>

        public ActionResult PrintJyReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Phiếu yêu cầu kiểm tra
        public ActionResult PrintJcReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Xếp hạng số tiền sử dụng thuốc kháng sinh toàn viện
        public ActionResult PrintKssJePMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Xếp hạng lượng sử dụng thuốc kháng sinh toàn viện
        public ActionResult PrintKssSlPMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Tỷ lệ đơn thuốc kháng khuẩn ngoại trú
        public ActionResult PrintKssCfPropReport()
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
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải là quản trị viên bệnh viện không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion
    }
}