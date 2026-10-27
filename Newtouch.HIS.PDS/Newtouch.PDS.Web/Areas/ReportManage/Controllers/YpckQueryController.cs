using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;

using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    [Area("ReportManage")]
    public class YpckQueryController : ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly ISysStaffRepo _sysStaffRepo;

        public IActionResult YpckQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
        public IActionResult AntibioticQuery()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult OutpatientPrescription()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult HospitalPrescription()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult DrugSubtotal()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult AntibioticCFQuery()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult CFQuantity()
        {
            ReportingServiceCom();

            return View();
        }
        public IActionResult YPDrugUsage()
        {
            ViewBag.OrganizeId = OrganizeId;
            ReportingServiceCom();
            return View();
        }
        public IActionResult YPInOutBound()
        {
            ViewBag.OrganizeId = OrganizeId;
            ReportingServiceCom();
            return View();
        }
        /// <summary>
        /// 获取科室
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSysDepartmentList(string keyword=null)
        {
            var data = _sysDepartmentRepo.GetList(OperatorProvider.GetCurrent().OrganizeId, "1", keyword);
            return Json(data);
        }

        public IActionResult GetSysStaffList(string keyword=null)
        {
            List<SysStaffVEntity> list = new List<SysStaffVEntity>();
            list = (List<SysStaffVEntity>)_sysStaffRepo.GetValidStaffListByOrganizeId(this.OrganizeId);
            if (!string.IsNullOrWhiteSpace(keyword))
                list = list.FindAll(p => p.gh == keyword || p.Name == keyword);

            return Json(list);
        }

        private void ReportingServiceCom()
        {

            ViewBag.OrgId = this.OrganizeId;
            ViewBag.topOrgId = Constants.TopOrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //是否是医院管理员
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }


        //药品价格分析表
        public IActionResult YPDrugComparisonTable() {
            ReportingServiceCom();
            return View();
        }

        // 药品入库查询
        public IActionResult YprkQuery()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
        public IActionResult YpckQueryNew()
        {
            ViewBag.OrganizeId = OrganizeId;
            ViewBag.yfbmCode = Constants.CurrentYfbm.yfbmCode;
            return View();
        }
        
        
    }
}