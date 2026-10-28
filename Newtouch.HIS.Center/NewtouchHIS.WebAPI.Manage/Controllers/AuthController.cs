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
            // QA PHASE 2 (CTR-06): trước đây trả null → AppResultFilter biến
            // thành HttpStatus 404 "Không tìm thấy tài nguyên", BusData=null,
            // lệch với 8015 (40004). Nay trả FAIL 40004 tường minh như 8015:
            // route này đã ngừng, client phải dùng trung tâm xác thực (8015).
            return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "Chức năng này đã ngừng, vui lòng dùng trung tâm xác thực" };
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
            // QA PHASE 2 (CTR-06): như GetTokenAsync — FAIL 40004 thay vì null
            // → 404, thống nhất với 8015.
            return new BusResult<JWTToken> { code = ResponseResultCode.FAIL, msg = "Chức năng này đã ngừng, vui lòng dùng trung tâm xác thực" };
        }
    }
}
