using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Operator;
using Newtonsoft.Json;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// đăng ghi đăng ra (SSO/UA callback actions from the Framework version are dropped:
    /// they depend on external SSO/UA hosts that do not exist in this deployment.
    /// NX16-001: UALogin IS now ported (HOSP pattern, NX2-002) — mint Redis-truc-tiep;
    /// other SSO/UA callbacks stay dropped.
    /// Core login goes through LoginBaseController.CheckLogin -> BaseAPI.)
    /// </summary>
    [HandlerLogin]
    public class LoginController : LoginBaseController
    {
        public LoginController(ILoginAppService loginApp, ISysUserVDmnService sysUserVDmn) : base(loginApp, sysUserVDmn)
        {
        }

        public override void OprBeforeSave(OperatorModel opr)
        {
        }

        [HttpGet]
        public override IActionResult Index()
        {
            //gỡ SSO đăng ghi đến nguồn tiêu ghi
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }

        /// <summary>
        /// Dang nhap uy quyen thong nhat (tile SSO -> GetUALoginAddres ->
        /// /Login/UALogin?access_token=...&returnUrl=...). NX16-001: port mau
        /// HOSP (NX2-002): access_token CHINH LA khoa phien SSO trong Redis
        /// dung chung (Db 0) nen kiem chung truc tiep: token con hieu luc
        /// trong Redis va JSON chua UserId/UserCode thi mint phien bang
        /// AddCurrent roi ve returnUrl. Khong goi API ngoai, khong tao
        /// identity tu token chua kiem chung: token het han / khong co trong
        /// Redis / JSON rong -> ve trang Login.
        /// </summary>
        public IActionResult UALogin(string access_token, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(access_token))
            {
                return RedirectToAction("Index", "Login"); // Thieu token
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
                return RedirectToAction("Index", "Login"); // Token het han/khong hop le
            }

            OperatorProvider.AddCurrent(opr);
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
