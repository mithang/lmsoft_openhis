using Autofac;
using Newtouch.MR.ManageSystem.APIRequest;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;
using Newtouch.HIS.API.Common.Models;

namespace Newtouch.MR.ManageSystem.API.Controllers
{
    /// <summary>
    /// Xác thực ủy quyền
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// Dùng thông tin quan trọng (tên đăng nhập, mật khẩu, v.v.) để đổi lấy mã truy cập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                //1、Xác thực danh tính người dùng từ request

                //2、Lưu trữ danh tínhtạiRedis（có thời hạn hết hạn nhất địnhthời gian）
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

                //3、Trả vềkey
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