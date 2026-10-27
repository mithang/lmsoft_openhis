using Newtouch.CIS.Compat;
using Newtouch.CIS.Web.Attributes;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Application.Interface;
using Newtouch.Domain.Entity;
using Newtouch.CIS.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.IDomainServices;
using System.Collections.Generic;
using System;
using Newtouch.Domain.IRepository;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Home/Indextảikhi Mặc địnhtảicủa chậmtồn tạiDữ liệu
    /// </summary>
    public class ClientsDataController : ClientsDataBaseController
    {
        private readonly IBaseDataDmnService _baseDataDmnService;

        private readonly ISysUserDmnService _sysUserDmnService;
        /// <summary>
        /// dântộc
        /// </summary>
        private readonly ISysNationRepo _sysNationRepo;
        /// <summary>
        /// quốc tịch
        /// </summary>
        private readonly ISysNationalityRepo _sysNationalityRepo;

        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        public ClientsDataController(NewtouchHIS.Framework.Web.Implementation.ISystemAppService systemApp, NewtouchHIS.Base.Domain.IDomainService.ISysConfigDmnService sysConfigDmn, IBaseDataDmnService baseDataDmnService, ISysUserDmnService sysUserDmnService, ISysNationRepo sysNationRepo, ISysNationalityRepo sysNationalityRepo, ISysDepartmentRepo sysDepartmentRepo)
            : base(systemApp, sysConfigDmn)
        {
            this._baseDataDmnService = baseDataDmnService;
            this._sysUserDmnService = sysUserDmnService;
            this._sysNationRepo = sysNationRepo;
            this._sysNationalityRepo = sysNationalityRepo;
            this._sysDepartmentRepo = sysDepartmentRepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async System.Threading.Tasks.Task<IActionResult> GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = await this.GetMenuList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetClientsDataJson()
        {
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                enums = this.GetEnumList(typeof(Newtouch.Infrastructure.Constants).Namespace),
                ypjxyfdy = _baseDataDmnService.GetMediFormlUsageList(),
                ypyf = _baseDataDmnService.GetMediUsageList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAsyncClientsDataJson()
        {
            var opr = OperatorProvider.GetCurrent();
            object sysStaffDutyList = null, doctorInHosBookkeep = null, sysPatientNatureList = null,sysDepartList = null;
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                sysStaffDutyList = _sysUserDmnService.GetStaffDutyListByOrganizeId(opr.OrganizeId);//（Bác sĩ，sức khỏehuấn luyện viên）
                doctorInHosBookkeep = _sysUserDmnService.GetStaffByDutyCode(opr.OrganizeId, "Doctor");//Ghi nợ nội trúLấyNgoại trúBác sĩ
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId, "1");//Khoa
                sysPatientNatureList = _sysNationalityRepo.GetBRXZList(opr.OrganizeId);//Tính chất bệnh nhân,báobánchính sách
            }

            var data = new
            {
                sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks" }),    //Khoa
                sysPatientNatureList = sysPatientNatureList,
                sysStaffDutyList = sysStaffDutyList == null ? null : sysStaffDutyList.ToJson(new[] { "StaffGh", "StaffName", "StaffPY", "DutyCode" }),//sức khỏehuấn luyện viên，Bác sĩ
                sysNationList = _sysNationRepo.GetmzList(),//dântộc
                SysForCashPayList = _sysNationalityRepo.GetCashPay(), //LấyhiệnkimPhương thức thanh toán
                sysNationalityList = _sysNationalityRepo.GetgjList()//quốc tịch
        };
            return Content(data.ToJson());
        }

    }
}
