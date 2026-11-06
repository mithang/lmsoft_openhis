using Microsoft.AspNetCore.Mvc;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ViewModels;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Utilities;
using NewtouchHIS.Lib.DataBaseSvr;
using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.CIS.Web.Controllers
{
    /// <summary>
    /// CIS portal home (SHELL-C: khung shell goc 329 dong da khoi phuc tren
    /// Views/Home/Index.cshtml, tham chieu lai 4 partial _Index* mo coi +
    /// _FrontReference/_BrowseAdaptiveSupport; Default.cshtml 17 dong giu
    /// nguyen nhu goc + action Default() theo D-SHELL-003).
    /// ViewBag org theo D-SHELL-002 (mau HOSP/MRMS don-org): CIS khong co
    /// organize service day du (ISysOrganizeDmnService chi co GetCodeByOrgId)
    /// nen ten org doc truc tiep tu BaseDb nhu MRQC (SqlSugarDbContext).
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
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            // Goc CIS: Index() chi set loginFromFlag roi base.Index() (base nhi
            // phan cap ViewBag org). Port tu cap du 3 ViewBag theo D-SHELL-002.
            ViewBag.loginFromFlag = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag") ?? "UNKNOWN";
            string orgId = opr.OrganizeId ?? "";
            string orgName = GetOrgName(orgId);
            ViewBag.MediOrgList = new List<SysOrganizeVEntity>
            {
                new SysOrganizeVEntity { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return View();
        }

        /// <summary>
        /// Tab mac dinh /Home/Default (D-SHELL-003). View 17 dong goc CIS cung
        /// la trang rong nen giu nguyen, khong bia dashboard.
        /// </summary>
        public IActionResult Default()
        {
            return View();
        }

        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            string orgId = opr?.OrganizeId ?? "";
            ViewBag.MediOrgList = new List<SysOrganizeVEntity>
            {
                new SysOrganizeVEntity { Id = orgId, Name = GetOrgName(orgId) }
            };
            return View();
        }

        /// <summary>
        /// Doi org hep don-org (mau MRMS/Sett/MRQC): chi chap nhan doi ve dung
        /// org hien tai; doi sang org khac tra loi (luong da-org day du nam
        /// trong base nhi phan FrameworkBase.MultiOrg da mat).
        /// </summary>
        public IActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null && string.Equals(dstnOrgId, opr.OrganizeId, StringComparison.OrdinalIgnoreCase))
            {
                return Content(new { state = "success" }.ToJson());
            }
            return Content(new { state = "error", message = "Chuyển đổi cơ sở y tế khác không khả dụng trong triển khai đơn cơ sở này." }.ToJson());
        }

        private static string GetOrgName(string orgId)
        {
            string orgName = orgId;
            try
            {
                var name = SqlSugarDbContext.Db.GetConnection("BaseDb").Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            return orgName;
        }

        /// <summary>
        /// Lấy giá trị ngẫu nhiên của trường nghiệp vụ (tự tăng + Format).
        /// Faithful to the original: blank org or null format yields data null.
        /// </summary>
        public IActionResult GetNewFieldUniqueValue(string fieldName, string initFormat = "", int initFieldLength = 0)
        {
            var orgId = OperatorProvider.GetCurrent()?.OrganizeId;
            // FA-005: blank fieldName reached the serial INSERT and surfaced a
            // DB exception ("Cannot insert NULL into EntityFieldName").
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "Tên trường không được để trống" }.ToJson());
            }
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
