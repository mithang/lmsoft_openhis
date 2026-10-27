using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.HIS.Base.HOSP.Web
{
    /// <summary>
    /// Port of the HOSP local ControllerBase (App_Start/01 Handler/ControllerBase.cs):
    /// OrgControllerBase (UserIdentity, OrganizeId, GetAuthOrganizeId, Success/Error)
    /// + [HandlerLogin] + the virtual Details action the Framework base lacks.
    /// </summary>
    [HandlerLogin]
    public class ControllerBase : OrgControllerBase
    {
        public virtual IActionResult Details()
        {
            return View();
        }
    }
}
