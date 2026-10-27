using Microsoft.AspNetCore.Mvc.Filters;

namespace Newtouch.CIS.Web.Attributes
{
    /// <summary>
    /// Port of the MVC5 Web.Core HandlerAjaxOnlyAttribute (actions callable
    /// only via Ajax). In the ASP.NET Core port this is a no-op marker:
    /// Ajax callers keep working, and plain browser requests are also
    /// accepted (more permissive, pages still work). The vendor
    /// FrameworkAuthorizationFilter does not enforce Ajax-only.
    /// </summary>
    public class HandlerAjaxOnlyAttribute : ActionFilterAttribute
    {
    }
}
