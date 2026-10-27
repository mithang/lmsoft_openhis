using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtouch.Herp.API.Common;
using Newtouch.Herp.API.Models;
using Newtouch.Herp.API.Requests;
using Newtouch.Herp.API.Services;
using NewtouchHIS.Lib.Services.HttpService;
using Newtonsoft.Json;

namespace Newtouch.Herp.API.Controllers
{
    /// <summary>
    /// GetToken KIỂM TRA CREDENTIAL THẬT (giai đoạn GetToken chạy thật).
    ///
    /// Khác hẳn stub cũ (AUTH_NOT_IMPLEMENTED) và khác hẳn bản gốc (cấp token
    /// cho identity toàn "?"): endpoint này chỉ cấp token khi BaseAPI CheckPwd
    /// xác nhận credential thật (Account + Password dạng md5, đúng contract
    /// web login). Sai/khóa/ngoài phạm vi tổ chức → 40004, KHÔNG cấp token,
    /// KHÔNG ghi Redis. Identity lưu Redis lấy từ kết quả CheckPwd (Id/Account/
    /// TopOrganizeId; OrganizeId = TopOrganizeId theo fallback của web login),
    /// KHÔNG tin các trường identity do client tự khai.
    ///
    /// BẢO MẬT — PRODUCTION CHỈ PHƠI ROUTE NÀY QUA HTTPS: reverse proxy phải
    /// chặn HTTP cho route này (việc chặn ở proxy thuộc giai đoạn đóng gói,
    /// giai đoạn này KHÔNG tự dựng TLS). Không log mật khẩu, không log token.
    /// Token dùng cơ chế sẵn có của Herp: ITokenStore.SaveIdentity (GUID thô
    /// làm key Redis, TTL 20 phút trượt) — giữ nguyên, không chuyển sang JWT.
    /// </summary>
    [Route("api/Auth")]
    public class AuthController : ApiControllerBase<AuthController>
    {
        private readonly ITokenStore _tokenStore;
        private readonly IHttpClientHelper _http;
        private readonly IConfiguration _config;

        public AuthController(IUserIdentityResolver<Identity> userIdentityResolver,
            ITokenStore tokenStore, IHttpClientHelper http, IConfiguration config)
            : base(userIdentityResolver)
        {
            _tokenStore = tokenStore;
            _http = http;
            _config = config;
        }

        /// <summary>Cấp token sau khi kiểm tra credential thật qua BaseAPI CheckPwd.</summary>
        [Route("GetToken")]
        [HttpPost]
        public IActionResult GetToken([FromBody] AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> action = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(req.TokenType))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "TOKEN_TYPE_IS_REQUIRED";
                    resp.sub_msg = "Loại mã truy cập trống";
                    return;
                }
                if (string.IsNullOrWhiteSpace(req.Account) || string.IsNullOrWhiteSpace(req.Password))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "Tên đăng nhập và mật khẩu không được để trống";
                    return;
                }
                var user = CheckPwd(req.Account, req.Password);
                if (user == null)
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa";
                    return;
                }
                if (user.Locked == true)
                {
                    // Phòng thủ thêm: BaseAPI hiện tại đã trả FAIL (không Data)
                    // cho tài khoản khóa nên nhánh này thường không tới được
                    // qua CheckPwd, nhưng vẫn từ chối tường minh nếu Data có
                    // Locked = true.
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "Tài khoản đã bị khóa";
                    return;
                }
                var configuredTopOrg = _config["BusinessConfig:Top_OrganizeId"] ?? _config["Top_OrganizeId"];
                if (!string.IsNullOrWhiteSpace(configuredTopOrg)
                    && !string.IsNullOrWhiteSpace(user.TopOrganizeId)
                    && !string.Equals(configuredTopOrg, user.TopOrganizeId, StringComparison.OrdinalIgnoreCase))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "Tài khoản không thuộc phạm vi tổ chức của API này";
                    return;
                }
                // Lưu dạng UserIdentity để OrganizeId/TopOrganizeId có trong
                // JSON Redis (Identity gốc của Herp không có 2 trường này;
                // resolver deserialize về UserIdentity).
                var identity = new UserIdentity
                {
                    AppId = _config["BusinessConfig:AppId"] ?? _config["AppId"] ?? req.AppId,
                    TokenType = req.TokenType,
                    UserId = user.Id,
                    Account = user.Account,
                    UserCode = user.Account,
                    UserName = user.Account,
                    OrganizeId = user.TopOrganizeId,
                    TopOrganizeId = user.TopOrganizeId,
                };
                var token = _tokenStore.SaveIdentity(identity);
                if (string.IsNullOrWhiteSpace(token))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    resp.sub_msg = "Không lưu được token, vui lòng thử lại";
                    return;
                }
                resp.code = ResponseResultCode.SUCCESS;
                resp.data = new { token = token };
            };

            var response = CommonExecute(action, request, nameof(GetToken));
            return CreateResponse(response);
        }

        /// <summary>
        /// Gọi BaseAPI CheckPwd đúng contract web login (LoginAppService):
        /// Request{AppId = AppId của chính API này (bắt buộc — CheckPwd từ chối
        /// request thiếu AppId), Timestamp, Data{Username, Password md5}}; bóc
        /// envelope ngoài {busData:{code,msg,data}}. Trả null khi credential
        /// sai/khóa/BaseAPI không tới được — caller luôn fail-closed (40004,
        /// không cấp token).
        /// </summary>
        private CheckPwdUser CheckPwd(string account, string passwordMd5)
        {
            try
            {
                var host = _config["BusinessConfig:AppAPIHost:HisAppBaseAPIHost"]
                    ?? _config["AppAPIHost:HisAppBaseAPIHost"]
                    ?? "http://127.0.0.1:7001/";
                var appId = _config["BusinessConfig:AppId"] ?? _config["AppId"] ?? "Herp";
                var payload = new
                {
                    AppId = appId,
                    Timestamp = DateTime.Now,
                    Data = new { Username = account, Password = passwordMd5 },
                };
                var envelope = _http.PostAsync<CheckPwdEnvelope>(
                    host.TrimEnd('/') + "/api/login/CheckPwd",
                    JsonConvert.SerializeObject(payload)).GetAwaiter().GetResult();
                var result = envelope?.busData;
                if (result == null || result.code != 10000 || result.Data == null) return null;
                return result.Data;
            }
            catch
            {
                return null;
            }
        }

        private class CheckPwdEnvelope
        {
            public CheckPwdResult busData { get; set; }
        }

        private class CheckPwdResult
        {
            public int code { get; set; }
            public string msg { get; set; }
            public CheckPwdUser Data { get; set; }
        }

        private class CheckPwdUser
        {
            public string Account { get; set; }
            public string Id { get; set; }
            public string TopOrganizeId { get; set; }
            public bool? Locked { get; set; }
        }
    }
}
