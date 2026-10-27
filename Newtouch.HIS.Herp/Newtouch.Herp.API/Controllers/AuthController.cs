using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Requests;

namespace Newtouch.Herp.API.Controllers
{
    /// <summary>
    /// Port of Newtouch.Herp.API.Controllers.AuthController (route api/Auth).
    /// FAITHFULNESS NOTE — read the original before changing this: the original
    /// GetToken does NOT authenticate anybody. Its request type
    /// (AuthGetTokenRequest) is an empty RequestBase subclass — no username,
    /// password, UserId, Account or OrganizeId — and the action skips step 1
    /// (verify identity from the request) entirely,
    /// builds a placeholder identity whose UserId / Account / OrganizeId /
    /// TopOrganizeId are all literally "?", stores it in Redis and returns the
    /// token. Minting that token here would hand out a FAKE token that
    /// authenticates nobody, so this port deliberately does NOT reproduce that
    /// step: it returns an explicit FAIL instead of a token. Real token
    /// issuance requires a verified identity (e.g. checked against BaseAPI or
    /// the 8015 auth-center host, neither of which this host can verify
    /// against today); when that verification lands, mint via ITokenStore.
    /// SaveIdentity (Redis, 20-minute token) and re-issue the token in
    /// resp.data.token with code SUCCESS, as the original shapes it.
    /// </summary>
    [Route("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IUserIdentityResolver<Identity> userIdentityResolver)
            : base(userIdentityResolver)
        {
        }

        /// <summary>Đổi thông tin xác thực lấy mã truy cập (xem ghi chú ở lớp).</summary>
        [Route("GetToken")]
        [HttpPost]
        public IActionResult GetToken([FromBody] AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> action = (req, resp) =>
            {
                resp.code = ResponseResultCode.FAIL;
                resp.sub_code = "AUTH_NOT_IMPLEMENTED";
                resp.sub_msg = "Không thể cấp mã truy cập: yêu cầu gốc không xác thực danh tính người dùng "
                    + "(danh tính trong mã gốc chỉ là giá trị giữ chỗ '?', không kiểm tra thông tin đăng nhập), "
                    + "vì vậy máy chủ này không cấp mã giả. Cần tích hợp xác thực thực tế "
                    + "(ví dụ qua BaseAPI hoặc trung tâm xác thực) trước khi cấp mã.";
            };

            var response = CommonExecute(action, request, nameof(GetToken));
            return CreateResponse(response);
        }
    }
}
