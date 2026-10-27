using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;

namespace NewtouchHIS.Lib.Base.Utilities
{
    public static class MvcHelper
    {
        private static readonly string? _SiteStaticHost;
        static MvcHelper()
        {
            _SiteStaticHost = ConfigInitHelper.SysConfig.AppAPIHost?.SiteStaticHost ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "SiteStaticHost");
        }
        /// <summary>
        /// Tài nguyên tĩnhHost
        /// configcấu hìnhSiteStaticHost
        /// </summary>
        public static string? SiteStaticHost
        {
            get
            {
                return _SiteStaticHost;
            }
        }
        /// <summary>
        /// Số phiên bản tài nguyên
        /// Dùng để tránh bộ nhớ đệm tài nguyên tĩnh, tương ứngconfig Version
        /// </summary>
        public static string ver
        {
            get
            {
                string result = string.Empty;
#if DEBUG
                result = DateTime.Now.Ticks.ToString();
#else
                result = AppSettings.GetConfig("Version");
#endif
                return result;
            }
        }

        /// <summary>
        /// miền http://sample.com/
        /// </summary>
        public static string SiteStaticDomain
        {
            get
            {
                if (!SiteStaticHost.Contains("http"))
                {
                    return "http://" + SiteStaticHost + "/";
                }
                else
                {
                    return SiteStaticHost;
                }
            }
        }
        /// <summary>
        /// Trả về tài nguyên tĩnh kèm số phiên bản phần mềmUri
        /// </summary>
        /// <param name="url">url như'~/Home/Index'</param>
        /// <param name="isInSiteStatic">Tài nguyên có nằm trong tĩnh độc lập khôngDomain</param>
        /// <returns></returns>
        public static string GetStaticResourceScriptUrl(string url, bool isInSiteStatic = true)
        {
            string staticResource = "/";
            if (isInSiteStatic)
            {
                staticResource = SiteStaticDomain;
            }
            string fullUrl = staticResource + url.Replace("~/", "");
            return isInSiteStatic ? fullUrl : fullUrl + (!url.Contains("?") ? "?V=" : "&V=") + ver;
        }
    }
}
