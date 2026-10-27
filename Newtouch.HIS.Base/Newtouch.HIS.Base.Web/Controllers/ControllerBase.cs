using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.HIS.Base.Web
{
    /// <summary>
    /// Port of the HOSP local ControllerBase (App_Start/01 Handler/ControllerBase.cs):
    /// OrgControllerBase (UserIdentity, OrganizeId, GetAuthOrganizeId, Success/Error)
    /// + the virtual Details action the Framework base lacks.
    /// SECURITY (giai doan 7): KHONG dat [HandlerLogin] o day — xem ghi chu tuong tu
    /// trong HOSP ControllerBase (attribute do vo hieu filter global).
    /// </summary>
    public class ControllerBase : OrgControllerBase
    {
        public virtual IActionResult Details()
        {
            return View();
        }
    }
}
