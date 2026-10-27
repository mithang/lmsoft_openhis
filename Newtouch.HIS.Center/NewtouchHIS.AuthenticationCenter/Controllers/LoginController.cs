using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.AuthenticationCenter.Services;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Security;

namespace NewtouchHIS.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly ISysUserVDmnService _userVDmnService;
        private readonly IJWTService _jwtService;
        public LoginController(ISysUserVDmnService userVDmnService, IJWTService jWTService)
        {
            _userVDmnService = userVDmnService;
            _jwtService = jWTService;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [Route("UserLogin")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<JWTToken>> UserLoginAsync(RequestBus<LoginBO> login)
        {
            if (login == null || login.Data == null || (string.IsNullOrWhiteSpace(login.Data!.Username) && (string.IsNullOrWhiteSpace(login.Data!.Password))))
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "Đăng nhập thất bại，Vui lòng nhập tên người dùng và mật khẩu" };
            }
            login.Data.Password = MD5Encrypt.Encrypt(login.Data.Password);
            var logResult = await _userVDmnService.UserLogin(login.Data);
            if (logResult == null || logResult.Data == null)
            {
                return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = logResult!.msg ?? "Đăng nhập thất bại，Không tìm thấy thông tin người dùng" };
            }
            AuthIdentity authIdentity = new AuthIdentity()
            {
                UserCode = logResult.Data.Account,
                UserName = login.Data.Password,
            };
            return await _jwtService.GetTokenAsync(new JWTTokenRequest
            {
                authType = AuthType.Web,
                authIdentity = new AuthIdentity
                {
                    TopOrganizeId = logResult.Data.TopOrganizeId,
                    Account = logResult.Data.Account,
                    AppId = login.AppId,
                    OrganizeId = login.OrganizeId
                }
            });
        }

    }
}
