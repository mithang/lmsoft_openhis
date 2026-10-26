using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportController : OrgControllerBase
    {
        private readonly IReportDmnService reportDmnService;
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
        /// Tra cứu phát thuốc y lệnh
        /// </summary>
        /// <returns></returns>
        public ActionResult DispensingQuery()
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


        #region 报表工具
        public ActionResult ReportConfig()
        {
            return View();
        }
        public ActionResult ReportConfigForm()
        {
            return View();
        }
        public ActionResult PrintReport(string rpt)
        {
            ViewBag.rptCode = rpt;
            ViewBag.user = this.UserIdentity.rygh;
            ViewBag.orgId = OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }
        public ActionResult GetReportFormJson(string keyValue)
        {
            var data = reportDmnService.ReportConfInfo(OrganizeId, keyValue);            
            return Content(data.ToJson());
        }
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = reportDmnService.ReportList(pagination,OrganizeId,null,keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        public ActionResult ReportEdit(ReportConfigVO vo,string keyValue)
        {
            vo.OrganizeId = OrganizeId;
            reportDmnService.ReportAdd(vo,keyValue, this.UserIdentity.rygh);
            return Success();
        }
        public ActionResult ReportDelete(string keyValue)
        {
            reportDmnService.ReportDelete(keyValue, this.UserIdentity.rygh, OrganizeId);
            return Success();
        }

        #endregion
    }
}