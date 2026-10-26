using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Base.HOSP.Request;
using System;
using System.Net.Http;
using System.Web.Http;
using Newtouch.HIS.API.Common.Models;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// Xác thực ủy quyền
    /// </summary>
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        public AuthController(IComponentContext com) : base(com)
        { }
        #region private
        private readonly IAccessAuthDmnService _accessAuthDmnService;
        #endregion
        /// <summary>
        /// Dùng thông tin quan trọng (tên đăng nhập, mật khẩu, v.v.) để đổi lấy access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetTokenbyProxy")]
        public HttpResponseMessage GetTokenbyProxy(AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.AppId)
                    || string.IsNullOrWhiteSpace(request.OrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "";
                    return;
                }
                //then giả sử xác thực danh tính đã thành công
                AccessIdentity ident= new AccessIdentity();
                ident.AppId = request.AppId;
                ident.OrganizeId = request.OrganizeId;
                ident.Account = request.Account;
                //1. Xác thực danh tính người dùng từ request // thêm xác thực khi cần
                string message = "";
                bool authed= _accessAuthDmnService.Authorized(ident, out message);
                if (authed)
                {
                    //2. Danh tính được lưu trong Redis (có thời gian hết hạn nhất định)
                    var identity = new UserIdentity
                    {
                        AppId = request.AppId,
                        TokenType = request.TokenType,
                        UserId = request.UserId,
                        Account = request.Account,
                        OrganizeId = request.OrganizeId,
                        TopOrganizeId = request.TopOrganizeId,
                        //Tag = "ProxyAPI_TP"
                    };
                    var token = SaveIdentity(identity);
                    //3. Trả về key
                    resp.data = new
                    {
                        token = token
                    };
                }
                else
                {
                    resp.msg = message;
                }
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = base.CommonExecute(ac, request);
            return base.CreateResponse(response);
        }

        /// <summary>
        /// Dùng thông tin quan trọng (tên đăng nhập, mật khẩu, v.v.) để đổi lấy access token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                //then giả sử xác thực danh tính đã thành công
                //1. Xác thực danh tính người dùng từ request // thêm xác thực khi cần

                //2. Danh tính được lưu trong Redis (có thời gian hết hạn nhất định)
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
