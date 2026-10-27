using Microsoft.AspNetCore.Mvc;
using Newtouch.PDS.API.Envelope;
using Newtouch.PDS.API.Services;
using Newtouch.PDS.Requset;

namespace Newtouch.PDS.API.Controllers
{
    /// <summary>
    /// STUB CÓ CHỦ ĐÍCH — KHÔNG CẤP TOKEN.
    ///
    /// Bản gốc (api/Auth/GetToken) chỉ kiểm tra UserId/Account/OrganizeId/TopOrganizeId
    /// khác rỗng (thiếu thì AUTH_FAILED), sau đó có comment "Giả sử xác thực danh tính
    /// đã thành công / Khi cần thì thêm xác thực" rồi gọi SaveIdentity cấp token Redis
    /// thật — tức là bất kỳ ai gửi đủ 4 trường cũng nhận được token hợp lệ mà không cần
    /// credential gì. Đó không phải là xác thực, mà là cửa hậu; port nguyên vẹn sẽ là
    /// hồi quy bảo mật, nên endpoint này giữ lại đúng bước kiểm tra khác-rỗng của gốc
    /// (thiếu trường -> AUTH_FAILED) rồi luôn trả GETTOKEN_DISABLED và KHÔNG BAO GIỜ
    /// trả token.
    ///
    /// Muốn mở lại: phải gắn kiểm tra credential thật (ví dụ gọi BaseAPI CheckPwd như
    /// LoginBaseController của Web) rồi mới SaveIdentity — việc đó thuộc về quyết định
    /// thiết kế của chủ hệ thống, không tự ý làm trong task port này.
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
                if (string.IsNullOrWhiteSpace(req.UserId)
                    || string.IsNullOrWhiteSpace(req.Account)
                    || string.IsNullOrWhiteSpace(req.OrganizeId)
                    || string.IsNullOrWhiteSpace(req.TopOrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                resp.code = ResponseResultCode.FAIL;
                resp.sub_code = "GETTOKEN_DISABLED";
                resp.sub_msg = "Endpoint cấp token đã bị vô hiệu hóa vì bản gốc cấp token mà chỉ kiểm tra các trường khác rỗng, không xác thực credential (comment gốc: 'giả sử xác thực đã thành công'). Endpoint này không bao giờ trả token; cần thiết kế lại kèm kiểm tra credential thật trước khi mở lại.";
            }, request);
            return CreateResponse(response);
        }
    }
}
