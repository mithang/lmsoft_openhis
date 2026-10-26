using Autofac;
using Newtouch.MRQC.API.Models;
using Newtouch.MRQC.APIRequest;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.MRQC.API.Controllers
{
    /// <summary>
    /// xác thực ủy quyền
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// bằng thông tin quan trọng（tên người dùng、mật khẩu, v.v.）đổi lấy mã truy cập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                //1、bởirequestxác minh được danh tính người dùng

                //2、danh tính được lưu trữ tạiRedis（có thời gian hết hạn nhất định）
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

                //3、trả vềkey
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