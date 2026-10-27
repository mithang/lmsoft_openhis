using AutoMapper;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Areas.System.Controllers
{
    /// <summary>
    /// Tổ chức管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SysOrgController : ControllerBase
    {
        private readonly ISysOrgDmnService _sysOrgVDmn;
        public SysOrgController(ISysOrgDmnService sysOrgVDmn)
        {
            _sysOrgVDmn = sysOrgVDmn; 
        }
        /// <summary>
        /// 获取Tổ chức列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrgList")]
        public async Task<BusResult<List<SysOrgIndexVo>>> GetOrgListAsync(Request<SysOrgVo> request)
        {
            var orgData = await _sysOrgVDmn.GetOrganizeList(request.Data?.TopOrganizeId);
            if (orgData == null)
            {
                return new BusResult<List<SysOrgIndexVo>> { code = ResponseResultCode.FAIL, msg = "Không tìm thấy dữ liệu liên quan" };
            }
            return new BusResult<List<SysOrgIndexVo>> { code = ResponseResultCode.SUCCESS, Data = orgData.Adapt<List<SysOrgIndexVo>>() };
        }



    }
}
