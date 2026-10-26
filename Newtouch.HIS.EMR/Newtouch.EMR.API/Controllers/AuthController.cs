using Autofac;
using Newtouch.EMR.API.Models;
using Newtouch.EMR.APIRequest;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.EMR.API.Controllers
{
    /// <summary>
    /// Ủy quyền và xác thực
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// Đổi thông tin quan trọng (tên người dùng, mật khẩu...) lấy mã truy cập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                //1. Xác minh danh tính người dùng từ request

                //2. Danh tính lưu trong Redis (có thời gian hết hạn nhất định)
                var identity = new UserIdentity()
                {
                    AppId = request.AppId,
                    TokenType = request.TokenType,
                    UserId = "?",
                    Account = "?",
                    OrganizeId = "?",
                    TopOrganizeId = "?",
                };
                var token = SaveIdentity(identity);

                //3. Trả về key
                resp.data = new
                {
                    token = token
                };
                resp.code = ResponseResultCode.SUCCESS;
            };

            var response = base.CommonExecute(ac, request);

            return base.CreateResponse(response);
        }

    }
}