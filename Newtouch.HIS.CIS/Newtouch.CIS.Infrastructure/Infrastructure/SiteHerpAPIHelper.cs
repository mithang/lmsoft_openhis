using Newtouch.CIS.Compat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// Herp API yêu cầu giúp giúp loại
    /// </summary>
    public class SiteHerpAPIHelper : SiteAPIRequestHelperBase<SiteHerpAPIHelper>
    {
        /// <summary>
        /// lĩnh vực Addr cấu hình của Key
        /// </summary>
        private readonly static string _domainConfigKey = "SiteHerpAPIHost";

        /// <summary>
        /// API Name
        /// </summary>
        private readonly static string _apiName = "Herp";
    }
}
