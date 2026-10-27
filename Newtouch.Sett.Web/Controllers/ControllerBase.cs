using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>Sett local base (mirrors Framework Sett ControllerBase): login-checked OrgControllerBase.
    /// SECURITY (giai doan 7): KHONG dat [HandlerLogin] o day — attribute do lam
    /// FrameworkAuthorizationFilter bo qua moi action ke thua base nay.</summary>
    public abstract class ControllerBase : OrgControllerBase
    {
    }
}
