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
using Newtonsoft.Json;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    /// <summary>
    /// Login via LoginBaseController (same as the MRQC pilot): the Framework
    /// LoginController is replaced, not ported; SSO/UA callback actions from
    /// the Framework version are dropped (the net6 SSO host 5208 exposes no
    /// GetOpenId endpoint, so SSOCallback + the no-arg CheckLogin SSO-callback
    /// path have nothing to call; SSO login in the port goes through SSO +
    /// CheckLogin POST -> BaseAPI). CheckLogin POST, GetAuthCode and OutLogin
    /// are inherited from LoginBaseController.
    /// ReturnSSOHome IS ported (audit 2026-10-27): Views/Home/Index links to
    /// it for SSO-origin logins, but the action was missing (HTTP 404).
    /// GetWebClientIp IS ported: Core equivalent of the Framework
    /// ServerVariables lookup (X-Forwarded-For else RemoteIpAddress).
    /// Core login goes through LoginBaseController.CheckLogin -> BaseAPI.
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
            //Loại bỏSSODấu nguồn đăng nhập
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }

        /// <summary>
        /// Đăng nhập ủy quyền thống nhất (tile SSO → GetUALoginAddres →
        /// /Login/UALogin?access_token=…&amp;returnUrl=…).
        /// NX2-002: KHÁC CIS/EMR/OR — các portal đó đổi token qua
        /// UAUserInfoGetAPI ngoài (không cấu hình trong triển khai này nên
        /// nhánh đó chết → về trang Login; tile via-SSO của chúng cũng chết
        /// theo). Ở đây access_token CHÍNH LÀ khóa phiên SSO trong Redis
        /// dùng chung (Db 0) — cùng kho mà WriteLoginUserCookie() (?appId=)
        /// của FrameworkAuthorizationFilter dùng — nên kiểm chứng trực tiếp:
        /// token còn hiệu lực trong Redis và JSON chứa UserId/UserCode thì
        /// mint phiên HOSP bằng AddCurrent rồi về returnUrl. Không gọi API
        /// ngoài, không tạo identity từ token chưa kiểm chứng: token hết hạn
        /// / không có trong Redis / JSON rỗng → về trang Login. Nhánh đầu
        /// (đã có phiên HOSP trùng token) mirror CIS.
        /// </summary>
        public IActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login"); // Thiếu token
            }

            var eOpr = OperatorProvider.GetCurrent();
            if (eOpr != null && eOpr.token == access_token)
            {
                if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
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
                return RedirectToAction("Index", "Login"); // Token hết hạn/không hợp lệ
            }

            OperatorProvider.AddCurrent(opr);
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Quay lại trang chủ đăng nhập một lần (bản gốc: ghi log thoát, xóa
        /// cookie LoginFlag, chuyển tới SSOHomeHost đã cấu hình).
        /// </summary>
        [HttpGet]
        public IActionResult ReturnSSOHome()
        {
            //Tương đương với việc thực hiện thao tác thoát trước
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                OperatorProvider.RemoveCurrent();
                HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_" + opr.UserCode + "_" + "LoginFlag");
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

        /// <summary>
        /// Lấy IP của Web client (bản gốc đọc ServerVariables
        /// HTTP_X_FORWARDED_FOR rồi REMOTE_ADDR).
        /// </summary>
        public string GetWebClientIp()
        {
            var forwarded = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
            if (!string.IsNullOrEmpty(forwarded))
            {
                return forwarded;
            }
            var remote = HttpContext.Connection.RemoteIpAddress;
            if (remote != null)
            {
                return remote.ToString();
            }
            return string.Empty;
        }
    }
}
