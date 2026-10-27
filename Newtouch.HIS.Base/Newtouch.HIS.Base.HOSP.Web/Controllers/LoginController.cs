using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Operator;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// Login via LoginBaseController (same as the MRQC pilot): the Framework
    /// LoginController is replaced, not ported; SSO/UA callback actions from
    /// the Framework version are dropped (the net6 SSO host 5208 exposes no
    /// GetOpenId endpoint, so SSOCallback + the no-arg CheckLogin SSO-callback
    /// path have nothing to call; SSO login in the port goes through SSO +
    /// CheckLogin POST -> BaseAPI). CheckLogin POST, GetAuthCode and OutLogin
    /// are inherited from LoginBaseController.
    /// ReturnSSOHome IS ported (audit 2026-10-27): Views/Home/Index links to
    /// it for SSO-origin logins, but the action was missing (HTTP 404).
    /// GetWebClientIp IS ported: Core equivalent of the Framework
    /// ServerVariables lookup (X-Forwarded-For else RemoteIpAddress).
    /// Core login goes through LoginBaseController.CheckLogin -> BaseAPI.
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        private readonly IConfiguration _config;

        public LoginController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn, IConfiguration config) : base(loginApp, sysUserVDmn)
        {
            _config = config;
        }

        public override void OprBeforeSave(OperatorModel opr)
        {
        }

        [HttpGet]
        public override IActionResult Index()
        {
            //Loại bỏSSODấu nguồn đăng nhập
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }

        /// <summary>
        /// Quay lại trang chủ đăng nhập một lần (bản gốc: ghi log thoát, xóa
        /// cookie LoginFlag, chuyển tới SSOHomeHost đã cấu hình).
        /// </summary>
        [HttpGet]
        public IActionResult ReturnSSOHome()
        {
            //Tương đương với việc thực hiện thao tác thoát trước
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                OperatorProvider.RemoveCurrent();
                HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_" + opr.UserCode + "_" + "LoginFlag");
            }

            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            //Chuyển về trang đăng nhập một lần
            var url = _config?["SSOHomeHost"];
            if (!string.IsNullOrWhiteSpace(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// Lấy IP của Web client (bản gốc đọc ServerVariables
        /// HTTP_X_FORWARDED_FOR rồi REMOTE_ADDR).
        /// </summary>
        public string GetWebClientIp()
        {
            var forwarded = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            if (!string.IsNullOrEmpty(forwarded))
            {
                return forwarded;
            }
            var remote = HttpContext.Connection.RemoteIpAddress;
            if (remote != null)
            {
                return remote.ToString();
            }
            return string.Empty;
        }
    }
}
