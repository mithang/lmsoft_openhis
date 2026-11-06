using Microsoft.AspNetCore.Mvc;
using Newtouch.MRQC.Web.Models;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.DataBaseSvr;
using NewtouchHIS.Lib.Framework.Operator;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Newtouch.MRQC.Web.Controllers
{
    /// <summary>
    /// Trang chủ — khung shell admin (SHELL-A, khôi phục từ mẫu 329 dòng gốc).
    /// Deliberately NO [HandlerLogin] here: that attribute means "skip login
    /// verification" in this framework — the global FrameworkAuthorizationFilter
    /// must enforce login, as it does for Home/Index (unauthenticated -&gt; 302).
    /// MRQC không có organize service riêng nên ViewBag org cấp theo mẫu MRMS
    /// (D-SHELL-002): danh sách 1 org hiện tại, đọc tên trực tiếp từ BaseDb.
    /// </summary>
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // NX2-002: filter (không còn [HandlerLogin]) đã chặn request ẩn danh
            // trước khi vào đây. Guard dưới đây chỉ là phòng thủ.
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            var loginFromFlag = Request.Cookies[ConfigInitHelper.SysConfig.AppId + "_LoginFromFlag"];
            ViewBag.loginFromFlag = loginFromFlag;

            // D-SHELL-002 (mẫu MRMS, triển khai đơn cơ sở): chỉ org hiện tại.
            string orgId = opr?.OrganizeId ?? "";
            string orgName = orgId;
            try
            {
                var name = SqlSugarDbContext.Db.GetConnection("BaseDb").Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) orgName = name;
            }
            catch { /* fall back to org id */ }
            ViewBag.MediOrgList = new List<SysOrganizeV>
            {
                new SysOrganizeV { Id = orgId, Name = orgName }
            };
            ViewBag.OrgName = orgName;
            ViewBag.NeedChooseOrg = false;
            return View();
        }

        /// <summary>
        /// Dashboard tab mặc định của khung (/Home/Default trong iframe).
        /// Gốc MRQC Default là trang demo tĩnh (biểu đồ Chart.js số liệu mẫu +
        /// KPI/tin tức cứng của vendor) — không có dữ liệu máy chủ, chỉ return View().
        /// </summary>
        [HttpGet]
        public IActionResult Default()
        {
            return View();
        }

        /// <summary>
        /// Khung chọn cơ sở y tế (modal showSwitchOrgPrompt trong Index).
        /// </summary>
        public IActionResult UserOrgChoose()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr == null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.MediOrgList = new List<SysOrganizeV>
            {
                new SysOrganizeV { Id = opr?.OrganizeId ?? "", Name = GetOrgName(opr?.OrganizeId ?? "") }
            };
            return View();
        }

        private static string GetOrgName(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId)) return orgId;
            try
            {
                var name = SqlSugarDbContext.Db.GetConnection("BaseDb").Ado.GetString(
                    "select top 1 Name from [NewtouchHIS_Base].[dbo].[Sys_Organize] with(nolock) where Id = @id",
                    new SugarParameter("@id", orgId));
                if (!string.IsNullOrWhiteSpace(name)) return name;
            }
            catch { }
            return orgId;
        }

        /// <summary>
        /// Đổi cơ sở y tế. Triển khai đơn cơ sở (mẫu MRMS): chỉ chấp nhận đổi
        /// về đúng org hiện tại; đổi sang org khác trả lỗi thay vì im lặng.
        /// Luồng đổi org đa cơ sở đầy đủ của gốc nằm trong base nhị phân
        /// FrameworkBase.MultiOrg đã mất nên không port; NeedChooseOrg=false
        /// với user 1 org nên đường này không tới được trong thực tế.
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SwithOrg(string dstnOrgId)
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null && string.Equals(dstnOrgId, opr.OrganizeId, StringComparison.OrdinalIgnoreCase))
            {
                return Content(new { state = "success" }.ToJson());
            }
            return Content(new { state = "error", message = "Chuyển đổi cơ sở y tế khác không khả dụng trong triển khai đơn cơ sở này." }.ToJson());
        }

        public IActionResult MessageBox()
        {
            return View();
        }
    }
}
