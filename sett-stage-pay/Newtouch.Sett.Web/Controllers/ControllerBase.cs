using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// Sett area controller base (mirrors the Framework Sett local base:
    /// login-required + multi-org base). Same pattern as the MRQC pilot.
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
    }
}
