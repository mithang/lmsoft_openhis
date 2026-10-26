namespace Newtouch.Infrastructure
{
    /// <summary>
    /// pds api helper
    /// </summary>
    public class SitePDSAPIHelper
    {
        /// <summary>
        /// BaseHệ thốngAPI
        /// </summary>
        private static readonly string SitePdsapiHost;

        /// <summary>
        /// Hàm tạo tĩnh
        /// </summary>
        static SitePDSAPIHelper()
        {
            SitePdsapiHost = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("SitePDSAPIHost");
            if (string.IsNullOrWhiteSpace(SitePdsapiHost))
            {
                SitePdsapiHost = "localhost:20951";
            }
        }

        /// <summary>
        /// post json return T
        /// </summary>
        /// <typeparam name="T">Trả về loại tham số</typeparam>
        /// <typeparam name="TV">Loại tham số yêu cầu</typeparam>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="tokenExpireReTryTimes">VìTokenSố lần thử lại khi hết hạn</param>
        /// <returns></returns>
        public static T Request<TV, T>(string url, TV req, int tokenExpireReTryTimes = 1)
            where T : new()
            where TV : class
        {
            return HIS.API.Common.Helper.HttpClientHelper.HttpPostStringAndRead<TV, T>(GetPDSAPIUrl(url), req);
        }

        #region private methods

        /// <summary>
        /// BaseAPIHost
        /// configCấu hình_SitePDSApiHost
        /// </summary>
        private static string SitePDSApiHost
        {
            get
            {
                return SitePdsapiHost;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static string SitePDSApiDomain
        {
            get
            {
                if (!SitePDSApiHost.Contains("http"))
                {
                    return "http://" + SitePDSApiHost + "/";
                }
                return SitePDSApiHost;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string GetPDSAPIUrl(string url)
        {
            return SitePDSApiDomain + url.Replace("~/", "");
        }

        #endregion

    }
}