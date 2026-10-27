using Newtouch.CIS.Compat;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Base APIyêu cầugiúpgiúploại
    /// </summary>
    public class SiteBaseAPIHelper : SiteAPIRequestHelperBase<SiteBaseAPIHelper>
    {
        /// <summary>
        /// lĩnh vựcAddrcấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteBaseAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Base";

    }
}
