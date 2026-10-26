using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.CIS.Web
{
    /// <summary>
    /// ControllerLớp cơ sở
    /// </summary>
    [HandlerLogin]
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        protected OperatorModel UserIdentity;

        /// <summary>
        /// 
        /// </summary>
        protected string OrganizeId
        {
            get
            {
                if (this.UserIdentity == null)
                {
                    return null;
                }
                return this.UserIdentity.OrganizeId;
            }
        }

        public ControllerBase()
        {
            this.UserIdentity = OperatorProvider.GetCurrent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize]
        public virtual ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize]
        public virtual ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[HandlerAuthorize]
        public virtual ActionResult Details()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message = null)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }



        /// <summary>
        /// Lấy quyềnOrg
        /// </summary>
        /// <param name="throwFailedException"></param>
        /// <returns></returns>
        [NonAction]
        protected string GetAuthOrganizeId(bool throwFailedException = true)
        {
            var opr = Common.Operator.OperatorProvider.GetCurrent();
            string orgId = null;
            if (opr.IsAdministrator || opr.IsRoot)
            {
                orgId = Infrastructure.Constants.TopOrganizeId; //Nếu là quản trị viên hệ thống thì để nó lấy tất cả dữ liệu tổ chức
            }
            else
            {
                orgId = Common.Operator.OperatorProvider.GetCurrent().OrganizeId;   //Mặc định dùng của người dùng liên kết hiện tạiOrganizeId
            }
            if (throwFailedException && string.IsNullOrWhiteSpace(orgId))
            {
                throw new Common.Exceptions.FailedException("定位当前权限内的组织机构失败");
            }
            return orgId;
        }

    }

}
