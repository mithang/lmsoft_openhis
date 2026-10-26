using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base.Model;

namespace HIS.BaseAPI.Controllers
{
    /// <summary>
    /// người dùng hệ thống
    /// </summary>
    [Route("api/sysuser")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserDmnService _sysUserDmn;
        public SysUserController(ISysUserDmnService sysUserDmn)
        {
            _sysUserDmn = sysUserDmn;
        }
        /// <summary>
        /// lấy người dùng hệ thống（phân trang）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSysUserPage")]
        public async Task<BusResult<PageResponseRow<List<SysUserVO>>>> GetPagintionUserListAsync(Request<OLPagination<QueryParamsBase>> request)
        {
            var data = await _sysUserDmn.GetPagintionUserList(request.Data, request.OrganizeId ?? request.Data?.queryParams?.orgId);
            return new BusResult<PageResponseRow<List<SysUserVO>>> { code = ResponseResultCode.SUCCESS, Data = data };
        }
        /// <summary>
        /// cây người dùng tổ chức（phân nhóm khoa）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrgUserTreeWithDept")]
        public async Task<BusResult<List<BsTreeSelectExtDataModel>>> GetOrgUserTreeWithDeptAsync(Request<QueryParamsBase> request)
        {
            var data = await _sysUserDmn.GetOrgUserTreeWithDeptAsync(request.OrganizeId ,request.Data?.keyword,request.Data?.stringList);
            return new BusResult<List<BsTreeSelectExtDataModel>> { code = ResponseResultCode.SUCCESS, Data = data };
        }


    }
}
