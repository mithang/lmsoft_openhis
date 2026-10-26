using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;

namespace HIS.SSO.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : ClientsDataBaseController
    {
        private readonly ISystemAppService _systemApp;
        private readonly IOrganizeAppService _organizeApp;
        public ClientsDataController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn, IOrganizeAppService organizeAppService) : base(systemApp, sysConfigDmn)
        {
            _systemApp = systemApp;
            _organizeApp = organizeAppService;

        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = await this.GetMenuList(),
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetClientsDataJson()
        {
            var yearArr = new List<int>();
            var monthArr = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var yearMin = DateTime.Now.Year - 5;
            var yearMax = DateTime.Now.Year + 1;
            for (var i = yearMin; i <= yearMax; i++)
            {
                yearArr.Add(i);
            }
            var data = new
            {
                sysMajorClassList = await _organizeApp.GetsysMajorClassListAsync(OrganizeId),//nhóm lớn
                doctorInHosBookkeep = await _organizeApp.GetStaffByDutyCodeAsync(OrganizeId),
                commercialInsuranceList = "",//bảo hiểm thương mại（tạm thời chưa có）
                yearArr = yearArr.ToArray(),
                monthArr = monthArr.ToArray(),
                SysFailedCodeMessageMapList = await _organizeApp.GetSysFailedCodeMessageMapListAsync(OrganizeId),
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetAsyncClientsDataJson()
        {
            object sysDepartList = null;
            sysDepartList = await _organizeApp.GetsysDepartListAsync(OrganizeId);
            var data = new
            {
                itemDetails = await _systemApp.GetItemDetailsListAsync(""),
                sysNationalityList = await _systemApp.GetsysNationalityListAsync(""),   //quốc tịch
                sysNationList = await _systemApp.GetsysNationListAsync(""),//dân tộc
                sysPatientNatureList = await _organizeApp.GetsysatientNatureListAsync(OrganizeId),//tính chất bệnh nhân,chính sách hoàn trả
                SysForCashPayList = await _systemApp.GetSysForCashPayAsync(""),//lấy phương thức thanh toán tiền mặt
                sysStaffDutyList = await _organizeApp.GetsysStaffDutyListAsync(OrganizeId),//huấn luyện viên sức khỏe，bác sĩ
                sysDepartList = sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py", "zlks", "mzzybz" }),    //khoa
                sysPatiAreaList = await _organizeApp.GetsysPatiAreaListAsync(OrganizeId),  //khu bệnh
                sysWardDeptRelation = await _organizeApp.GetsysWardDeptRelationAsync(OrganizeId),  //khu bệnh
                ypjxyfdy = await _systemApp.GetDrugFormulationUsageListAsync(),
                ypyf = await _systemApp.GetMediUsageListAsync(),
                enums = await GetEnumList("NewtouchHIS.Base.Domain", "HIS.SSO.Domain"),

            };
            return Content(data.ToJson());
        }
    }
}
