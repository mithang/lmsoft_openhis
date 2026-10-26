using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.EMR.Infrastructure
{
    public class SiteHispatinfoAPIHelper: SiteAPIRequestHelperBase<SiteHispatinfoAPIHelper>
    {
        /// <summary>
        /// Key cấu hình Addr của miền
        /// </summary>
        private readonly static string _domainConfigKey = "SiteSettAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Sett";
    }
}
