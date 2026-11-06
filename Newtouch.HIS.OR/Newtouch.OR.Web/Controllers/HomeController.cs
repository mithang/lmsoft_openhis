using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.OR.ManageSystem.Web.Models;
using System;
using System.Collections.Generic;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Trang chủ OR — khung portal shell (SHELL-B, D-SHELL-001/002/003).
    /// Index là khung shell gốc 329 dòng (skin-black, #theme-wrapper, sidebar
    /// + tab/iframe, tab mặc định /Home/Default); dashboard gốc nằm ở
    /// Default.cshtml + action Default(). Org switcher: triển khai đơn cơ sở
    /// như mẫu MRMS (chỉ 1 org hiện tại, NeedChooseOrg=false).
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
            // D-SHELL-002: cấp đủ 3 ViewBag org theo mẫu MRMS (1 org duy nhất,
            // tên đọc từ [NewtouchHIS_Base]..Sys_Organize; không bịa org).
            var opr = OperatorProvider.GetCurrent();
            string orgId = opr?.OrganizeId ?? "";
            string orgName = orgId;
            try
            {
                var name = OrDb.GetBaseConnection().Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SqlSugar.SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            ViewBag.MediOrgList = new List<OrOrgItem>
            {
                new OrOrgItem { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return View();
        }

        /// <summary>Tab mặc định của khung shell: dashboard gốc của OR.</summary>
        public IActionResult Default()
        {
            return View();
        }

        /// <summary>Trang chọn cơ sở y tế (modal đổi org của shell).</summary>
        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            ViewBag.MediOrgList = new List<OrOrgItem>
            {
                new OrOrgItem { Id = opr?.OrganizeId ?? "", Name = ViewBag.OrgName as string ?? opr?.OrganizeId ?? "" }
            };
            return View();
        }

        /// <summary>
        /// Đổi cơ sở y tế hiện tại. Triển khai đơn cơ sở (mẫu MRMS): đổi về
        /// chính org hiện tại thì success, org khác thì lỗi rõ ràng thay vì
        /// im lặng (shell chỉ hiện nút đổi khi MediOrgList.Count &gt; 1 nên
        /// nhánh lỗi không tới được với dữ liệu hiện tại).
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

        /// <summary>
        /// Đồng bộ cấu hình tham số hệ thống. NOTE: in this port SysConfigRepo reads AND writes
        /// NewtouchHIS_Base..Sys_GlobalConfig (the Framework repo's separate
        /// target database is not available here). The demo Base table is
        /// empty (0 rows), so this is currently a no-op returning 0.
        /// </summary>
        public IActionResult SyncSysConfigParams(string orgId)
        {
            var sysConfigEntities = _sysConfigRepo.GetList("", "*");
            foreach (var item in sysConfigEntities)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            var insert = _sysConfigRepo.Insert(sysConfigEntities);
            return Content(new AjaxResult
            {
                state = ResultType.success.ToString(),
                data = insert,
            }.ToJson());
        }
    }
}
