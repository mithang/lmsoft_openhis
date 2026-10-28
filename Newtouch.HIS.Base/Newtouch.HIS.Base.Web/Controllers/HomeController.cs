using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        private readonly ICommonDmnService _commonDmnService;

        public HomeController(ICommonDmnService commonDmnService)
        {
            this._commonDmnService = commonDmnService;

        }

        [HttpGet]
        public IActionResult Index()
        {
            // Home mang [HandlerLogin] nên filter không chặn request ẩn danh:
            // không có session thì về Login (mẫu Organize) thay vì NRE kèm
            // stack trace ở GetCurrent().UserCode như trước.
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            var cookieLoginFlag = HttpWebHelper.GetCookie(Constants.AppId + "_" + opr.UserCode + "_" + "LoginFlag");
            ViewBag.cookieLoginFlag = cookieLoginFlag;
            return View();
        }
        [HttpGet]
        public IActionResult Default()
        {
            return View();
        }
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        /// <summary>
        /// Lấy số lượng người khám（Ghi nợ ngoại trú、Ghi nợ nội trú）
        /// </summary>
        public IActionResult GetVisitNum()
        {
            VisitNumBO BO = null;
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại." }.ToJson());
            }
            var userCode = opr.UserCode;
            if (userCode == "root")
            {
                BO = null;
            }
            else if (userCode == "admin")
            {
                var topOrgId = Constants.TopOrganizeId;
                if (string.IsNullOrEmpty(topOrgId))
                {
                    throw new FailedCodeException("SYS_GET_TOPORGANIZATIONAL_FAILURE");

                }
                BO = _commonDmnService.GetVisitNum(true, null, topOrgId);
            }
            else
            {
                var orgId = opr.OrganizeId;
                if (string.IsNullOrEmpty(orgId))
                {
                    throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
                }
                BO = _commonDmnService.GetVisitNum(false, orgId);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        /// <summary>
        /// Lấy giá trị sinh ngẫu nhiên của trường nghiệp vụ（Tự tăng+Format）
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orgIdIsStar"></param>
        /// <param name="topOrgIdIsStar"></param>
        /// <param name="initFormat"></param>
        /// <param name="initFieldLength"></param>
        /// <returns></returns>
        public IActionResult GetNewFieldUniqueValue(string fieldName, bool? orgIdIsStar = null, bool? topOrgIdIsStar = null, string orgId = null, string topOrgId = null, string initFormat = "", int initFieldLength = 0)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                if (orgIdIsStar.HasValue)
                {
                    // Ẩn danh (GetCurrent null) thì coi như không có org:
                    // nhánh dưới trả value null như trường hợp thiếu org.
                    var opr = OperatorProvider.GetCurrent();
                    orgId = orgIdIsStar.Value ? "*" : opr?.OrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(topOrgId))
            {
                if (topOrgIdIsStar.HasValue)
                {
                    topOrgId = topOrgIdIsStar.Value ? "*" : Constants.TopOrganizeId;
                }
            }
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (string.IsNullOrWhiteSpace(orgId) || string.IsNullOrWhiteSpace(topOrgId) || initFormat == null)
            {
                value = null;
            }
            //else if(orgId == topOrgId)
            //{
            //    value = null;   //???????????????Như vậy có hợp lý không
            //}
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, topOrgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

    }
}