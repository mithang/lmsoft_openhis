using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Text;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Đăng nhập/Đăng xuất. Core login goes through LoginBaseController.CheckLogin -> BaseAPI.
    /// GetUALoginAddres is inherited from LoginBaseController (it resolves the
    /// address from BusinessConfig AppAPIHostName, same result as the original
    /// ConfigurationHelper keys). UALogin is ported up to the identity-exchange
    /// call; the Framework-only QuickLoginSuccess has no Core equivalent, so a
    /// successful exchange falls back to the normal Login page (the UA host
    /// http://localhost:7002 does not exist in this deployment, and no
    /// UAUserInfoGetAPI key is configured, so this branch is unreachable here).
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        public LoginController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn)
            : base(loginApp, sysUserVDmn)
        {
        }

        public override void OprBeforeSave(OperatorModel opr)
        {
        }

        [HttpGet]
        public override IActionResult Index()
        {
            //XóaSSOĐánh dấu nguồn đăng nhập
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

        // NX18-002 (du thao D-NX18-002): UALoginPre DA XOA khoi file nay.
        // Ly do: 0 caller repo-wide (grep .cs/.cshtml/.js); key cau hinh
        // Sett_UALoginPreAddress(2) khong ton tai o bat ky appsettings nao
        // nen ham duoc goi cung la no-op (addrList rong); endpoint dich
        // UALoginPre khong co o cay port (vendor CIS/MRQC co action nay
        // nhung port khong port sang) nen khong the wire sau AddCurrent.
        // Hanh vi UALogin truoc/sau xoa khong doi (ham khong bao gio duoc
        // goi); probe truoc xoa (NX18, OR 5231): token gia -> 302 /,
        // 0 cookie, GET / -> 200 login. CHUA live-proven sau xoa (can
        // redeploy + probe lai; coordinator quyet).
    }
}
