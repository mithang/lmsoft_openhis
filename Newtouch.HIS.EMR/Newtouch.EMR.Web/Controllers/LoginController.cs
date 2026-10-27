using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Login/logout. Core login goes through LoginBaseController.CheckLogin -> BaseAPI.
    /// The Framework UALogin callback is dropped: it depends on the external UA host
    /// (localhost:7002) that does not exist in this deployment.
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        public LoginController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn) : base(loginApp, sysUserVDmn)
        {
        }

        public override void OprBeforeSave(OperatorModel opr)
        {
        }

        [HttpGet]
        public override IActionResult Index()
        {
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");
            return base.Index();
        }

        /// <summary>
        /// Logout: drop the operator identity and return to the login page.
        /// (No server-side Session middleware on Core, so there is no Session to abandon.)
        /// </summary>
        [HttpGet]
        public IActionResult OutLogin()
        {
            OperatorProvider.RemoveCurrent();
            return RedirectToAction("Index", "Login");
        }
    }
}
