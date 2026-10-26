using System;
using System.Net.Http;
using System.Web.Http;
using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.API.Common.Models;
using Newtouch.PDS.Requset;

namespace Newtouch.PDS.API.Controllers
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
        /// Đổi thông tin quan trọng (tên đăng nhập, mật khẩu, v.v.) lấy mã truy cập
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
                //thenGiả sử xác thực danh tính đã thành công
                //1、BởirequestXác thực ra danh tính người dùng //Khi cần thì thêm xác thực

                //2、Danh tính được lưu tạiRedis（Có thời gian hết hạn nhất định）
                var identity = new UserIdentity
                {
                    AppId = request.AppId,
                    TokenType = request.TokenType,
                    UserId = request.UserId,
                    Account = request.Account,
                    OrganizeId = request.OrganizeId,
                    TopOrganizeId = request.TopOrganizeId,
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
