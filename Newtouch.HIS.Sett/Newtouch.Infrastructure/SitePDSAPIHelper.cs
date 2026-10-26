using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// PDS API yêu cầu giúp đ ỡ loại 
    /// </summary>
    public class SitePDSAPIHelper : SiteAPIRequestHelperBase<SitePDSAPIHelper>
    {
        /// <summary>
        /// lĩnh vực Addr cấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SitePDSAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "PDS";

    }

}
