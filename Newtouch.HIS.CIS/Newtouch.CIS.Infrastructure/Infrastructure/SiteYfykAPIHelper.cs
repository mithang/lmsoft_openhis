using Newtouch.CIS.Compat;

namespace Newtouch.Infrastructure
{
    public class SiteYfykAPIHelper:SiteAPIRequestHelperBase<SiteYfykAPIHelper>
    {
        /// <summary>
        /// lĩnh vựcAddrcấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteYfykAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Yfyk";
    }
}
