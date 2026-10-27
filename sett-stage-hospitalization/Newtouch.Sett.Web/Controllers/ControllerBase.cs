using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// Sett area base controller (mirrors the Framework Sett local base):
    /// login-required + multi-org identity from OrgControllerBase.
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
    }
}
