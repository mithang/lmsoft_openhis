using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// giao diện dịch vụ cơ bản của tổ chức
    /// </summary>
    [Route("api/organize")]
    [ApiController]
    public class OrgServiceController : ControllerBase
    {
        private readonly ISysOrgDmnService _sysOrgVDmn;
        private readonly ISysUserStaffDutyDmnService _sysUserStaffDutyDmnService;
        private readonly ISysDepartmentDmnService _sysDepartmentDmnService;
        private readonly ISysWardDmnService _sysWardDmnService;
        private readonly ISysChargeCategoryDmnService _sysChargeCategoryDmnService;
        private readonly ISysPatBasicInfoAppDmnService _sysPatBasicInfoAppDmnService;
        private readonly ISysFailedCodeMessageMappDmnService _sysFailedCodeMessageMappDmnService;
        public OrgServiceController(ISysOrgDmnService sysOrgVDmn, ISysUserStaffDutyDmnService sysUserStaffDutyDmnService, ISysDepartmentDmnService sysDepartmentDmnService, ISysWardDmnService sysWardDmnService, ISysChargeCategoryDmnService sysChargeCategoryDmnService, ISysPatBasicInfoAppDmnService sysPatBasicInfoAppDmnService, ISysFailedCodeMessageMappDmnService sysFailedCodeMessageMappDmnService)
        {
            _sysOrgVDmn = sysOrgVDmn;
            _sysUserStaffDutyDmnService = sysUserStaffDutyDmnService;
            _sysDepartmentDmnService = sysDepartmentDmnService;
            _sysWardDmnService = sysWardDmnService;
            _sysChargeCategoryDmnService = sysChargeCategoryDmnService;
            _sysPatBasicInfoAppDmnService = sysPatBasicInfoAppDmnService;
            _sysFailedCodeMessageMappDmnService = sysFailedCodeMessageMappDmnService;
        }
        /// <summary>
        /// danh sách tổ chức（Toporganizethuộc về）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("OrgList")]
        public async Task<BusResult<List<SysOrgVo>>> GetOrgListAsync(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysOrgVo>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var orgList = await _sysOrgVDmn.GetOrganizeList(null);
            return new BusResult<List<SysOrgVo>> { code = ResponseResultCode.SUCCESS, Data = orgList };
        }
        /// <summary>
        /// quan hệ liên kết nhân viên - vị trí
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysStaffDutyList")]
        public async Task<BusResult<List<SysStaffDutyComplexVEntity>>> GetsysStaffDutyList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysStaffDutyComplexVEntity>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysStaffDutyList = await _sysUserStaffDutyDmnService.GetStaffDutyListByOrganizeId(request.OrganizeId);
            return new BusResult<List<SysStaffDutyComplexVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysStaffDutyList };
        }
        /// <summary>
        /// lấy khoa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysDepartList")]
        public async Task<BusResult<List<SysDepartmentEntity>>> GetsysDepartList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysDepartmentEntity>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysDepartList = await _sysDepartmentDmnService.GetList(request.OrganizeId);
            return new BusResult<List<SysDepartmentEntity>> { code = ResponseResultCode.SUCCESS, Data = sysDepartList };
        }
        /// <summary>
        /// lấy thông tin khu bệnh
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysPatiAreaList")]
        public async Task<BusResult<List<SysWardVEntity>>> GetsysPatiAreaList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysWardVEntity>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysPatiAreaList = await _sysWardDmnService.GetbqList(request.OrganizeId);
            return new BusResult<List<SysWardVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysPatiAreaList };
        }
        /// <summary>
        /// lấy thông tin nhóm lớn thu phí
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysMajorClassList")]
        public async Task<BusResult<List<SysChargeCategoryVEntity>>> GetsysMajorClassList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysChargeCategoryVEntity>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysMajorClassList = await _sysChargeCategoryDmnService.GetList(request.OrganizeId);
            return new BusResult<List<SysChargeCategoryVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysMajorClassList };
        }
        /// <summary>
        /// ghi sổ nằm viện lấy bác sĩ ngoại trú
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetStaffByDutyCode")]
        public async Task<BusResult<List<SysDutyStaffVO>>> GetStaffByDutyCode(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysDutyStaffVO>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var doctorInHosBookkeep = await _sysUserStaffDutyDmnService.GetStaffByDutyCode(request.OrganizeId);
            return new BusResult<List<SysDutyStaffVO>> { code = ResponseResultCode.SUCCESS, Data = doctorInHosBookkeep };
        }
        /// <summary>
        /// tính chất bệnh nhân,chính sách hoàn trả
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysPatientNatureList")]
        public async Task<BusResult<List<SysPatientNatureEntity>>> GetsysPatientNatureList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysPatientNatureEntity>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysPatientNatureList = await _sysPatBasicInfoAppDmnService.GetBRXZListAsync(request.OrganizeId);
            return new BusResult<List<SysPatientNatureEntity>> { code = ResponseResultCode.SUCCESS, Data = sysPatientNatureList };
        }
        /// <summary>
        /// lấy khu bệnh theo khoa
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysWardDeptRelation")]
        public async Task<BusResult<List<SysDepartmentWardRelationVO>>> GetsysWardDeptRelation(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysDepartmentWardRelationVO>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysPatientNatureList = await _sysWardDmnService.GetWardbyDept(request.OrganizeId, null, null);
            return new BusResult<List<SysDepartmentWardRelationVO>> { code = ResponseResultCode.SUCCESS, Data = sysPatientNatureList };
        }
        /// <summary>
        /// lấy thông tin báo lỗi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSysFailedCodeMessageMapList")]
        public async Task<BusResult<List<SysFailedCodeMessageMappEntity>>> GetSysFailedCodeMessageMapList(Request<string> request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysFailedCodeMessageMappEntity>> { code = ResponseResultCode.FAIL, msg = "Thông tin cơ sở (OrganizeId) không được để trống" };
            }
            var sysPatientNatureList = await _sysFailedCodeMessageMappDmnService.GetList(request.OrganizeId);
            return new BusResult<List<SysFailedCodeMessageMappEntity>> { code = ResponseResultCode.SUCCESS, Data = sysPatientNatureList };
        }

    }
}
