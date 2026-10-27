using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.WebAPI.Manage.Controllers
{
    /// <summary>
    /// Quản lý xác thực
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Obsolete("Chức năng này sắp ngừng,Vui lòng truy cập trung tâm xác thực")]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
        }
        /// <summary>
        /// Hệ thống thứ ba lấyToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetAppToken")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<JWTToken>> GetTokenAsync(Request<string> request)
        {
            //return await _jwtService.GetTokenAsync(new JWTTokenRequest
            //{
            //    authType = AuthType.WebApi,
            //    authIdentity = new AuthIdentity
            //    {
            //        AppId = request.AppId
            //    }
            //});
            return null;
        }
        /// <summary>
        /// Nội bộWebHệ thống lấyToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("GetUserToken")]
        [AllowAnonymous, HttpPost]
        public async Task<BusResult<JWTToken>> GetUserTokenAsync(Request<AuthIdentity> request)
        {
            //return await _jwtService.GetTokenAsync(new JWTTokenRequest
            //{
            //    authType = AuthType.Web,
            //    authIdentity = request.Data
            //});
            return null;
        }
    }
}
