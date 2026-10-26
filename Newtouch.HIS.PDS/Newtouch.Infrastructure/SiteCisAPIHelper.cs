using System;
using System.Web;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Trạm làm việc phối hợp y bác sĩ
    /// </summary>
    public class SiteCisAPIHelper
    {
        /// <summary>
        /// Thanh toánAPI
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
        /// <param name="force">Có làm mới mạnh hay khôngToken</param>
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
        /// <param name="tokenExpireReTryTimes">VìTokenSố lần thử lại khi hết hạn</param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static T Request<TV, T>(string url, TV req, int tokenExpireReTryTimes = 1, HttpContext httpContext = null)
            where T : new()
            where TV : class
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<TV, T>(GetCisApiUrl(url), req, httpContext: httpContext);
        }

        #region private methods

        /// <summary>
        /// Thanh toánAPI Host
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
        /// Hệ thống phối hợp y bác sĩAPI Url
        /// </summary>
        /// <param name="url">url Như'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetCisApiUrl(string url)
        {
            return SiteCisApiDomain + url.Replace("~/", "");
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
                TokenType = "PDS"
            };
            var resp = Request<object, APIRequestHelper.DefaultResponse<GetTokenResponse>>("api/Auth/GetToken", req);
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