using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtouch.HIS.Base.HOSP.API.Envelope;
using Newtouch.HIS.Base.HOSP.API.Services;
using Newtouch.HIS.Base.HOSP.Request;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Services.HttpService;
using Newtonsoft.Json;

namespace Newtouch.HIS.Base.HOSP.API.Controllers
{
    /// <summary>
    /// Chứng thực ủy quyền. GetTokenbyProxy giữ nguyên (xác thực quyền truy cập
    /// qua Authorized). GetToken dưới đây là bản KIỂM TRA CREDENTIAL THẬT
    /// (giai đoạn GetToken chạy thật): chỉ cấp token khi BaseAPI CheckPwd xác
    /// nhận credential thật (Account + Password dạng md5, đúng contract web
    /// login); sai/khóa/ngoài phạm vi tổ chức → 40004, KHÔNG cấp token, KHÔNG
    /// ghi Redis. Identity lấy từ kết quả CheckPwd, không tin trường do client
    /// tự khai. BẢO MẬT — PRODUCTION CHỈ PHƠI ROUTE NÀY QUA HTTPS (reverse
    /// proxy chặn HTTP; việc chặn ở proxy thuộc giai đoạn đóng gói, giai đoạn
    /// này KHÔNG tự dựng TLS). Không log mật khẩu, không log token.
    /// Lưu ý: SaveIdentity của base dùng TTL 120 phút (hành vi sẵn có của
    /// HOSP.API, dùng chung với GetTokenbyProxy) — giữ nguyên, không đổi
    /// thành 20 phút để khỏi đổi hành vi token của GetTokenbyProxy.
    /// </summary>
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IAccessAuthDmnService _accessAuthDmnService;
        private readonly IHttpClientHelper _http;
        private readonly IConfiguration _config;

        public AuthController(TokenStore tokenStore, IAccessAuthDmnService accessAuthDmnService,
            IHttpClientHelper http, IConfiguration config)
            : base(tokenStore)
        {
            _accessAuthDmnService = accessAuthDmnService;
            _http = http;
            _config = config;
        }

        /// <summary>
        /// Dùng thông tin chính để đổi lấy mã truy cập（Qua xác thực ủy quyền đại lý）。Gốc gọi Authorized() kiểm tra
        /// quyền truy cập rồi mới cấp token — bản port giữ nguyên bước kiểm tra này.
        /// </summary>
        [HttpPost("GetTokenbyProxy")]
        public ContentResult GetTokenbyProxy([FromBody] AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.AppId)
                    || string.IsNullOrWhiteSpace(request.OrganizeId))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "AUTH_FAILED";
                    // QA PHASE 2 (API-09): trước đây sub_msg rỗng.
                    resp.sub_msg = "UserId, AppId và OrganizeId không được để trống";
                    return;
                }
                var ident = new Newtouch.HIS.Domain.Entity.AccessIdentity();
                ident.AppId = request.AppId;
                ident.OrganizeId = request.OrganizeId;
                ident.Account = request.Account;
                string message = "";
                bool authed = _accessAuthDmnService.Authorized(ident, out message);
                if (authed)
                {
                    var identity = new Identity
                    {
                        AppId = request.AppId,
                        TokenType = request.TokenType,
                        UserId = request.UserId,
                        Account = request.Account,
                        OrganizeId = request.OrganizeId,
                        TopOrganizeId = request.TopOrganizeId,
                    };
                    var token = SaveIdentity(identity);
                    resp.data = new { token = token };
                }
                else
                {
                    resp.sub_msg = message;
                }
                // QUIRK GỐC (giữ nguyên): code vẫn là SUCCESS ngay cả khi
                // Authorized() trả false (chỉ khác ở sub_msg). Không tự ý sửa
                // hành vi này để tránh lệch contract với client cũ.
                resp.code = ResponseResultCode.SUCCESS;
            };
            var response = CommonExecute(ac, request);
            return CreateResponse(response);
        }

        /// <summary>
        /// Cấp token sau khi kiểm tra credential thật qua BaseAPI CheckPwd.
        /// </summary>
        [HttpPost("GetToken")]
        public ContentResult GetToken([FromBody] AuthGetTokenRequest request)
        {
            Action<AuthGetTokenRequest, DefaultResponse> ac = (req, resp) =>
            {
                if (string.IsNullOrWhiteSpace(req.TokenType))
                {
                    resp.code = ResponseResultCode.FAIL;
                    resp.sub_code = "TOKEN_TYPE_IS_REQUIRED";
                    resp.sub_msg = "Loại token truy cập trống";
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
                var identity = new Identity
                {
                    AppId = _config["BusinessConfig:AppId"] ?? _config["AppId"] ?? req.AppId,
                    TokenType = req.TokenType,
                    UserId = user.Id,
                    Account = user.Account,
                    UserCode = user.Account,
                    OrganizeId = user.TopOrganizeId,
                    TopOrganizeId = user.TopOrganizeId,
                };
                var token = SaveIdentity(identity);
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
            var response = CommonExecute(ac, request);
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
                var appId = _config["BusinessConfig:AppId"] ?? _config["AppId"] ?? "base.api";
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
