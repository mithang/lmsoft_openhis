using Autofac;
using FrameworkBase.API;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Sett.Request;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// ủy quyền 认证
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com)
       : base(com)
        {

        }

        /// <summary>
        /// 以关键thông tin(ngư ời dùng 名, mật khẩu V.v.) đ ổi lấy truy cập 令牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public HttpResponseMessage GetToken(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.Account)
                    || string.IsNullOrWhiteSpace(request.OrganizeId)
                    || string.IsNullOrWhiteSpace(request.TopOrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                //then giả sử thân phận xác minh thông qua 
                //1, 由request xác minh出 ngư ời dùng thân phận //Cần của giờ候 thêm vào xác minh

                //2, thân phận tồn tại 储tạiRedis(có nhất đ ịnh của hết hạn thời gian)
                var identity = new UserIdentity()
                {
                    AppId = request.AppId,
                    TokenType = request.TokenType,
                    UserId = request.UserId,
                    Account = request.Account,
                    OrganizeId = request.OrganizeId,
                    TopOrganizeId = request.TopOrganizeId,
                };
                var token = SaveIdentity(identity);

                //3, Quay lạikey
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
