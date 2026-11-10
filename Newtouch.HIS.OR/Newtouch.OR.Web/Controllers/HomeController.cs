using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.OR.ManageSystem.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Đồng bộ cấu hình tham số hệ thống. NOTE: in this port SysConfigRepo reads AND writes
        /// NewtouchHIS_Base..Sys_GlobalConfig (the Framework repo's separate
        /// target database is not available here). The demo Base table is
        /// empty (0 rows), so this is currently a no-op returning 0.
        /// G24 (theo mau G22 MRMS, nhung Sys_GlobalConfig KHONG co cot
        /// OrganizeId nen khong the gan org nhu MRMS — dedup theo Code):
        /// (1) orgId rong thi loi ro rang thay vi Success gia data=0;
        /// (2) chi insert dong mau chua co o Base STG (dedup theo Code),
        /// chay lai khong tao trung; (3) tra data = so dong insert that.
        /// Nhanh copy/dedup chua chung minh live duoc vi demo Base co 0 dong
        /// mau — xem PROBE-G24 §1.4.
        /// </summary>
        public IActionResult SyncSysConfigParams(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return Content(new { state = "error", message = "Vui lòng chọn tổ chức", data = (object)null }.ToJson());
            }
            var templateEntities = _sysConfigRepo.GetList("", "*");
            var existingCodes = new HashSet<string>(
                _sysConfigRepo.IQueryable().Where(e => e.zt == "1").Select(e => e.Code).ToList());
            var filtered = templateEntities.Where(e => e.Code != null && !existingCodes.Contains(e.Code)).ToList();
            foreach (var item in filtered)
            {
                item.Id = Guid.NewGuid().ToString();
            }

            var insert = filtered.Count == 0 ? 0 : _sysConfigRepo.Insert(filtered);
            return Content(new { state = "success", message = "", data = insert }.ToJson());
        }
    }
}
