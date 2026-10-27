using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.Herp.Web
{
    /// <summary>
    /// Herp web controller base (mirrors the original App_Start/ControllerBase:
    /// empty subclass of OrgControllerBase with login enforcement).
    /// SECURITY (giai doan 7): KHONG dat [HandlerLogin] o day — attribute do lam
    /// FrameworkAuthorizationFilter bo qua moi action ke thua base nay.
    /// </summary>
    public abstract class ControllerBase : OrgControllerBase
    {
    }
}
