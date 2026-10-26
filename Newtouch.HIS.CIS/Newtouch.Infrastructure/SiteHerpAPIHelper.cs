using FrameworkBase.MultiOrg.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Lớp trợ giúp yêu cầu Herp API
    /// </summary>
    public class SiteHerpAPIHelper : SiteAPIRequestHelperBase<SiteHerpAPIHelper>
    {
        /// <summary>
        /// Key cấu hình Addr của miền
        /// </summary>
        private readonly static string _domainConfigKey = "SiteHerpAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Herp";
    }
}
