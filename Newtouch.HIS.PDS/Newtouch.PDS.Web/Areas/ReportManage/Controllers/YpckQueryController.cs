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
        /// Lấy khoa
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
            ViewBag.IsHospAdministrator = this.UserIdentity.IsHospAdministrator.ToString().ToLower();  //Có phải quản trị viên bệnh viện không
            ViewBag.CurUserCode = this.UserIdentity.UserCode;
            ViewBag.curUsergh = UserIdentity.rygh;
        }


        //Bảng phân tích giá thuốc
        public IActionResult YPDrugComparisonTable() {
            ReportingServiceCom();
            return View();
        }

        // Truy vấn nhập kho thuốc
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