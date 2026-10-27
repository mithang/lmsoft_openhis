using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.Herp.Web
{
    /// <summary>
    /// Herp web controller base (mirrors the original App_Start/ControllerBase:
    /// empty subclass of OrgControllerBase with login enforcement).
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
    }
}
