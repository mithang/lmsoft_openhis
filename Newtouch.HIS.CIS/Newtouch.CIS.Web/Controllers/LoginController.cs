using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtouch.CIS.Web.Attributes;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Đăng nhậpđăngra (SSO/UA callback actions from the Framework version are dropped:
    /// they depend on external SSO/UA hosts that do not exist in this deployment.
    /// Core login goes through LoginBaseController.CheckLogin -> BaseAPI.)
    /// GetUALoginAddres + OutLogin are inherited from LoginBaseController.
    /// UALogin is ported up to the identity-exchange call following the OR
    /// precedent: Core has no QuickLoginSuccess, so a successful exchange falls
    /// back to the normal Login page (no UAUserInfoGetAPI key is configured here,
    /// so this branch is unreachable in this deployment).
    /// SSOCallback is NOT ported: the net6 SSO host (5208) exposes no GetOpenId
    /// endpoint, and SSO login in the port goes through SSO + CheckLogin.
    /// UALoginPre is NOT ported as an action: it needs QuickLoginSuccess (no Core
    /// equivalent) and no CIS view calls it.
    /// ReturnSSOHome is ported: it clears the operator and redirects to the
    /// configured SSOHomeHost, falling back to the Login page when unconfigured.
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
            //loại bỏSSOĐăng nhậpđếnnguồndấughi
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất
        /// </summary>
        public IActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login"); //Thiếu token
            }

            var eOpr = OperatorProvider.GetCurrent();
            if (eOpr != null && eOpr.token == access_token)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            OperatorModel opr = null;
            try
            {
                var json = RedisHelper.GetString(access_token);
                if (!string.IsNullOrWhiteSpace(json) && json != "SIDELINED")
                {
                    opr = JsonConvert.DeserializeObject<OperatorModel>(json);
                }
            }
            catch
            {
                opr = null;
            }
            if (opr == null || (string.IsNullOrWhiteSpace(opr.UserId) && string.IsNullOrWhiteSpace(opr.UserCode)))
            {
                return RedirectToAction("Index", "Login"); // Token het han/khong hop le
            }

            OperatorProvider.AddCurrent(opr);
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Quay lại trang chủ đăng nhập một lần
        /// </summary>
        [HttpGet]
        public IActionResult ReturnSSOHome()
        {
            //Tương đương với việc thực hiện thao tác thoát trước
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                OperatorProvider.RemoveCurrent();
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
    }
}
