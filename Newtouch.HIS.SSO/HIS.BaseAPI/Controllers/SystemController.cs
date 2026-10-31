using HIS.BaseAPI.Models.System;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.AppManage;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// dùng chung cho hệ thốngAPI
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISysConfigDmnService _sysConfigDmn;
        private readonly ISysNationalityDmnService _sysNationalityDmnService;
        private readonly ISysNationDmnService _sysNationDmnService;
        private readonly ISysForCashPayDmnService _sysForCashPayDmnService;
        private readonly IAppManageDmnService _regAppDmnService;
        private readonly ISysMedicineService _medicineService;
        public SystemController(ISysConfigDmnService sysConfigDmn, IAppManageDmnService regAppDmnService, ISysNationalityDmnService sysNationalityDmnService, ISysNationDmnService sysNationDmnService, ISysForCashPayDmnService sysForCashPayDmnService, ISysMedicineService medicineService)
        {
            _sysConfigDmn = sysConfigDmn;
            _regAppDmnService = regAppDmnService;
            _sysNationalityDmnService = sysNationalityDmnService;
            _sysNationDmnService = sysNationDmnService;
            _sysForCashPayDmnService = sysForCashPayDmnService;
            _medicineService = medicineService;
        }
        #region Menu
        /// <summary>
        /// lấy danh sách menu hệ thống người dùng(thư viện chính)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMenuList")]
        [HttpPost]
        public async Task<BusResult<List<SysModuleVO>>> GetMenuListAsync(Request<MenuAuthRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || (request.Data != null && request.Data.RoleIdList.Count == 0))
            {
                return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.FAIL, msg = "Id cơ sở và thông tin vai trò không được để trống" };
            }
            var list = await _sysConfigDmn.GetMenuList(request.OrganizeId, request.Data.RoleIdList, request.Data.IsRoot, request.Data.IsAdministrator, request.Data.UserId, request.Data.ValidLimit);
            return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.SUCCESS, Data = list != null ? list.Adapt<List<SysModuleVO>>() : null };
        }
        /// <summary>
        /// lấy danh sách menu người dùng hệ thống nghiệp vụ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetAppMenuList")]
        [HttpPost]
        public async Task<BusResult<List<SysModuleVO>>> GetAppMenuListAsync(Request<MenuAuthRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null)
            {
                return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.FAILOfEmpty, msg = "Id cơ sở, thông tin vai trò không được để trống" };
            }
            if(string.IsNullOrWhiteSpace(request.Data.MenuAppId))
            {
                request.Data.MenuAppId = request.AppId;
            }
            var list = await _sysConfigDmn.GetMenuList(request.Data.MenuAppId, request.OrganizeId, request.Data.RoleIdList, request.Data.IsRoot, request.Data.IsAdministrator, request.Data.UserId, request.Data.ValidLimit);
            return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.SUCCESS, Data = list != null ? list.Adapt<List<SysModuleVO>>() : null };
        }
        /// <summary>
        /// lấy danh sách menu hệ thống ứng dụng(chỉ quản trị viên dùng để đồng bộ)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMenuListbyAppId")]
        [HttpPost]
        public async Task<BusResult<List<SysModuleVO>>> GetMenuListbyDBAsync(Request<MenuAuthRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null || string.IsNullOrWhiteSpace(request.Data.UserId))
            {
                return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.FAILOfEmpty, msg = "Id cơ sở, Id ứng dụng menu, Id người dùng không được để trống" };
            }
            var list = await _sysConfigDmn.GetMenuListbyAppId(request.OrganizeId, request.Data.MenuAppId ?? request.AppId, request.Data.UserId, request.Data.ValidLimit);
            return new BusResult<List<SysModuleVO>> { code = ResponseResultCode.SUCCESS, Data = list != null ? list.Adapt<List<SysModuleVO>>() : null };
        }

        #endregion
        /// <summary>
        /// bảng từ điển hệ thống
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetItemDetailsList")]
        [HttpPost]
        public async Task<BusResult<List<SysItemExtendVO>>> GetItemDetailsListAsync(Request<string> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId))
            {
                return new BusResult<List<SysItemExtendVO>> { code = ResponseResultCode.FAIL, msg = "Id cơ sở và thông tin vai trò không được để trống" };
            }
            var list = await _sysConfigDmn.GetItemDetailsList(request.OrganizeId);
            return new BusResult<List<SysItemExtendVO>> { code = ResponseResultCode.SUCCESS, Data = list };

        }
        /// <summary>
        /// lấy quốc tịch
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysNationalityList")]
        public async Task<BusResult<List<SysNationalityVEntity>>> GetsysNationalityList(Request<string> request)
        {
            var sysNationalityList = await _sysNationalityDmnService.GetgjList();
            return new BusResult<List<SysNationalityVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysNationalityList };
        }
        /// <summary>
        /// lấy dân tộc
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetsysNationList")]
        public async Task<BusResult<List<SysNationVEntity>>> GetsysNationList()
        {
            var sysNationList = await _sysNationDmnService.GetmzList();
            return new BusResult<List<SysNationVEntity>> { code = ResponseResultCode.SUCCESS, Data = sysNationList };
        }
        /// <summary>
        /// lấy phương thức thanh toán tiền mặt
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSysForCashPayList")]
        public async Task<BusResult<List<SysCashPaymentModelEntity>>> GetSysForCashPayList()
        {
            List<SysCashPaymentModelEntity> sysCashPaymentModelEntity = await _sysForCashPayDmnService.GetList();
            return new BusResult<List<SysCashPaymentModelEntity>> { code = ResponseResultCode.SUCCESS, Data = sysCashPaymentModelEntity };
        }
        /// <summary>
        /// lấy danh sách ứng dụng đã đăng ký
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRegAppList")]
        public async Task<BusResult<List<AuthAppVO>>> GetRegAppListAsync(Request<string> request)
        {
            var data = await _regAppDmnService.GetRegAppList();
            return new BusResult<List<AuthAppVO>> { code = ResponseResultCode.SUCCESS, Data = data };
        }

        #region thuốc
        /// <summary>
        /// cách dùng thuốc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost,AllowAnonymous]
        [Route("GetMediUsageList")]
        public async Task<BusResult<List<DrugUsageEntity>>> GetMediUsageListAsync(Request<DrugUsageRequest> request)
        {
            var data = await _medicineService.DrugUsageDic(request.Data?.code,request.Data?.yplx);
            return new BusResult<List<DrugUsageEntity>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// lấy quan hệ đối chiếu dạng bào chế - cách dùng thuốc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        [Route("GetDrugFormulationUsageList")]
        public async Task<BusResult<List<DrugFormulationUsageVO>>> GetDrugFormulationUsageListAsync(Request<string> request)
        {
            var data = await _medicineService.GetDrugFormulationUsageList();
            return new BusResult<List<DrugFormulationUsageVO>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        #endregion

    }
}
