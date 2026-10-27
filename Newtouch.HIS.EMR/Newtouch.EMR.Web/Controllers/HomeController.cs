using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Home landing page + the two Home actions the EMR views call.
    /// The Framework portal frame is replaced by a minimal landing view (same as MRQC);
    /// EMR business pages are opened directly from the SSO portal menus.
    /// Note: the original HomeController declared _sysConfigRepo/_sysUserDmnService fields
    /// with no constructor, so the original SyncSysConfigParams could never run; the
    /// Core version implements it directly on the Base DB instead.
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
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
