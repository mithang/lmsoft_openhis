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
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Đăng nhập/Đăng xuất. Core login goes through LoginBaseController.CheckLogin -> BaseAPI.
    /// GetUALoginAddres is inherited from LoginBaseController (it resolves the
    /// address from BusinessConfig, same result as the original ConfigurationHelper keys).
    /// UALogin is ported up to the identity-exchange call; the Framework-only
    /// QuickLoginSuccess has no Core equivalent, so a successful exchange falls
    /// back to the normal Login page (no UAUserInfoGetAPI key is configured in
    /// this deployment, so this branch is unreachable here — same treatment as
    /// the OR port). UALoginPre keeps the original EMR preheat target (CIS).
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

        #region Đăng nhập ủy quyền thống nhất

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất (port từ bản gốc Framework).
        /// </summary>
        public async Task<IActionResult> UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login"); // Thiếu token
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
                return RedirectToAction("Index", "Login"); // Chưa cấu hình địa chỉ API đổi danh tính
            }

            // Tới API yêu cầu đổi lấy danh tính
            var sw = new Stopwatch();
            sw.Start();
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = ConfigInitHelper.SysConfig.AppId,
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
                        return RedirectToAction("Index", "Login"); // Đổi lấy danh tính thất bại
                    }
                    sw.Stop();
                    // Core has no QuickLoginSuccess equivalent: fall back to normal login.
                    return RedirectToAction("Index", "Login");
                }
            }
            catch
            {
                return RedirectToAction("Index", "Login"); // Đổi lấy danh tính thất bại
            }
        }

        /// <summary>
        /// Khởi động trước ủy quyền thống nhất (mở khởi động trước trang CIS) — port từ bản gốc Framework.
        /// </summary>
        public static void UALoginPre(OperatorModel opr)
        {
            try
            {
                if (opr != null && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    var appId = "CIS";
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

        #endregion
    }
}
