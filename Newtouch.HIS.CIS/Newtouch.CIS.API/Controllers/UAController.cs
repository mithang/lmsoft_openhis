using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// Port của UAController (api/UA). GetUserInfo dùng token THÔ tra Redis trực tiếp
    /// (gốc gắn [IgnoreTokenDecrypt], không qua bước giải mã token) + gia hạn 120 phút
    /// khi tìm thấy — giữ nguyên semantics, kể cả mã AUTH_FAILED khi thiếu/không khớp.
    /// Controller này KHÔNG gắn [DefaultAuthorize] nên CommonExecute không bắt token;
    /// logic kiểm tra nằm trong action như bản gốc.
    /// </summary>
    [ApiController]
    [Route("api/UA")]
    public class UAController : ApiControllerBase
    {
        private readonly TokenStore _tokenStore;

        public UAController(TokenStore tokenStore)
            : base(tokenStore)
        {
            _tokenStore = tokenStore;
        }

        /// <summary>Đổi access_token lấy Account + OrganizeId.</summary>
        [HttpPost("GetUserInfo")]
        [IgnoreTokenDecrypt]
        public IActionResult GetUserInfo([FromBody] UAGetUserInfoRequest request)
        {
            var response = CommonExecute<UAGetUserInfoRequest, DefaultResponse>((req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(req.access_token)
                    || string.IsNullOrWhiteSpace(req.AppId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    // QA PHASE 2 (API-09): trước đây sub_msg rỗng.
                    resp.sub_msg = "Access_token hoặc AppId không được để trống";
                    return;
                }

                var (account, organizeId) = _tokenStore.GetUaUser(req.access_token);
                if (string.IsNullOrWhiteSpace(account))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    // QA PHASE 2 (API-09): trước đây sub_msg rỗng.
                    resp.sub_msg = "Access_token không hợp lệ hoặc đã hết hạn";
                    return;
                }

                resp.data = new
                {
                    Account = account,
                    OrganizeId = organizeId,
                };
                resp.code = ResponseResultCode.SUCCESS;
            }, request);
            return CreateResponse(response);
        }
    }
}
