using System.Web.Mvc;
using FrameworkBase.MultiOrg.Web;

namespace Newtouch.HIS.Web
{
    /// <summary>
    /// Controller lớp cơ sở
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : OrgControllerBase
    {
        
    }

}
