using Microsoft.AspNetCore.Mvc;
using Newtouch.EMR.API.Models;
using Newtouch.EMR.APIRequest;
using Newtouch.EMR.Infrastructure;
using Newtouch.HIS.API.Common;
using StackExchange.Redis;

namespace Newtouch.EMR.API.Controllers
{
    /// <summary>Port of Web API 2 AuthController (GetToken) — as an EXPLICIT-ERROR STUB ONLY.
    /// Reason (parent-track pattern, same as OR.API): the original GetToken minted tokens
    /// for dummy identities (UserId/Account/OrganizeId all "?") WITHOUT any credential
    /// validation — its "verify identity from request" step is empty in source. Porting it
    /// as a real token minter would be a security regression, so this port refuses to
    /// issue tokens and says so explicitly. A real GetToken needs credential validation
    /// against the user store first; that is the one remaining item for EMR.API auth.</summary>
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IConnectionMultiplexer redis) : base(redis)
        {
        }

        /// <summary>换取访问令牌 — not available: see class note.</summary>
        [HttpPost]
        [Route("GetToken")]
        public ContentResult GetToken([FromBody] AuthGetTokenRequest request)
        {
            var resp = new DefaultResponse
            {
                code = APIRequestHelper.ResponseResultCode.FAIL,
                msg = "Chức năng cấp token chưa khả dụng: bản gốc cấp token mà không kiểm tra thông tin đăng nhập, cần bổ sung xác thực tài khoản trước khi đưa vào sử dụng.",
                data = null
            };
            return CreateResponse(resp);
        }
    }
}
