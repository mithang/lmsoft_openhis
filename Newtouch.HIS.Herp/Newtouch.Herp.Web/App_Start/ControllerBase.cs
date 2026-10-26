using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.Herp.Web
{
    /// <summary>
    /// Controller Lớp cơ sở
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {

    }
}