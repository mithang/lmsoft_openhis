using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Operator;
using System;
using System.Collections.Generic;

namespace Newtouch.Sett.Web.Controllers
{
    /// <summary>
    /// Trang chủ — khung shell admin (SHELL-A, khôi phục từ mẫu 329 dòng gốc).
    /// Deliberately NO [HandlerLogin] here: that attribute means "skip login
    /// verification" in this framework — the global FrameworkAuthorizationFilter
    /// must enforce login, as it does for Home/Index (unauthenticated -&gt; 302).
    /// ViewBag org cấp theo mẫu HOSP (D-SHELL-002); base nhị phân
    /// FrameworkBase.MultiOrg đã mất nên không còn base.Index() để kế thừa.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysRoleShortcutMenuDmnService _sysRoleShortcutMenuDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly ICommonDmnService _commonDmnService;

        public HomeController(
            ISysOrganizeDmnService sysOrganizeDmnService,
            ISysRoleShortcutMenuDmnService sysRoleShortcutMenuDmnService,
            ISysConfigRepo sysConfigRepo,
            ICommonDmnService commonDmnService)
        {
            _sysOrganizeDmnService = sysOrganizeDmnService;
            _sysRoleShortcutMenuDmnService = sysRoleShortcutMenuDmnService;
            _sysConfigRepo = sysConfigRepo;
            _commonDmnService = commonDmnService;
        }

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

            // D-SHELL-002: cấp đủ 3 ViewBag org theo mẫu HOSP. Accessor là
            // ISysOrganizeDmnService của Sett (2 method đã port thật ở SHELL-A,
            // trước đây là stub trả []/null).
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.OrganizeId);
            }
            else
            {
                ViewBag.OrgName = _sysOrganizeDmnService.GetNameByOrgId(opr.TopOrganizeId);
            }
            if (!opr.IsRoot && !opr.IsAdministrator)
            {
                var mediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);
                ViewBag.MediOrgList = mediOrgList;
                if (mediOrgList != null && mediOrgList.Count > 1)
                {
                    var needChooseOrg = Request.Cookies[ConfigInitHelper.SysConfig.AppId + "_CookieKey_ChooseOrg"];
                    ViewBag.NeedChooseOrg = !string.IsNullOrWhiteSpace(needChooseOrg);
                }
            }
            return View();
        }

        /// <summary>
        /// Dashboard tab mặc định của khung (/Home/Default trong iframe).
        /// Gốc: HomeController.Default() cấp ShortcutMenuList + chart config.
        /// </summary>
        [HttpGet]
        public IActionResult Default()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr != null && opr.RoleIdList != null && opr.RoleIdList.Count > 0)
            {
                ViewBag.ShortcutMenuList = _sysRoleShortcutMenuDmnService.GetAuthedSCMList(opr.RoleIdList, opr.OrganizeId);
            }
            ViewBag.Metod_VisitsCountChart = _sysConfigRepo.GetValueByCode("Metod_VisitsCountChart_Outpatient", opr?.OrganizeId);
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
            ViewBag.MediOrgList = _sysOrganizeDmnService.GetMedicalOrganizeListByUserId(opr.UserId);
            return View();
        }

        /// <summary>
        /// Đổi cơ sở y tế. Triển khai đơn cơ sở (mẫu MRMS): chỉ chấp nhận đổi
        /// về đúng org hiện tại; đổi sang org khác trả lỗi thay vì im lặng.
        /// Luồng đổi org đa cơ sở đầy đủ của gốc nằm trong base nhị phân
        /// FrameworkBase.MultiOrg (SwithOrgSuccessOprBeforeSave + yfbm +
        /// UALoginPre) — base đã mất, UALoginPre đã xóa ở NX18-002 — nên
        /// không port; NeedChooseOrg=false với user 1 org nên đường này
        /// không tới được trong thực tế.
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
        /// Lấy số người khám bệnh (ngoại trú + nội trú) cho biểu đồ Default.
        /// Gốc: CommonDmnService.GetVisitNum(configmethod, orgId).
        /// </summary>
        public IActionResult GetVisitNum()
        {
            var opr = OperatorProvider.GetCurrent();
            var configMethod = _sysConfigRepo.GetValueByCode("Metod_VisitsCountChart_Outpatient", opr?.OrganizeId);
            var data = _commonDmnService.GetVisitNum(configMethod ?? "", opr?.OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        public IActionResult GetWeekNum()
        {
            var opr = OperatorProvider.GetCurrent();
            var configMethod = _sysConfigRepo.GetValueByCode("Metod_VisitsCountChart_Outpatient", opr?.OrganizeId);
            var data = _commonDmnService.GetWeekNum(configMethod ?? "", opr?.OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        public IActionResult GetLastWeek()
        {
            var data = _commonDmnService.GetLastWeek();
            return Content(data.ToJson());
        }

        public IActionResult MessageBox()
        {
            return View();
        }

        /// <summary>
        /// Port of the original HomeController.GetNewFieldUniqueValue (parity audit
        /// 2026-10-27). Called live by SystemManage/SysParameterConfig/
        /// ChargeTemplate_EditForm. Deliberately NO [HandlerLogin] here: that
        /// attribute means "skip login verification" in this framework, and the
        /// original action was login-checked — the global filter must enforce
        /// login, as it does for Home/Index (unauthenticated -> 302).
        /// WARNING: the underlying spFieldGetNewID stored procedure ADVANCES a
        /// serial-number counter (INSERT/UPDATE EntityFieldSerialNo) — calling this
        /// endpoint is a write, so it is ported but NOT live-invoked in this audit.
        /// </summary>
        public IActionResult GetNewFieldUniqueValue(string fieldName, string orgId)
        {
            var data = SettKeyHelper.GetNewFieldUniqueValue(fieldName, orgId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = "", data = data }.ToJson());
        }
    }
}
