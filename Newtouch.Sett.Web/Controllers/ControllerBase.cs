using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>Sett local base (mirrors Framework Sett ControllerBase): login-checked OrgControllerBase.</summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
    }
}
