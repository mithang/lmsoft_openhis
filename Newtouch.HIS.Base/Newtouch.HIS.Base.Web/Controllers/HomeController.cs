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
            var cookieLoginFlag = HttpWebHelper.GetCookie(Constants.AppId + "_" + OperatorProvider.GetCurrent().UserCode + "_" + "LoginFlag");
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
        /// 获取就诊人数（门诊记账、住院记账）
        /// </summary>
        public IActionResult GetVisitNum()
        {
            VisitNumBO BO = null;
            var userCode = OperatorProvider.GetCurrent().UserCode;
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
                var orgId = OperatorProvider.GetCurrent().OrganizeId;
                if (string.IsNullOrEmpty(orgId))
                {
                    throw new FailedCodeException("SYS_GET_ORGANIZATIONAL_FAILURE");
                }
                BO = _commonDmnService.GetVisitNum(false, orgId);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = BO }.ToJson());
        }

        /// <summary>
        /// 获取业务字段的随机产生值（自增+Format）
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
                    orgId = orgIdIsStar.Value ? "*" : OperatorProvider.GetCurrent().OrganizeId;
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
            //    value = null;   //???????????????这样合适么
            //}
            else
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, topOrgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

    }
}