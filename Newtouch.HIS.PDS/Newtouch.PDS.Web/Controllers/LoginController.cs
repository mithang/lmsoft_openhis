using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.Infrastructure.Log;
using Newtouch.Infrastructure.Common;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Framework.Web.Controllers;
using Newtouch.Core.Common;
using System;
using Microsoft.AspNetCore.Mvc;
using Newtouch.Tools;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;
using Newtouch.Infrastructure.Model;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Core.Common.Utils;

using System.Diagnostics;
using Newtouch.Common;
using Newtonsoft.Json.Linq;
using Newtouch.Common.Web;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.HIS.Web.Controllers
{
    /// <summary>
    /// Đăng nhập
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysUserRepo _sysUserRepo;

        public LoginController(NewtouchHIS.Framework.Web.Implementation.ILoginAppService loginApp,
            NewtouchHIS.Base.Domain.Organize.ISysUserVDmnService sysUserVDmn,
            ISysUserExDmnService sysUserDmnService,
            ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo,
            ISysUserRepo sysUserRepo) : base(loginApp, sysUserVDmn)
        {
            _sysUserDmnService = sysUserDmnService;
            _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
            _sysUserRepo = sysUserRepo;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override IActionResult Index()
        {
            //Ghi vào máy kháchId
            Response.Cookies.Append(Constants.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            ViewBag.IsCheckChkCode = ConfigurationHelper.GetAppConfigBoolValue("Is_CheckChkCode");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public new IActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            HttpContext.Session.Clear();
            HttpContext.Session.Clear();
            var userCode = "";
            if (opr != null) { userCode = opr.UserCode; }
            var cookieLoginFlag = Request.Cookies[Constants.AppId + "_" + userCode + "_" + "LoginFlag"];
            if (string.IsNullOrWhiteSpace(cookieLoginFlag))
            {
                return RedirectToAction("Index", "Login");
            }
            Response.Cookies.Delete(Constants.AppId + "_" + userCode + "_" + "LoginFlag");

            var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");  //ThoátSSOĐăng nhập

            return Redirect(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opr"></param>
        public override void OprBeforeSave(OperatorModel opr)
        {
            //Cập nhật phần nhà thuốc người dùng
            if (ConfigurationHelper.GetAppConfigBoolValue("Is_UserYfbmRelated") == true && opr.OrganizeId != null)
            {
                //Bộ phận nhà thuốc hiện cần thao tác
                if (!string.IsNullOrWhiteSpace(opr.UserId) && !string.IsNullOrWhiteSpace(opr.OrganizeId))
                {
                    opr.yfbmCodeList = _sysUserDmnService.GetYfbmCodeListByUserId(opr.UserId, opr.OrganizeId)?.ToList();
                }
                if (opr.yfbmCodeList != null && opr.yfbmCodeList.Count > 0)
                {
                    //Bộ phận nhà thuốc hiện cần thao tác    //tạiOrgIdĐã liên kết nhiềuyfbmLàm thế nào
                    var curYfbmCode = opr.yfbmCodeList.FirstOrDefault();
                    var curYfbmObj = _sysPharmacyDepartmentRepo.GetUserYfbmByCode(curYfbmCode, opr.OrganizeId);
                    if (curYfbmObj != null)
                    {
                        Constants.SetCurrentYfbm(opr.UserId, new LoginUserCurrentYfbmModel()
                        {
                            yfbmCode = curYfbmCode,
                            yfbmjb = curYfbmObj.yfbmjb,
                            mzzybz = curYfbmObj.mzzybz,
                            yfbmmc = curYfbmObj.yfbmmc
                        });
                    }
                }
                else
                {
                    Constants.SetCurrentYfbm(opr.UserId, null);  //Loại bỏ
                }
            }

            //
            Response.Cookies.Append(Constants.AppId + "_" + "CookieKey_IsHospAdministrator", opr.IsHospAdministrator.ToString().ToLower());

            //
            var rpHost = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            if (!string.IsNullOrWhiteSpace(rpHost))
            {
                Response.Cookies.Append(Constants.AppId + "_" + "ReportServer_HOST", rpHost);
            }
            else
            {
                Response.Cookies.Delete(Constants.AppId + "_" + "ReportServer_HOST");
            }

        }


        #region Đăng nhập ủy quyền thống nhất

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult UALogin(string access_token, string returnUrl)
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
                return RedirectToAction("Index", "Login");  //Chưa cấu hình đổi danh tínhAPIĐịa chỉ
            }
            //tớiAPIYêu cầu đổi danh tính
            var sw = new Stopwatch();
            sw.Start();
            var reqObj = new
            {
                Timestamp = DateTime.Now,
                AppId = Constants.AppId,
                access_token = access_token
            };
            var apiResp = Tools.Net.HttpClientHelper.HttpPostStringAndRead<APIRequestHelper.DefaultResponse>(ConfigurationHelper.GetAppConfigValue("UAUserInfoGetAPI"), Tools.Json.ToJson(reqObj), contentType: Newtouch.Core.Common.Utils.HttpClientHelper.EnumContentType.json);
            AppLogger.Info(string.Format("{0}Ủy quyền đăng nhập thống nhất，Lấy danh tínhAPIThời gian tốn yêu cầu{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
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
                AppLogger.Info(string.Format("{0}Ủy quyền đăng nhập thống nhất，QuickLoginSuccessThời gian tốn{1}ms", Constants.AppId, sw.ElapsedMilliseconds));
                sw.Stop();
            };

            return QuickLoginSuccess(account, organizeId, returnUrl: returnUrl, withToken: access_token, acBeforeRedirect: acBeforeRedirect);
        }

        /// <summary>
        /// Lấy địa chỉ chuyển hướng ủy quyền thống nhất
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public new IActionResult GetUALoginAddres(string appId)
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
                    addr = null;    //Được coi lànull
                }
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), data = addr }.ToJson());
        }


        #endregion

        private void WriteOutLoginLog()
        {
            try { AppLogger.Info(string.Format("{0}Thoát đăng nhập", Constants.AppId)); } catch { }
        }

        // SSO token-login fallback (FrameworkBase LoginController.QuickLoginSuccess is binary-only):
        // with no validated operator session, fall back to the normal login page rather than
        // minting a session from an unverified token. Documented stub, not a silent success.
        private IActionResult QuickLoginSuccess(string account, string organizeId, string returnUrl, string withToken, System.Action acBeforeRedirect)
        {
            try { acBeforeRedirect?.Invoke(); } catch { }
            if (OperatorProvider.GetCurrent() != null && !string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Login");
        }

    }
}
