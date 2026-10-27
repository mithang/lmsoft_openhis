using Microsoft.AspNetCore.Mvc;
using Newtouch.MRQC.API.Envelope;
using Newtouch.MRQC.API.Requests;
using Newtouch.MRQC.API.Services;

namespace Newtouch.MRQC.API.Controllers
{
    /// <summary>
    /// STUB CÓ CHỦ ĐÍCH — KHÔNG CẤP TOKEN.
    ///
    /// Bản gốc (api/Auth/GetToken) có bước 1 "xác thực danh tính" chỉ là comment rỗng,
    /// sau đó tạo UserIdentity với toàn bộ trường là "?" (UserId/Account/OrganizeId/
    /// TopOrganizeId) và gọi SaveIdentity để cấp token Redis thật. Tức là bất kỳ ai
    /// gọi endpoint này cũng nhận được token hợp lệ mà không cần xác thực gì —
    /// đó không phải là xác thực, mà là cửa hậu. Port nguyên vẹn sẽ là hồi quy
    /// bảo mật, nên endpoint này luôn trả lỗi tường minh và KHÔNG BAO GIỜ trả token.
    ///
    /// Muốn mở lại: phải gắn kiểm tra credential thật (ví dụ gọi BaseAPI CheckPwd
    /// như LoginBaseController của Web) rồi mới SaveIdentity — việc đó thuộc về
    /// quyết định thiết kế của chủ hệ thống, không tự ý làm trong task port này.
    /// </summary>
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ApiControllerBase
    {
        public AuthController(TokenStore tokenStore)
            : base(tokenStore)
        {
        }

        [HttpPost("GetToken")]
        public IActionResult GetToken([FromBody] AuthGetTokenRequest request)
        {
            var response = CommonExecute<AuthGetTokenRequest, DefaultResponse>((req, resp) =>
            {
                resp.code = ResponseResultCode.FAIL;
                resp.sub_code = "GETTOKEN_DISABLED";
                resp.sub_msg = "Endpoint cấp token đã bị vô hiệu hóa vì bản gốc cấp token mà không xác thực credential (identity toàn là '?'). Endpoint này không bao giờ trả token; cần thiết kế lại kèm kiểm tra credential thật trước khi mở lại.";
            }, request);
            return CreateResponse(response);
        }
    }
}
