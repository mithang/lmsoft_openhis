using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>授权认证</summary>
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IAccessAuthDmnService _accessAuthDmnService;

        public AuthController(TokenStore tokenStore, IAccessAuthDmnService accessAuthDmnService)
            : base(tokenStore)
        {
            _accessAuthDmnService = accessAuthDmnService;
        }

        /// <summary>
        /// 以关键信息换取访问令牌（经代理授权验证）。Gốc gọi Authorized() kiểm tra
        /// quyền truy cập rồi mới cấp token — bản port giữ nguyên bước kiểm tra này.
        /// </summary>
        [HttpPost("GetTokenbyProxy")]
        public ContentResult GetTokenbyProxy([FromBody] AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.AppId)
                    || string.IsNullOrWhiteSpace(request.OrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                var ident = new Newtouch.HIS.Domain.Entity.AccessIdentity();
                ident.AppId = request.AppId;
                ident.OrganizeId = request.OrganizeId;
                ident.Account = request.Account;
                string message = "";
                bool authed = _accessAuthDmnService.Authorized(ident, out message);
                if (authed)
                {
                    var identity = new Identity
                    {
                        AppId = request.AppId,
                        TokenType = request.TokenType,
                        UserId = request.UserId,
                        Account = request.Account,
                        OrganizeId = request.OrganizeId,
                        TopOrganizeId = request.TopOrganizeId,
                    };
                    var token = SaveIdentity(identity);
                    resp.data = new { token = token };
                }
                else
                {
                    resp.sub_msg = message;
                }
                // QUIRK GỐC (giữ nguyên): code vẫn là SUCCESS ngay cả khi
                // Authorized() trả false (chỉ khác ở sub_msg). Không tự ý sửa
                // hành vi này để tránh lệch contract với client cũ.
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }

        /// <summary>
        /// STUB CÓ CHỦ ĐÍCH — KHÔNG cấp token thật: action GetToken gốc mint token
        /// mà KHÔNG kiểm tra bất kỳ credential nào ("giả thiết đã xác thực xong").
        /// Port thành token thật sẽ là lỗ hổng bảo mật (tiền lệ MRMS/OR). Client cần
        /// dùng GetTokenbyProxy (có bước Authorized) để lấy token.
        /// </summary>
        [HttpPost("GetToken")]
        public ContentResult GetToken([FromBody] AuthGetTokenRequest request)
        {
            var response = new DefaultResponse
            {
                code = ResponseResultCode.FAIL,
                sub_code = "GETTOKEN_DISABLED",
                sub_msg = "GetToken đã bị vô hiệu hóa: action gốc cấp token mà không kiểm tra credential. Vui lòng dùng GetTokenbyProxy (có xác thực quyền truy cập) để lấy token.",
            };
            return CreateResponse(response);
        }
    }
}
