using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using System.Reflection;

namespace NewtouchHIS.Framework.Web.Controllers
{
    public abstract class LoginBaseController : Controller
    {
        private readonly ILoginAppService _loginApp;
        private readonly ISysUserVDmnService _sysUserVDmn;
        public LoginBaseController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn)
        {
            _loginApp = loginApp;
            _sysUserVDmn = sysUserVDmn;
        }
        /// <summary>
        /// overridekhi，vui lòng cuối cùngreturn base.Index();
        /// </summary>
        public virtual IActionResult Index()
        {
            //ghi vào máy kháchId
            HttpWebHelper.WriteCookie(ConfigInitHelper.SysConfig.AppId + "_ClientId", Guid.NewGuid().ToString("N").ToLower());

            //có cần mã xác minh hay không
            ViewBag.IsCheckChkCode = ConfigInitHelper.SysConfig.Is_CheckChkCode;

            return View();
        }

        /// <summary>
        /// lấy mã xác minh
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult GetAuthCode()
        {
            var clientId = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_ClientId");
            string code;
            CommonTools gen = new CommonTools();
            var stream = gen.GetVerifyCode(out code);

            //mã xác minh 5có hiệu lực trong vài phút
            RedisHelper.SetString(clientId + "_chkCode", code, new TimeSpan(0, 5, 0));

            return File(stream, @"image/Gif");
        }

        /// <summary>
        /// xác minh tính đúng đắn của mã xác minh
        /// </summary>
        /// <param name="code"></param>
        /// <returns>mã xác minh đúng trả vềtrue，nếu không thì trả vềfalse</returns>
        [NonAction]
        public virtual bool CheckChkCode(string code)
        {
            if (ConfigInitHelper.SysConfig.Is_CheckChkCode ?? true)
            {
                //mã xác minh đăng nhập xác minh
                var clientId = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_ClientId");
                var chkCode = RedisHelper.GetString(clientId + "_chkCode");
                if (string.IsNullOrWhiteSpace(chkCode) || code.ToLower() != chkCode.ToLower())
                {
                    return false;
                }
            }
            return true;
        }

        [HttpPost]
        [HandlerAjaxOnly]
        public async Task<IActionResult> CheckLogin(string username, string password, string code)
        {
            if (!CheckChkCode(code))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Mã xác minh sai, vui lòng nhập lại" }.ToJson());
            }
            var logReq = new LoginBO { Username = username, Password = password };
            var logResult = await _loginApp.CheckPwdAsync(logReq);
            if (logResult == null || string.IsNullOrWhiteSpace(logResult.Id))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Thông tin người dùng bất thường" }.ToJson());
            }
            //chỉ dùng làmtokenđổi lấy
            OperatorModel oprInit = new OperatorModel
            {
                UserCode = logResult.Account,
                UserName = logResult.Account,
                TopOrganizeId = logResult.TopOrganizeId ?? ConfigInitHelper.SysConfig.Top_OrganizeId,
            };
            var userOpr = await _sysUserVDmn.BuildLoginStatusOpr(logResult.Id);
            if (userOpr == null || string.IsNullOrWhiteSpace(userOpr?.UserCode))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Khởi tạo dữ liệu người dùng bất thường" }.ToJson());
            }
            HttpWebHelper.WriteCookie(ConfigInitHelper.SysConfig.AppId + "_" + "CookieKey_CurUserCode", userOpr.UserCode);
            OprBeforeSave(userOpr);
            OperatorProvider.AddCurrent(userOpr);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "Đăng nhập thành công" }.ToJson());
        }

        /// <summary>
        /// danh tính đăng nhập xử lý lại trước khi lưu
        /// </summary>
        /// <param name="opr"></param>
        /// <returns></returns>
        public virtual void OprBeforeSave(OperatorModel opr)
        {

        }

        /// <summary>
        /// thoát đăng nhập
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OutLogin()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                //this.WriteOutLoginLog();

                OperatorProvider.RemoveCurrent();
            }
            //Session.Abandon();
            //Session.Clear();

            var loginFromFlag = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");
            if (!string.IsNullOrWhiteSpace(loginFromFlag))
            {
                HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");
                ////thoátSSOđăng nhập
                //var url = ConfigurationHelper.GetAppConfigValue("SSOOutLogin");
                //return Redirect(url);
            }

            return RedirectToAction("Index", "Login");
        }
        #region đăng nhập ủy quyền thống nhất
        /// <summary>
        /// lấy địa chỉ chuyển hướng qua ủy quyền thống nhất
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ActionResult GetUALoginAddres(string appId)
        {
            string? addr = null;
            if (!string.IsNullOrWhiteSpace(appId))
            {
                var hostName = ConfigInitHelper.SysConfig.AppAPIHostName;
                if (hostName != null)
                {

                    var app = hostName.GetType().GetPropertyList();
                    foreach (var p in app)
                    {
                        var pValue = p.GetValue(hostName);
                        if (pValue != null && pValue.ToString()?.ToLower() == appId.ToLower())
                        {
                            var host = ConfigInitHelper.SysConfig.AppAPIHost;
                            PropertyInfo propInfo = typeof(AppAPIHostConfig).GetProperty(p.Name);
                            addr = propInfo?.GetValue(host)?.ToString();
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(addr))
            {
                addr = $"{addr}{ConfigInitHelper.SysConfig.UALoginAddress}";
                if (addr.EndsWith("/"))
                {
                    addr = addr.Substring(0, addr.Length - 1);
                }
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
                    addr = null;    //được coi lànull
                }
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), data = addr }.ToJson());
        }

        #endregion
    }
}
