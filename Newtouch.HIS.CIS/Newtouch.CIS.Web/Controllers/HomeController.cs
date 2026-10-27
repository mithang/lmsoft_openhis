using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// CIS landing page (the original portal frame views depended on Framework-only
    /// helpers; this is a minimal landing page linking to each Area's main page).
    /// Home actions that live views still call are ported here (parity audit
    /// 2026-10-27): GetNewFieldUniqueValue (called by SystemManage Form views),
    /// MSGQuery + NoticeReadSync (called by Views/Home/_IndexHeaderNav).
    /// SyncSysConfigParams is NOT ported: its only caller (SystemManage SysConfig
    /// page) has no controller in the original source tree either (SSO serves Sys
    /// pages in the port), the action performs INSERTs, and the ported
    /// SysGlobalConfigEntity has no OrganizeId column for the org-copy logic.
    /// IndexBeforeReturnView was a no-op override of the Framework base and has
    /// no equivalent on this plain Controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IOrderExecutionDmnService _orderExecutionDmnService;

        public HomeController(IOrderExecutionDmnService orderExecutionDmnService)
        {
            _orderExecutionDmnService = orderExecutionDmnService;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Lấy giá trị ngẫu nhiên của trường nghiệp vụ (tự tăng + Format).
        /// Faithful to the original: blank org or null format yields data null.
        /// </summary>
        public IActionResult GetNewFieldUniqueValue(string fieldName, string initFormat = "", int initFieldLength = 0)
        {
            var orgId = OperatorProvider.GetCurrent()?.OrganizeId;
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (!string.IsNullOrWhiteSpace(orgId) && initFormat != null)
            {
                value = EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue(fieldName, orgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

        /// <summary>Thông báo nhắc việc (message reminder query).</summary>
        public IActionResult MSGQuery(string gh)
        {
            var orgId = OperatorProvider.GetCurrent()?.OrganizeId;
            var data = _orderExecutionDmnService.MSGQuery(gh, orgId);
            return Content(data.ToJson());
        }

        /// <summary>Đồng bộ trạng thái đã đọc của thông báo qua API.Manage.</summary>
        public IActionResult NoticeReadSync(List<NoticeSendBase> noticeSends)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Chưa đăng nhập" }.ToJson());
            }
            var queueids = (noticeSends ?? new List<NoticeSendBase>())
                .Where(p => !string.IsNullOrWhiteSpace(p.msgid)).GroupBy(p => p.msgid).Select(p => p.Key).ToArray();
            var apiReqObj = new ApiManageRequest<NoticeJobRequest>
            {
                Data = new NoticeJobRequest
                {
                    QueueIds = queueids
                },
                AppId = AuthenManageHelper.accessAppId,
                OrganizeId = opr.OrganizeId,
            };
            var readApi = AuthenManageHelper.HttpPost<bool>(apiReqObj.ToJson(), AuthenManageHelper.NoticeReadSendApi, opr);
            if (readApi.data)
            {
                return Content(new AjaxResult { state = ResultType.success.ToString() }.ToJson());
            }
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Cập nhật trạng thái đã đọc thất bại" }.ToJson());
        }
    }
}
