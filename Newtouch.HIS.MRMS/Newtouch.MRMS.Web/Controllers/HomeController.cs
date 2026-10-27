using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using Newtouch.MR.ManageSystem.Infrastructure;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.MRMS.Web.Models;

namespace Newtouch.MRMS.Web.Controllers
{
    /// <summary>
    /// Portal home (replaces the binary FrameworkBase HomeController base).
    /// Single-org deployment: the org list contains only the current org, so
    /// the switch-org prompt never triggers; SwithOrg to another org is an
    /// explicit error rather than a silent no-op.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ISysConfigRepo _sysConfigRepo;

        public HomeController(ISysConfigRepo sysConfigRepo)
        {
            _sysConfigRepo = sysConfigRepo;
        }

        public IActionResult Index()
        {
            var opr = OperatorProvider.GetCurrent();
            string orgId = opr?.OrganizeId ?? "";
            string orgName = orgId;
            try
            {
                var name = MrmsDb.GetBaseConnection().Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SqlSugar.SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            ViewBag.MediOrgList = new List<MrmsOrgItem>
            {
                new MrmsOrgItem { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return View();
        }

        public IActionResult Default()
        {
            return View();
        }

        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            ViewBag.MediOrgList = new List<MrmsOrgItem>
            {
                new MrmsOrgItem { Id = opr?.OrganizeId ?? "", Name = ViewBag.OrgName as string ?? opr?.OrganizeId ?? "" }
            };
            return View();
        }

        public IActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null && string.Equals(dstnOrgId, opr.OrganizeId, StringComparison.OrdinalIgnoreCase))
            {
                return Content(new { state = "success" }.ToJson());
            }
            return Content(new { state = "error", message = "Chuyển đổi cơ sở y tế khác không khả dụng trong triển khai đơn cơ sở này." }.ToJson());
        }

        /// <summary>
        /// Sync system parameters into the current org (same semantics as the
        /// Framework original: copies all template rows with fresh ids).
        /// NOTE: this writes Sys_Config rows for the target org — it is the
        /// original feature, kept behind the explicit SyncSysConfigParams call.
        /// </summary>
        public IActionResult SyncSysConfigParams(string orgId)
        {
            var sysConfigEntities = _sysConfigRepo.GetList("", "*").ToList();
            foreach (var item in sysConfigEntities)
            {
                item.Id = Guid.NewGuid().ToString();
                item.OrganizeId = orgId;
            }

            var insert = _sysConfigRepo.Insert(sysConfigEntities);
            return Content(new { state = "success", message = "", data = insert }.ToJson());
        }
    }
}
