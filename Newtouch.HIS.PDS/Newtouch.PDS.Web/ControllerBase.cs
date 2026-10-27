using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Web
{
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
        protected ControllerBase()
        {
            // ASP.NET Core port of the Framework PRF mechanism: the Framework base
            // ctor reflected over the derived controller and injected its private
            // readonly interface fields from the container. Services are registered
            // as singletons, so resolving them from the root provider here is safe.
            ServiceLocator.ResolveFields(this);
        }
    }
}
