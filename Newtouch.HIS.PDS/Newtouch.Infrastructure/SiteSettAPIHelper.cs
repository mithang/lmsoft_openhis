using Newtouch.Common.Operator;
using Newtouch.Core.Common.Utils;
using Newtouch.Core.Redis;
using System;
using Newtouch.Tools;
using static Newtouch.Common.Web.APIRequestHelper;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Hệ thống thanh toán APILớp trợ giúp yêu cầu
    /// </summary>
    public class SiteSettAPIHelper
    {
        /// <summary>
        /// Thanh toánAPI
        /// </summary>
        private static readonly string _SiteSettAPIHost;

        /// <summary>
        /// Hàm tạo tĩnh
        /// </summary>
        static SiteSettAPIHelper()
        {
            _SiteSettAPIHost = ConfigurationHelper.GetAppConfigValue("SiteSettAPIHost");
            if (string.IsNullOrWhiteSpace(_SiteSettAPIHost))
            {
                _SiteSettAPIHost = "localhost:17361";
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
            var key = string.Format("Token.Access.API.Sett.{0}.{1}", opr.UserId.Replace("-", ""), opr.OrganizeId.Replace("-", ""));
            TokenClass tokenClass;
            if (!force)
            {
                tokenClass = RedisHelper.Get<TokenClass>(key);
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
            tokenClass = new TokenClass
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
        /// <returns></returns>
        public static T Request<TV, T>(string url, TV req, int tokenExpireReTryTimes = 1)
            where T : new()
            where TV : class
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<TV, T>(GetSettApiUrl(url), req);
        }

        #region private methods

        /// <summary>
        /// Thanh toánAPI Host
        /// </summary>
        private static string SiteSettApiHost
        {
            get
            {
                return _SiteSettAPIHost;
            }
        }

        /// <summary>
        /// Miền http://sample.com/
        /// </summary>
        private static string SiteSettApiDomain
        {
            get
            {
                if (!SiteSettApiHost.Contains("http"))
                {
                    return "http://" + SiteSettApiHost + "/";
                }
                return SiteSettApiHost;
            }
        }

        /// <summary>
        /// Hệ thống thanh toánAPI Url
        /// </summary>
        /// <param name="url">url Như'~/Home/Index'</param>
        /// <returns></returns>
        private static string GetSettApiUrl(string url)
        {
            return SiteSettApiDomain + url.Replace("~/", "");
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

            var resp = Request<object, DefaultResponse<GetTokenResponse>>("api/Auth/GetToken", req);

            return resp.code == ResponseResultCode.SUCCESS ? resp.data.Token : null;
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
