using System;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Tools;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 药房药库
    /// </summary>
    public class SitePdsApiHelper
    {
        /// <summary>
        /// 结算API
        /// </summary>
        private static readonly string _sitePdsAPIHost;

        /// <summary>
        /// 静态构造函数
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
        /// <param name="force">是否强刷新Token</param>
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
        /// <param name="tokenExpireReTryTimes">因Token过期重试次数</param>
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
            // Original: HIS.API.Common HttpClientHelper (Framework binary, no source).
            // Replaced with a plain JSON POST; the PDS host does not exist in this
            // deployment, so callers get a connection error until it does.
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(req);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = _http.PostAsync(GetPdsApiUrl(url), content).GetAwaiter().GetResult();
            var str = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }

        #region private methods

        /// <summary>
        /// 结算API Host
        /// </summary>
        private static string SitePdsApiHost
        {
            get
            {
                return _sitePdsAPIHost;
            }
        }

        /// <summary>
        /// 域 http://sample.com/
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
        /// 结算系统API Url
        /// </summary>
        /// <param name="url">url 如'~/Home/Index'</param>
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
                TokenType = "Base.web"
            };

            var resp = Request<APIRequestHelper.DefaultResponse<GetTokenResponse>>("api/Auth/GetToken", req);

            return resp.code == APIRequestHelper.ResponseResultCode.SUCCESS ? resp.data.Token : null;
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