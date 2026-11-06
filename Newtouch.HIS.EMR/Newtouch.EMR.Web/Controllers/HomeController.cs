using Microsoft.AspNetCore.Mvc;
using Newtouch.EMR.Web.Models;
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

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Home landing page + the two Home actions the EMR views call.
    /// SHELL-C (S5-7, D-SHELL-002): the portal frame is INTACT (Index 357
    /// lines, shell + 4 partials); Index() now supplies the 3 org ViewBags
    /// the shell reads (single-org MRQC pattern: org name from BaseDb via
    /// SqlSugarDbContext; EMR has no organize service) + loginFromFlag as
    /// in the original; UserOrgChoose()/SwithOrg() added (view + header
    /// prompt already call them, both actions were missing -> 404).
    /// Note: the original HomeController declared _sysConfigRepo/_sysUserDmnService fields
    /// with no constructor, so the original SyncSysConfigParams could never run; the
    /// Core version implements it directly on the Base DB instead.
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.loginFromFlag = HttpWebHelper.GetCookie(ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag") ?? "UNKNOWN";
            string orgId = opr.OrganizeId ?? "";
            string orgName = GetOrgName(orgId);
            ViewBag.MediOrgList = new List<SysOrganizeV>
            {
                new SysOrganizeV { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return View();
        }

        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            string orgId = opr?.OrganizeId ?? "";
            ViewBag.MediOrgList = new List<SysOrganizeV>
            {
                new SysOrganizeV { Id = orgId, Name = GetOrgName(orgId) }
            };
            return View();
        }

        /// <summary>
        /// Doi org hep don-org (mau MRMS/Sett/MRQC/CIS): chi chap nhan doi ve
        /// dung org hien tai; doi sang org khac tra loi (luong da-org day du
        /// nam trong base nhi phan FrameworkBase.MultiOrg da mat).
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
        /// Root users' default tab (see Views/Home/_IndexContentWrapper.cshtml:
        /// defaultPage is "/Home/Default" when Operator IsRoot). The view is a
        /// standalone dashboard needing no model; previously the missing action
        /// made the root default tab 404. Added in the redesign program.
        /// </summary>
        public IActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// Business-field auto value (increment + format), via stored procedure spFieldGetNewID.
        /// Called by the template Form views.
        /// </summary>
        public IActionResult GetNewFieldUniqueValue(string fieldName, string initFormat = "", int initFieldLength = 0)
        {
            string orgId = null;
            try { orgId = NewtouchHIS.Lib.Framework.Operator.OperatorProvider.GetCurrent()?.OrganizeId; } catch { }
            if (string.IsNullOrWhiteSpace(initFormat) && initFieldLength > 0)
            {
                initFormat = "{0:D" + initFieldLength + "}";
            }
            string value = null;
            if (!string.IsNullOrWhiteSpace(orgId) && initFormat != null)
            {
                value = FieldValueHelper.GetNewFieldUniqueValue(fieldName, orgId, initFormat);
            }
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = value }.ToJson());
        }

        /// <summary>
        /// Sync system-config parameters: copy base ("*") rows that the org does not
        /// already have, under new ids for this org. Called by SysConfig/Index.
        /// Implemented directly on the EMR DB (Sys_Config lives in Newtouch_EMR,
        /// not in NewtouchHIS_Base) since no Core ISysConfigRepo exists.
        /// </summary>
        public IActionResult SyncSysConfigParams(string orgId)
        {
            var db = SqlSugarDbContext.Db.GetConnection("EmrDb");
            var baseEntities = db.Queryable<SysConfigRow>().Where(r => r.OrganizeId == "*").ToList();
            var orgEntities = db.Queryable<SysConfigRow>().Where(r => r.OrganizeId == orgId).ToList();
            var orgCodes = new HashSet<string>(orgEntities.Select(e => e.Code));
            var filtered = baseEntities.Where(e => !orgCodes.Contains(e.Code)).ToList();
            foreach (var item in filtered)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }
            var inserted = filtered.Count == 0 ? 0 : db.Insertable(filtered).ExecuteCommand();
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "", data = inserted }.ToJson());
        }

        [SugarTable("Sys_Config")]
        public class SysConfigRow
        {
            [SugarColumn(IsPrimaryKey = true)]
            public string Id { get; set; }
            public string OrganizeId { get; set; }
            public string Code { get; set; }
        }
    }
}
