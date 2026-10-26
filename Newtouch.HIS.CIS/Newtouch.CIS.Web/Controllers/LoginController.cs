using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtonsoft.Json.Linq;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Web.Core.Url;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// Đăng nhập, đăng xuất
    /// </summary>
    public class LoginController : FrameworkBase.MultiOrg.Web.Controllers.LoginController
    {
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysLogRepo _sysLogRepo;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        #region 单点登录start

        /// <summary>
        /// 1.Gọi lại của đăng nhập một lần, nhậntoken, truy cập của đăng nhập một lầnAPIđể nhận đượcUserId
        /// </summary>
        /// <returns></returns>
        public ActionResult SSOCallback(string access_token, string returnUrl)
        {
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(
                SiteUrl.GetSSOAPIUrl("api/Auth/GetOpenId"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            var openId = "";    //Đây làAccount
            var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
            foreach (JProperty prop in jObj.Properties())
            {
                if (prop.Name == "openId")
                {
                    openId = prop.Value.ToString();
                }
            }

            Action acBeforeRedirect = () =>
            {
                //dùng để đánh dấu nguồn đăng nhập của người dùng
                WebHelper.WriteCookie(Constants.AppId + "_LoginFromFlag", EnumLoginFlag.SSO.ToString());
            };
            return QuickLoginSuccess(openId, returnUrl: returnUrl, acBeforeRedirect: acBeforeRedirect);
        }

        /// <summary>
        /// Quay lại trang chủ đăng nhập một lần
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReturnSSOHome()
        {
            //Tương đương với việc thực hiện thao tác thoát trước
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();

            WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");

            //Chuyển về trang đăng nhập một lần
            var url = ConfigurationHelper.GetAppConfigValue("SSOHomeHost");

            return Redirect(url);
        }

        #endregion 单点登录 end

        #region 统一授权登录

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login");  //Thiếu mã thông báo
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
            var aaa = ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI");
            if (string.IsNullOrWhiteSpace(aaa))
            {
                return RedirectToAction("Index", "Login");  //chưa cấu hình đổi danh tínhAPIĐịa chỉ
            }
            //向APIYêu cầu đổi danh tính
            var sw = new Stopwatch();
            sw.Start();
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            AppLogger.Info(string.Format("{0}统一登录授权，身份获取API请求耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
            sw.Reset();
            if (apiResp.code != APIRequestHelper.ResponseResultCode.SUCCESS)
            {
                return RedirectToAction("Index", "Login");  //Đổi danh tính thất bại
            }
            sw.Start();
            string account = "", organizeId = "";
            var jObj = Tools.Json.ToJObject(apiResp.data.ToString());
            foreach (JProperty prop in jObj.Properties())
            {
                if (prop.Name == "Account")
                {
                    account = prop.Value.ToString();
                }
                else if (prop.Name == "OrganizeId")
                {
                    organizeId = prop.Value.ToString();
                }
            }

            Action acBeforeRedirect = () =>
            {
                AppLogger.Info(string.Format("{0}统一登录授权，QuickLoginSuccess耗时{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
                sw.Stop();
            };

            return QuickLoginSuccess(account, organizeId, returnUrl: returnUrl, withToken: access_token, acBeforeRedirect: acBeforeRedirect);
        }

        /// <summary>
        /// Ủy quyền thống nhất lấy địa chỉ chuyển hướng
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult GetUALoginAddres(string appId)
        {
            string addr = null;
            if (!string.IsNullOrWhiteSpace(appId))
            {
                appId = appId + "_UALoginAddress";
                addr = ConfigurationHelper.GetAppConfigValue(appId);
            }
            if (!string.IsNullOrWhiteSpace(addr))
            {
                var token = OperatorProvider.GetCurrentToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    if (addr.IndexOf('?') < 0)
                    {
                        addr += "?access_token=" + token;
                    }
                    else
                    {
                        addr += "&access_token=" + token;
                    }
                }
                else
                {
                    addr = null;    //Xem nhưnull
                }
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), data = addr }.ToJson());
        }

        /// <summary>
        /// Làm nóng đăng nhập ủy quyền thống nhất
        /// </summary>
        /// <param name="account"></param>
        /// <param name="organizeId"></param>
        /// <param name="configOrganizeId">Cấu hình tham số động làm nóng</param>
        /// <param name="outPatientRegistrationQueryPreOrganizeId">Tra cứu bệnh nhân ngoại trúAPILàm nóng</param>
        /// <returns></returns>
        public ActionResult UALoginPre(string account, string organizeId, string configOrganizeId
            , string outPatientRegistrationQueryPreOrganizeId)
        {
            if (!string.IsNullOrWhiteSpace(configOrganizeId))
            {
                //Cấu hình tham số động hệ thống
                _sysConfigRepo.GetByCode("any_any_any_any", configOrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(account) && !string.IsNullOrWhiteSpace(organizeId))
            {
                //Đăng nhập mô phỏng
                QuickLoginSuccess(account, organizeId, returnUrl: "null");
            }
            if (!string.IsNullOrWhiteSpace(outPatientRegistrationQueryPreOrganizeId))
            {
                //Tra cứu bệnh nhân ngoại trú
                var url = ConfigurationHelper.GetAppConfigValue("UAOutPatientRegistrationQueryPreAPI");
                if (!string.IsNullOrWhiteSpace(url))
                {
                    url += "?organizeId=" + outPatientRegistrationQueryPreOrganizeId;
                    var apiResp =
    Newtouch.Core.Common.Utils.HttpClientHelper.HttpGetString(url);
                }
            }
            return null;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Index()
        {
            //Xóa bỏSSODấu hiệu nguồn đăng nhập
            WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");

            return base.Index();
        }

        /// <summary>
        /// Thoát đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();

            var loginFromFlag = WebHelper.GetCookie(Constants.AppId + "_LoginFromFlag");
            if (!string.IsNullOrWhiteSpace(loginFromFlag))
            {
                WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");
                //ThoátSSOĐăng nhập
                var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");
                return Redirect(url);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}