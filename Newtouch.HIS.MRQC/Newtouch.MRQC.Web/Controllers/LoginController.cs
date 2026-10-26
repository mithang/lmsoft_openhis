using System;
using System.Diagnostics;
using System.Web.Mvc;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtonsoft.Json.Linq;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Common.Web;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Web.Core.Url;
using Newtouch.MRQC.Infrastructure;
using Newtouch.Tools;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// đăng nhập/đăng xuất
    /// </summary>
    public class LoginController : FrameworkBase.MultiOrg.Web.Controllers.LoginController
    {
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly ISysLogRepo _sysLogRepo;
        private readonly ISysUserRepo _sysUserRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        public override void OprBeforeSave(OperatorModel opr)
        {
            
        }

        #region Đăng nhập một lần start

        /// <summary>
        /// 1.hàm gọi lại của đăng nhập một lần，nhận token，truy cập đăng nhập một lần API để lấy được UserId
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
            var openId = "";    //đây làAccount
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
        /// trả về trang chủ đăng nhập một lần
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReturnSSOHome()
        {
            //tương đương với việc thực hiện thao tác thoát trước
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            Session.Abandon();
            Session.Clear();

            WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");

            //chuyển về trang đăng nhập một lần
            var url = ConfigurationHelper.GetAppConfigValue("SSOHomeHost");

            return Redirect(url);
        }

        #endregion Đăng nhập một lần end
        #region đăng nhập ủy quyền thống nhất

        /// <summary>
        /// đăng nhập ủy quyền thống nhất
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login");  //thiếu mã thông báo
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
                return RedirectToAction("Index", "Login");  //chưa cấu hình đổi danh tính API Địa chỉ
            }
            //tớiAPI yêu cầu đổi danh tính
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
                return RedirectToAction("Index", "Login");  //đổi danh tính thất bại
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
        /// lấy địa chỉ chuyển hướng qua ủy quyền thống nhất
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
                    addr = null;    //coi lànull
                }
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), data = addr }.ToJson());
        }

        /// <summary>
        /// khởi động trước đăng nhập ủy quyền thống nhất
        /// </summary>
        /// <param name="account"></param>
        /// <param name="organizeId"></param>
        /// <param name="configOrganizeId">khởi động trước cấu hình tham số động</param>
        /// <param name="outPatientRegistrationQueryPreOrganizeId">tra cứu bệnh nhân ngoại trúAPI khởi động trước</param>
        /// <returns></returns>
        public ActionResult UALoginPre(string account, string organizeId, string configOrganizeId
            , string outPatientRegistrationQueryPreOrganizeId)
        {
            if (!string.IsNullOrWhiteSpace(configOrganizeId))
            {
                //cấu hình tham số động của hệ thống
                _sysConfigRepo.GetByCode("any_any_any_any", configOrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(account) && !string.IsNullOrWhiteSpace(organizeId))
            {
                //mô phỏng đăng nhập
                QuickLoginSuccess(account, organizeId, returnUrl: "null");
            }
            if (!string.IsNullOrWhiteSpace(outPatientRegistrationQueryPreOrganizeId))
            {
                //tra cứu bệnh nhân ngoại trú
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
            //loại bỏSSOdấu nguồn đăng nhập
            WebHelper.RemoveCookie(Constants.AppId + "_LoginFromFlag");

            return base.Index();
        }
        /// <summary>
        /// đăng xuất
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
                //thoátSSOĐăng nhập
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