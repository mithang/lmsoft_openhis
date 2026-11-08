using Newtouch.CIS.Compat;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Base API yêu cầu giúp giúp loại
    /// </summary>
    public class SiteBaseAPIHelper : SiteAPIRequestHelperBase<SiteBaseAPIHelper>
    {
        /// <summary>
        /// lĩnh vực Addr cấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteBaseAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Base";

    }
}
