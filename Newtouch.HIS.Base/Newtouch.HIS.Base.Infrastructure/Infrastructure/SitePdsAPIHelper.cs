using System;
using Newtonsoft.Json.Linq;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Tools;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Kho dược
    /// </summary>
    public class SitePdsApiHelper
    {
        /// <summary>
        /// Loại token API mà helper này mint/gắn (phải trùng nhau: Authorize ở
        /// đích kiểm tra identity.TokenType == request.TokenType).
        /// </summary>
        private const string ApiTokenType = "Base.web";
        /// <summary>
        /// Quyết toán API
        /// </summary>
        private static readonly string _sitePdsAPIHost;

        /// <summary>
        /// Hàm khởi tạo tĩnh
        /// </summary>
        static SitePdsApiHelper()
        {
            _sitePdsAPIHost = ConfigurationHelper.GetAppConfigValue("SitePdsAPIHost");
            if (string.IsNullOrWhiteSpace(_sitePdsAPIHost))
            {
                _sitePdsAPIHost = "localhost:20138";
            }
        }

        /// <summary>
        /// GetToken
        /// </summary>
        /// <param name="force">Có làm mới mạnh không Token</param>
        /// <returns></returns>
        public static string GetToken(bool force = false)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return null;
            }
            var key = string.Format("Token.Access.API.Sett.{0}.{1}", opr.UserId.Replace("-", ""), opr.OrganizeId.Replace("-", ""));
            if (!force && _tokenCache.TryGetValue(key, out var cached)
                && cached.LastRequestTime >= DateTime.Now.AddMinutes(-19))
            {
                return cached.Token;
            }
            var tk = GetTokenFromApi();
            if (string.IsNullOrWhiteSpace(tk))
            {
                return null;
            }
            _tokenCache[key] = new TokenCacheEntry { Token = tk, LastRequestTime = DateTime.Now };
            return tk;
        }

        /// <summary>
        /// post json return T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="tokenExpireReTryTimes">DoToken Số lần thử lại khi hết hạn</param>
        /// <returns></returns>
        private static readonly HttpClient _http = new HttpClient();
        private static readonly ConcurrentDictionary<string, TokenCacheEntry> _tokenCache =
            new ConcurrentDictionary<string, TokenCacheEntry>();

        private class TokenCacheEntry
        {
            public string Token { get; set; }
            public DateTime LastRequestTime { get; set; }
        }

        public static T Request<T>(string url, object req, int tokenExpireReTryTimes = 1) where T : new()
        {
            // F14 (phương án (c)): gắn token API vào mọi request — đúng thứ tự
            // comment PharmacyDepartmentController prescribe ("sửa helper
            // trước, rồi mới gắn auth"). TokenType phải trùng chuỗi lúc mint.
            var body = AttachToken(req, GetToken());
            return PostRaw<T>(url, body);
        }

        /// <summary>
        /// POST thô không gắn token — CHỈ nhánh mint token (GetTokenFromApi)
        /// dùng. Request() mà gọi GetToken() ở đây sẽ đệ quy vô hạn (GetToken
        /// → GetTokenFromApi → Request → GetToken …) nên đường mint tách riêng
        /// thành PostRaw, không đi qua Request().
        /// </summary>
        private static T PostRaw<T>(string url, object req) where T : new()
        {
            // Original: HIS.API.Common HttpClientHelper (Framework binary, no source).
            // Replaced with a plain JSON POST; the PDS host does not exist in this
            // deployment, so callers get a connection error until it does.
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(req);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = _http.PostAsync(GetPdsApiUrl(url), content).GetAwaiter().GetResult();
            var str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// Chèn Token/TokenType vào bản sao JSON của req (req là anonymous
        /// type, không sửa trực tiếp được). Token null vẫn ghi rõ "Token":
        /// null để server thấy đúng là request chưa xác thực.
        /// </summary>
        private static JObject AttachToken(object req, string token)
        {
            var jobj = req == null ? new JObject()
                : req is JObject j ? new JObject(j) : JObject.FromObject(req);
            jobj["Token"] = new JValue(token);
            jobj["TokenType"] = new JValue(ApiTokenType);
            return jobj;
        }

        #region private methods

        /// <summary>
        /// Quyết toán API Host
        /// </summary>
        private static string SitePdsApiHost
        {
            get
            {
                return _sitePdsAPIHost;
            }
        }

        /// <summary>
        /// Miền http://sample.com/
        /// </summary>
        private static string SitePdsApiDomain
        {
            get
            {
                if (!SitePdsApiHost.Contains("http"))
                {
                    return "http://" + SitePdsApiHost + "/";
                }
                return SitePdsApiHost;
            }
        }

        /// <summary>
        /// Hệ thống quyết toán API Url
        /// </summary>
        /// <param name="url">url VD'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetPdsApiUrl(string url)
        {
            return SitePdsApiDomain + url.Replace("~/", "");
        }

        #endregion

        #region

        /// <summary>
        /// GetTokenFromApi
        /// </summary>
        /// <returns></returns>
        private static string GetTokenFromApi()
        {
            var opr = OperatorProvider.GetCurrent();
            var req = new
            {
                UserId = opr.UserId,
                Account = opr.UserCode,
                OrganizeId = opr.OrganizeId,
                TopOrganizeId = opr.TopOrganizeId,
                AppId = opr.AppId,
                Timestamp = DateTime.Now,
                TokenType = ApiTokenType
            };

            // F14: mint qua GetTokenbyProxy (không cần Password — 7 field giữ
            // nguyên, chỉ đổi URL). Đi đường POST thô PostRaw, không qua
            // Request() (tránh đệ quy mint). Quirk proxy: code vẫn SUCCESS
            // khi Authorized()=false, lúc đó data null → guard null (không
            // thì resp.data.Token ném NRE lên caller).
            var resp = PostRaw<APIRequestHelper.DefaultResponse<GetTokenResponse>>("api/Auth/GetTokenbyProxy", req);

            return resp != null && resp.code == APIRequestHelper.ResponseResultCode.SUCCESS && resp.data != null ? resp.data.Token : null;
        }

        /// <summary>
        /// GetTokenResponse
        /// </summary>
        protected class GetTokenResponse
        {
            public string Token { get; set; }
        }

        #endregion
    }
}