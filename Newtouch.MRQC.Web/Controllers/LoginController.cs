using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// 登录登出 (SSO/UA callback actions from the Framework version are dropped:
    /// they depend on external SSO/UA hosts that do not exist in this deployment.
    /// Core login goes through LoginBaseController.CheckLogin -> BaseAPI.)
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
            //移除SSO登录来源标记
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }
    }
}
