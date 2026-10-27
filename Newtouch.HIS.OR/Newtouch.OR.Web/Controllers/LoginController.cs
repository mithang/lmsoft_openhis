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
        public async Task<IActionResult> UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login"); //Thiếu token
            }

            var eOpr = OperatorProvider.GetCurrent();
            if (eOpr != null && eOpr.token == access_token)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            var uaApi = _staticConfig?["UAUserInfoGetAPI"];
            if (string.IsNullOrWhiteSpace(uaApi))
            {
                return RedirectToAction("Index", "Login"); //Chưa cấu hìnhAPIđịa chỉ
            }

            //TớiAPIYêu cầu đổi lấy danh tính
            var sw = new Stopwatch();
            sw.Start();
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token,
            };
            try
            {
                var handler = new HttpClientHandler { UseProxy = false };
                using (var http = new HttpClient(handler))
                {
                    var resp = await http.PostAsync(uaApi,
                        new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(reqObj), Encoding.UTF8, "application/json"));
                    var body = await resp.Content.ReadAsStringAsync();
                    var jResp = JObject.Parse(body);
                    if (jResp["code"]?.ToString() != "0" && jResp["code"]?.ToString()?.ToUpper() != "SUCCESS")
                    {
                        return RedirectToAction("Index", "Login"); //Đổi lấy danh tính thất bại
                    }
                    sw.Stop();
                    // Core has no QuickLoginSuccess equivalent: fall back to normal login.
                    return RedirectToAction("Index", "Login");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Login"); //Đổi lấy danh tính thất bại
            }
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
