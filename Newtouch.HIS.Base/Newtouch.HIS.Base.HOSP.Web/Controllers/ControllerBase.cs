using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Framework.Web.Controllers;

namespace Newtouch.HIS.Base.HOSP.Web
{
    /// <summary>
    /// Port of the HOSP local ControllerBase (App_Start/01 Handler/ControllerBase.cs):
    /// OrgControllerBase (UserIdentity, OrganizeId, GetAuthOrganizeId, Success/Error)
    /// + the virtual Details action the Framework base lacks.
    /// SECURITY (giai doan 7): KHONG dat [HandlerLogin] o day — attribute do lam
    /// FrameworkAuthorizationFilter bo qua TOAN BO action cua moi controller ke thua
    /// base nay (JSON GetGridJson/SubmitForm chay khong can session). Chi Login/
    /// Home/ClientsData moi mang [HandlerLogin] rieng.
    /// </summary>
    public class ControllerBase : OrgControllerBase
    {
        public virtual IActionResult Details()
        {
            return View();
        }
    }
}
