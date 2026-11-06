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
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        private static IConfiguration _staticConfig;

        public LoginController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn, IConfiguration config)
            : base(loginApp, sysUserVDmn)
        {
            _staticConfig = config;
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

        /// <summary>
        /// Khởi động trước ủy quyền thống nhất（Mở khởi động trướcCISTrang）
        /// </summary>
        public static void UALoginPre(OperatorModel opr)
        {
            try
            {
                if (opr != null && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    var appId = "Sett";
                    if (!string.IsNullOrWhiteSpace(appId))
                    {
                        var addrList = new System.Collections.Generic.List<string>();
                        var addrKey = appId + "_UALoginPreAddress";
                        var iaddr = _staticConfig?[addrKey];
                        if (!string.IsNullOrWhiteSpace(iaddr))
                        {
                            addrList.Add(iaddr);
                        }
                        addrKey = appId + "_UALoginPreAddress2";
                        iaddr = _staticConfig?[addrKey];
                        if (!string.IsNullOrWhiteSpace(iaddr))
                        {
                            addrList.Add(iaddr);
                        }
                        var handler = new HttpClientHandler { UseProxy = false };
                        foreach (var addr in addrList)
                        {
                            if (!string.IsNullOrWhiteSpace(addr))
                            {
                                var addr1 = addr + "?account=" + opr.UserCode + "&organizeId=" + opr.OrganizeId;
                                Task.Run(async () => { using (var http = new HttpClient(handler)) { await http.GetStringAsync(addr1); } });
                                var addr2 = addr + "?configOrganizeId=" + opr.OrganizeId;
                                Task.Run(async () => { using (var http = new HttpClient(handler)) { await http.GetStringAsync(addr2); } });
                                var addr3 = addr + "?outPatientRegistrationQueryPreOrganizeId=" + opr.OrganizeId;
                                Task.Run(async () => { using (var http = new HttpClient(handler)) { await http.GetStringAsync(addr3); } });
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}
