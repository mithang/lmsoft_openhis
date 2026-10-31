using HIS.BaseAPI.Models.System;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Base.Domain.ValueObjects.SysManage;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// dùng chung cho hệ thốngAPI 
    /// (liên quan Entity CRUD)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SystemedController : ControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmn;
        public SystemedController(ISysModuleDmnService sysModuleDmn)
        {
            _sysModuleDmn = sysModuleDmn;
        }
        #region Menu        
        /// <summary>
        /// chi tiết menu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetMenubyId")]
        [HttpPost]
        public async Task<BusResult<SysModuleEntity>> GetMenubyIdAsync(Request<string> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null)
            {
                return new BusResult<SysModuleEntity> { code = ResponseResultCode.FAIL, msg = "Id cơ sở và Id menu không được để trống" };
            }
            var menu = await _sysModuleDmn.GetEntity(request.Data);
            if (menu == null)
            {
                return new BusResult<SysModuleEntity> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy menu liên quan" };
            }
            return new BusResult<SysModuleEntity> { code = ResponseResultCode.SUCCESS, Data = menu };
        }
        #region cud
        /// <summary>
        /// thêm menu
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("AddMenu")]
        [HttpPost]
        public async Task<BusResult<string>> AddMenuAsync(Request<MenuAddRequest> request)
        {
            if (string.IsNullOrWhiteSpace(request.OrganizeId) || request.Data == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "Id cơ sở và thông tin menu không được để trống" };
            }
            return await _sysModuleDmn.AddEntity(request.Data, request.Data.user, request.OrganizeId);
        }
        #endregion
        #endregion
 
    }
}
