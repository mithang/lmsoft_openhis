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

            var uaApi = _config?["UAUserInfoGetAPI"];
            if (string.IsNullOrWhiteSpace(uaApi))
            {
                return RedirectToAction("Index", "Login"); //Chưa cấu hình API địa chỉ
            }

            //Tới API yêu cầu đổi lấy danh tính
            var sw = new Stopwatch();
            sw.Start();
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
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
