using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IRepository.InsuranceManage;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using System.Collections.Generic;
using System;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [HandlerLogin]
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = this.GetMenuList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            return null;
        }

        private readonly ISysPatBasicInfoApp _sysPatBasicInfoApp;
        private readonly ISysForCashPayApp _sysForCashPayApp;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IBookkeepInHosDmnService _BookkeepInHosDmnService;
        private readonly ISysWardRepo _sysWardRepo;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        private readonly ISysNationalityRepo _sysNationalityRepo;
        private readonly ISysNationRepo _sysNationRepo;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ICommercialInsuranceRepo _commercialInsuranceRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;
        private readonly ICommonDmnService _commonDmnService;

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAsyncClientsDataJson()
        {
            var opr = OperatorProvider.GetCurrent();
            object sysDepartList = null, sysPatiAreaList = null, sysNationalityList = null;
            object sysNationList = null, sysMajorClassList = null;
            object sysStaffDutyList = null, doctorInHosBookkeep = null, sysPatientNatureList = null;
            object commercialInsuranceList = null;
            object sysWardDeptRelation = null;
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                sysStaffDutyList = _sysUserDmnService.GetStaffDutyListByOrganizeId(opr.OrganizeId);//（Bác sĩ，Huấn luyện viên sức khỏe）
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId, "1");//Khoa
                sysPatiAreaList = _sysWardRepo.GetbqList(opr.OrganizeId);//Khu bệnh
                sysMajorClassList = _sysChargeCategoryRepo.GetList(opr.OrganizeId, zt: "1");//Nhóm lớn
                doctorInHosBookkeep = _sysUserDmnService.GetStaffByDutyCode(opr.OrganizeId, "Doctor");//Nhập viện bác sĩ ngoại trú lấy ghi sổ
                sysPatientNatureList = _sysPatBasicInfoApp.GetBRXZList(opr.OrganizeId);//Tính chất bệnh nhân,Chính sách hoàn trả
                commercialInsuranceList = _commercialInsuranceRepo.GetList(opr.OrganizeId);//Bảo hiểm thương mạilist
                sysWardDeptRelation = _commonDmnService.GetWardbyDept(opr.OrganizeId,null,null);
            }
            sysNationalityList = _sysNationalityRepo.GetgjList();//Quốc tịch
            sysNationList = _sysNationRepo.GetmzList();//Dân tộc

            //
            //Tháng năm
            var yearArr = new List<int>();
            var monthArr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var yearMin = DateTime.Now.Year - 5;
            var yearMax = DateTime.Now.Year + 1;
            var configVal = _sysConfigRepo.GetValueByCode("Hosp_Year_Start", opr.OrganizeId);
            if (!int.TryParse(configVal, out yearMin))
            {
                yearMin = DateTime.Now.Year - 5;
            }
            for (var i = yearMin; i <= yearMax; i++)
            {
                yearArr.Add(i);
            }
         
               var data = new
                {
                    sysPatientNatureList = sysPatientNatureList,
                    SysForCashPayList = _sysForCashPayApp.GetCashPay(), //Lấy phương thức thanh toán tiền mặt
                    sysStaffDutyList = sysStaffDutyList == null ? null : sysStaffDutyList.ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//Huấn luyện viên sức khỏe，Bác sĩ
                    sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks", "mzzybz" }),    //Khoa
                    sysPatiAreaList = sysPatiAreaList == null ? null : sysPatiAreaList.ToJson(new[] { "bqCode", "bqmc", "py" }),  //Khu bệnh
                   sysWardDeptRelation = sysWardDeptRelation == null ? null : sysWardDeptRelation.ToJson(),  //Khu bệnh
                   sysNationalityList = sysNationalityList == null ? null : sysNationalityList.ToJson(new[] { "gjCode", "gjmc", "py" }),   //Quốc tịch
                    sysNationList = sysNationList == null ? null : sysNationList.ToJson(new string[] { "mzCode", "mzmc", "py" }),//Dân tộc
                    sysMajorClassList = sysMajorClassList == null ? null : sysMajorClassList.ToJson(new string[] { "dlCode", "dlmc", "py" }),//Nhóm lớn
                    doctorInHosBookkeep = doctorInHosBookkeep,
                    commercialInsuranceList = commercialInsuranceList,//?.ToJson(new[] { "bxCode", "Name", "EnglishName" }),//Bảo hiểm thương mại
                    itemDetails = this.GetItemDetailsList(),
                    enums = this.GetEnumList(typeof(Infrastructure.Constants).Namespace),
                    yearArr = yearArr.ToArray(),
                    monthArr = monthArr.ToArray(),
                    //
                    SysFailedCodeMessageMapList = _sysFailedCodeMessageMappRepo.GetListByOrgId(opr.OrganizeId),
                    CataloguesComparisonList = this.GetCataloguesComparisonList(),
                };
           


            return Content(data.ToJson());
        }

    }
}
