using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.CIS.Compat;

namespace Newtouch.Infrastructure
{
    public class SiteORAPIHelper: SiteAPIRequestHelperBase<SiteORAPIHelper>
    {
        /// <summary>
        /// lĩnh vựcAddrcấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteORAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "OR";
    }
}
