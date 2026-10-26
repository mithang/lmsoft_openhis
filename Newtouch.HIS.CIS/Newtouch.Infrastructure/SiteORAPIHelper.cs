using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.Infrastructure
{
    public class SiteORAPIHelper: SiteAPIRequestHelperBase<SiteORAPIHelper>
    {
        /// <summary>
        /// Key cấu hình Addr của miền
        /// </summary>
        private readonly static string _domainConfigKey = "SiteORAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "OR";
    }
}
