using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;

namespace NewtouchHIS.Framework.Web.Controllers
{
    /// <summary>
    /// đã đăng nhập Controllerlớp cơ sở
    /// </summary>
    public abstract class BaseController : Controller
    {

        private OperatorModel? _userIdentity;
        /// <summary>
        /// danh tính người dùng đăng nhập
        /// </summary>
        protected internal OperatorModel? UserIdentity
        {
            get
            {
                if (_userIdentity == null)
                {
                    _userIdentity = OperatorProvider.GetCurrent();
                }
                return _userIdentity;
            }
        }

        /// <summary>
        /// trang danh sách
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public virtual IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// trang biểu mẫu
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// trả vềSuccess
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual IActionResult Success(string? message = null)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }

        /// <summary>
        /// trả vềSuccess
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual IActionResult Success(string? message, object? data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }

        /// <summary>
        /// trả vềError
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual IActionResult Error(string? message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}
