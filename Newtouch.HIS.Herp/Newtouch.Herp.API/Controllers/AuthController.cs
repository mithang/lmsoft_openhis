using Autofac;
using Newtouch.Herp.API.Models;
using Newtouch.Herp.APIRequest;
using Newtouch.HIS.API.Common;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.Herp.API.Controllers
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
        /// Bằng thông tin quan trọng(Tên người dùng,mật khẩu, v.v.)Đổi lấy mã truy cập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                //1,bởi request Xác minh danh tính người dùng

                //2,Danh tính được lưu trữ tại Redis(Có một thời gian hết hạn nhất định)
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

                //3,Trả về key
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