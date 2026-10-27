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
        /// 康复处方单
        /// </summary>
        /// <returns></returns>
        public IActionResult RehabPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 常规项目处方单
        /// </summary>
        /// <returns></returns>
        public IActionResult RegularItemPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 康复治疗单
        /// </summary>
        /// <returns></returns>
        public IActionResult RehabPresTreatmentReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 查询医嘱发药
        /// </summary>
        /// <returns></returns>
        public IActionResult DispensingQuery()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 西药处方单
        /// </summary>
        /// <returns></returns>
        public IActionResult WMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 中药处方单
        /// </summary>
        /// <returns></returns>
        public IActionResult TCMPresReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 病历单
        /// </summary>
        /// <returns></returns>
        public IActionResult MRReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 长期医嘱打印
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintCqyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 临时医嘱打印
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintLsyzReport()
        {
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 医嘱Thực hiện后，打印药品单
        /// </summary>
        /// <returns></returns>
        public IActionResult PrintYzYpReport()
        {
            ReportingServiceCom();
            return View();
        }

        /// <summary>
        /// 检验申请单
        /// </summary>
        /// <returns></returns>

        public IActionResult PrintJyReport()
        {
            ReportingServiceCom();
            return View();
        }
        //检查申请单
        public IActionResult PrintJcReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Toàn viện抗生素药物使用金额排名
        public IActionResult PrintKssJePMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //Toàn viện抗生素药物使用量排名
        public IActionResult PrintKssSlPMReport()
        {
            ReportingServiceCom();
            return View();
        }
        //门诊抗菌药物处方比例
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
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }

        #endregion


        #region Báo cáo工具
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