using Microsoft.AspNetCore.Mvc;
using Newtouch.CIS.API.Envelope;
using Newtouch.CIS.API.Requests;
using Newtouch.CIS.API.Services;

namespace Newtouch.CIS.API.Controllers
{
    /// <summary>
    /// STUB CÓ CHỦ ĐÍCH — KHÔNG CẤP TOKEN.
    ///
    /// Bản gốc (api/Auth/GetToken) chỉ từ chối khi UserId/Account/OrganizeId/
    /// TopOrganizeId trống, sau đó có comment "由request验证出用户身份，需要的时候
    /// 加上验证" (xác minh identity từ request, khi cần thì bổ sung kiểm tra) —
    /// tức là KHÔNG có bước kiểm tra credential nào — rồi tạo UserIdentity từ
    /// thẳng các trường request và SaveIdentity để cấp token Redis thật. Bất kỳ
    /// ai gọi endpoint này cũng nhận được token hợp lệ mà không cần xác thực —
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
                resp.sub_msg = "Endpoint cấp token đã bị vô hiệu hóa vì bản gốc cấp token mà không xác thực credential (chỉ có comment 'khi cần thì bổ sung kiểm tra', identity lấy thẳng từ request). Endpoint này không bao giờ trả token; cần thiết kế lại kèm kiểm tra credential thật trước khi mở lại.";
            }, request);
            return CreateResponse(response);
        }
    }
}
