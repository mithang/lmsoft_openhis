using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Lớp trợ giúp yêu cầu Base API
    /// </summary>
    public class SiteBaseAPIHelper : SiteAPIRequestHelperBase<SiteBaseAPIHelper>
    {
        /// <summary>
        /// Key cấu hình Addr của miền
        /// </summary>
        private readonly static string _domainConfigKey = "SiteBaseAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Base";

    }
}
