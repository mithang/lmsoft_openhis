using Newtouch.HIS.Base.SSO.App_Start;
using Newtouch.HIS.Domain.IDomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.SSO.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly ISysUserDmnService _sysUserDmnService;
        public UserController(ISysUserDmnService sysUserDmnService)
        {
            this._sysUserDmnService = sysUserDmnService;
        }

        // GET: User
        /// <summary>
        /// Thông tin cá nhân
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalInfo()
        {
            return View();
        }

        /// <summary>
        /// Gửi để đặt lại mật khẩu cá nhân
        /// </summary>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SubmitPersonalPassword(string newpwd)
        {
            _sysUserDmnService.RevisePassword(newpwd, this.UserIdentity.UserId);
            return Success("重置密码成功。");
        }
    }
}