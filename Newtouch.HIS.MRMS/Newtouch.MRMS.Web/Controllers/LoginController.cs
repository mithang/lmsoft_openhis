using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;

namespace Newtouch.MRMS.Web.Controllers
{
    /// <summary>
    /// Đăng nhập/Đăng xuất (SSO/UA callback actions from the Framework version are dropped:
    /// they depend on external SSO/UA hosts that do not exist in this deployment.
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
            //Xóa đánh dấu nguồn đăng nhập SSO
            HttpWebHelper.RemoveCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag");

            return base.Index();
        }

        /// <summary>
        /// Bên ngoài UA callback đăng nhập không có máy chủ tương ứng trong triển khai này, Báo lỗi rõ ràng thay vì thất bại thầm lặng.
        /// </summary>
        public IActionResult UALogin(string access_token, string returnUrl)
        {
            return Content(new { state = "error", message = "Đăng nhập UA ngoài không khả dụng trong triển khai này (không có máy chủ UA)." }.ToJson());
        }

        public IActionResult GetUALoginAddres(string appId)
        {
            return Content(new { state = "error", message = "Đăng nhập UA ngoài không khả dụng trong triển khai này (không có máy chủ UA)." }.ToJson());
        }
    }
}
