using Newtouch.Tools;
using System.Web.Mvc;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using System.Collections.Generic;
using System;
using Newtouch.Domain.IRepository;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Home/IndexKhi tải được tải mặc định Dữ liệu bộ nhớ đệm
    /// </summary>
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        private readonly IBaseDataDmnService _baseDataDmnService;

        private readonly ISysUserDmnService _sysUserDmnService;
        /// <summary>
        /// Dân tộc
        /// </summary>
        private readonly ISysNationRepo _sysNationRepo;
        /// <summary>
        /// Quốc tịch
        /// </summary>
        private readonly ISysNationalityRepo _sysNationalityRepo;

        private readonly ISysDepartmentRepo _sysDepartmentRepo;

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
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                enums = this.GetEnumList(typeof(Infrastructure.Constants).Namespace),
                ypjxyfdy = _baseDataDmnService.GetMediFormlUsageList(),
                ypyf = _baseDataDmnService.GetMediUsageList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAsyncClientsDataJson()
        {
            var opr = OperatorProvider.GetCurrent();
            object sysStaffDutyList = null, doctorInHosBookkeep = null, sysPatientNatureList = null,sysDepartList = null;
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                sysStaffDutyList = _sysUserDmnService.GetStaffDutyListByOrganizeId(opr.OrganizeId);//(Bác sĩ, huấn luyện viên sức khỏe)
                doctorInHosBookkeep = _sysUserDmnService.GetStaffByDutyCode(opr.OrganizeId, "Doctor");//Lấy bác sĩ ngoại trú cho ghi sổ nhập viện
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId, "1");//Khoa
                sysPatientNatureList = _sysNationalityRepo.GetBRXZList(opr.OrganizeId);//Tính chất bệnh nhân,Chính sách hoàn trả
            }

            var data = new
            {
                sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks" }),    //Khoa
                sysPatientNatureList = sysPatientNatureList,
                sysStaffDutyList = sysStaffDutyList == null ? null : sysStaffDutyList.ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//Huấn luyện viên sức khỏe, bác sĩ
                sysNationList = _sysNationRepo.GetmzList(),//Dân tộc
                SysForCashPayList = _sysNationalityRepo.GetCashPay(), //Lấy phương thức thanh toán tiền mặt
                sysNationalityList = _sysNationalityRepo.GetgjList()//Quốc tịch
        };
            return Content(data.ToJson());
        }

    }
}
