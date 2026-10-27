using NewtouchHIS.Framework.Web.Controllers;

using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using Newtouch.CIS.Repository;
using Newtouch.CIS.Compat;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;
using Newtouch.CIS.Compat;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.CIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("ReportManage")]
    public class ReportController : OrgControllerBase
    {
        private readonly IReportDmnService reportDmnService;
        /// <summary>
        /// Phục hồi chức năngĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RehabPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Hạng mục thông thườngĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult RegularItemPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Phục hồi chức năngĐiều trịphiếu
        /// </summary>
        /// <returns></returns>
        public IActionResult RehabPresTreatmentReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// Tra cứuY lệnhphát thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult DispensingQuery()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// Thuốc TâyĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult WMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Thuốc Đông yĐơn thuốc
        /// </summary>
        /// <returns></returns>
        public IActionResult TCMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Hồ sơphiếu
        /// </summary>
        /// <returns></returns>
        public IActionResult MRReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// Y lệnh dài hạnIn
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintCqyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// Y lệnh tạm thờiIn
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintLsyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// Y lệnhThực hiệnsau，InThuốcphiếu
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintYzYpReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// Xét nghiệmPhiếu
        /// </summary>
        /// <returns></returns>

        public IActionResult PrintJyReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Kiểm traPhiếu
        public IActionResult PrintJcReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Toàn việnKháng sinhthuốcvậtSử dụngSố tiềnxếptên
        public IActionResult PrintKssJePMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Toàn việnKháng sinhthuốcvậtSử dụnglượngxếptên
        public IActionResult PrintKssSlPMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Ngoại trú抗菌thuốcvậtĐơn thuốcsoVD
        public IActionResult PrintKssCfPropReport()
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
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //CóKhôngCóQuản trị viên bệnh viện
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion


        #region Báo cáocôngcụ thể
        public IActionResult ReportConfig()
        {
            return View();
        }
        public IActionResult ReportConfigForm()
        {
            return View();
        }
        public IActionResult PrintReport(string rpt)
        {
            ViewBag.rptCode = rpt;
            ViewBag.user = this.UserIdentity.rygh;
            ViewBag.orgId = OrganizeId;
            ViewBag.ReportServerHOST = Newtouch.CIS.Compat.AppConfig.Get("ReportServer.HOST");
            return View();
        }
        public IActionResult GetReportFormJson(string keyValue)
        {
            var data = reportDmnService.ReportConfInfo(OrganizeId, keyValue);            
            return Content(data.ToJson());
        }
        public IActionResult GetGridJson(Pagination pagination, string keyword)
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
        public IActionResult ReportEdit(ReportConfigVO vo,string keyValue)
        {
            vo.OrganizeId = OrganizeId;
            reportDmnService.ReportAdd(vo,keyValue, this.UserIdentity.rygh);
            return Success();
        }
        public IActionResult ReportDelete(string keyValue)
        {
            reportDmnService.ReportDelete(keyValue, this.UserIdentity.rygh, OrganizeId);
            return Success();
        }

        #endregion
    }
}