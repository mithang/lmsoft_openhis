using System;
using Newtonsoft.Json.Linq;
using Newtouch.Tools;

using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Trạm phối hợp y tế
    /// </summary>
    public class SiteCisAPIHelper
    {
        /// <summary>
        /// Loại token API mà helper này mint/gắn (phải trùng nhau: Authorize ở
        /// đích kiểm tra identity.TokenType == request.TokenType).
        /// </summary>
        private const string ApiTokenType = "PDS";
        /// <summary>
        /// Thanh toán API
        /// </summary>
        private static readonly string _siteCisAPIHost;

        /// <summary>
        /// Hàm tạo tĩnh
        /// </summary>
        static SiteCisAPIHelper()
        {
            _siteCisAPIHost = ConfigurationHelper.GetAppConfigValue("SiteCisAPIHost");
            if (string.IsNullOrWhiteSpace(_siteCisAPIHost))
            {
                _siteCisAPIHost = "localhost:17361";
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
            var key = string.Format("Token.Access.API.Cis.{0}.{1}", opr.UserId.Replace("-", ""), opr.OrganizeId.Replace("-", ""));
            APIRequestHelper.TokenClass tokenClass;
            if (!force)
            {
                tokenClass = RedisHelper.Get<APIRequestHelper.TokenClass>(key);
                if (tokenClass != null && tokenClass.LastRequestTime >= DateTime.Now.AddMinutes(-19))
                {
                    return tokenClass.Token;
                }
            }
            var tk = GetTokenFromApi();
            if (string.IsNullOrWhiteSpace(tk))
            {
                return null;
            }
            tokenClass = new APIRequestHelper.TokenClass
            {
                Token = tk,
                LastRequestTime = DateTime.Now
            };
            RedisHelper.Set(key, tokenClass);
            return tokenClass.Token;
        }

        /// <summary>
        /// post json return T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="tokenExpireReTryTimes">doToken Số lần thử lại khi hết hạn</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static T Request<TV, T>(string url, TV req, int tokenExpireReTryTimes = 1, Microsoft.AspNetCore.Http.HttpContext httpContext = null)
            where T : new()
            where TV : class
        {
            // F14 (phương án (c)): gắn token API vào mọi request — đích có
            // [DefaultAuthorize] (CIS.API HospitalizedBusiness/PatInfo) sẽ trả
            // TOKEN_IS_REQUIRED cho request trần không token.
            var body = AttachToken(req, GetToken());
            return PostRaw<JObject, T>(url, body, httpContext);
        }

        /// <summary>
        /// POST thô không gắn token — CHỈ nhánh mint token (GetTokenFromApi)
        /// dùng. Request() mà gọi GetToken() ở đây sẽ đệ quy vô hạn (GetToken
        /// → GetTokenFromApi → Request → GetToken …) nên đường mint tách riêng
        /// thành PostRaw, không đi qua Request().
        /// </summary>
        private static T PostRaw<TV, T>(string url, TV req, Microsoft.AspNetCore.Http.HttpContext httpContext = null)
            where T : new()
            where TV : class
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<TV, T>(GetCisApiUrl(url), req, httpContext: httpContext);
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
        /// Thanh toán API Host
        /// </summary>
        private static string SiteCisApiHost
        {
            get
            {
                return _siteCisAPIHost;
            }
        }

        /// <summary>
        /// Miền http://sample.com/
        /// </summary>
        private static string SiteCisApiDomain
        {
            get
            {
                if (!SiteCisApiHost.Contains("http"))
                {
                    return "http://" + SiteCisApiHost + "/";
                }
                return SiteCisApiHost;
            }
        }

        /// <summary>
        /// Hệ thống phối hợp y tế API Url
        /// </summary>
        /// <param name="url">url như'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetCisApiUrl(string url)
        {
            // F15: join chuẩn hoá — SiteCisApiDomain luôn kết thúc bằng '/'
            // (nhánh http trả nguyên host config, nhánh không-http thêm '/'
            // cuối; config PDS.Web SiteCisAPIHost = "http://127.0.0.1:5334/"),
            // nên nối trần Domain + url sẽ thành "//api/..." khi call-site có
            // slash đầu (HospitalizationPharmacyController :356/:404) → CIS.API
            // trả 404 body rỗng → DeserializeObject("") null → NRE dòng 358
            // (F14-PROBE §2.5). Trim 2 đầu để cả 2 dạng call-site đều ra đúng
            // 1 slash: "/api/..." và "api/..." (HospitalizationPharmacyApp
            // :182/:311, GetTokenFromApi) đều → ".../api/...".
            return SiteCisApiDomain.TrimEnd('/') + "/" + url.Replace("~/", "").TrimStart('/');
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
            var resp = PostRaw<object, APIRequestHelper.DefaultResponse<GetTokenResponse>>("api/Auth/GetTokenbyProxy", req);
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