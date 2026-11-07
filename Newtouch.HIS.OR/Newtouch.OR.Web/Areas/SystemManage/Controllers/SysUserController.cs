using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Web.Areas.SystemManage.Controllers
{
    // D-NX28-AUDIT-001: PORT — truoc day OR khong co SysUserController nen
    // /SystemManage/SysUser/* 404 live, du 1 caller that dang go toi:
    // header "Thong tin ca nhan" (Views/Home/_IndexHeaderNav.cshtml:45)
    // -> /SystemManage/SysUser/PersonalInfo 404 (probe live 5231, NX28).
    // OR khong co SysRoleController nen khong co caller Selector nhu CIS;
    // cung khong co UserController/PersonalInfo nao khac trong cay OR.
    // D-NX28-AUDIT-003: OR KHONG co view SysUser nao cua vendor (khac CIS:
    // CIS co san 4 view Index/Roles/Selector/PersonalInfo) nen ca 4 action
    // view (Index/Roles/Selector/PersonalInfo) deu tra SysManageNotice
    // thay vi return View() (se 500 vi thieu view). 3 action JSON port
    // verbatim tu SysUserBaseController theo mau D-NX27-AUDIT-002 (CIS).
    // PersonalInfo KHONG port form doi mat khau: RevisePassword chi co o
    // HOSP/PDS, khong co trong Center/OR, va probe co-token SPP bi cam
    // (D-NX24-002). UpdateUserRole them [ValidateAntiForgeryToken] theo
    // mau SSO/HOSP/CIS.
    [Area("SystemManage")]
    public class SysUserController : OrgControllerBase
    {
        private readonly ISysUserAppService _userAppService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmn;

        public SysUserController(ISysUserAppService userAppService, IUserRoleAuthDmnService userRoleAuthDmn)
        {
            _userAppService = userAppService;
            _userRoleAuthDmn = userRoleAuthDmn;
        }

        public override IActionResult Index()
        {
            ViewBag.PageName = "Người dùng hệ thống";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult PersonalInfo()
        {
            ViewBag.PageName = "Thông tin cá nhân";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult Roles()
        {
            ViewBag.PageName = "Vai trò người dùng";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult Selector()
        {
            ViewBag.PageName = "Chọn người dùng";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        // Port theo mau CIS (D-NX27-AUDIT-002): nhan Pagination (jqGrid)
        // roi map sang OLPagination cho app service (binding truc tiep
        // OLPagination cho rows=[] vi limit=0, orgId khong bind).
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetPagintionGridJson(Pagination pagination, string keyword, string organizeId)
        {
            if (string.IsNullOrWhiteSpace(organizeId))
            {
                return Error("Không xác định được tổ chức trong phạm vi quyền hiện tại");
            }
            var olPagination = new OLPagination<QueryParamsBase>
            {
                offset = ((pagination?.page ?? 1) - 1) * (pagination?.rows ?? 15),
                limit = pagination?.rows ?? 15,
                sort = pagination?.sidx,
                order = pagination?.sord,
                queryParams = new QueryParamsBase
                {
                    orgId = organizeId,
                    keyword = keyword ?? ""
                }
            };
            var userPage = await _userAppService.GetSysUserPageAsync(olPagination);
            return Content(userPage.ToJson());
        }

        // Port theo mau CIS: doi ten param theo view (organizeId/keyValue).
        public async Task<ActionResult> GetSysUserSelectorTree(string? organizeId = null, string? keyValue = null, string? from = null, bool isShowEmpty = false, bool isExpand = true, bool isContansChildOrg = true)
        {
            List<string> roleUser = new List<string>();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                roleUser = (await _userRoleAuthDmn.GetCurUserIdListByRoleId(keyValue)).Select(p => p.First).ToList();
            }
            var userTree = await _userAppService.GetOrgUserTreeWithDeptAsync(new QueryParamsBase
            {
                stringList = roleUser,
            });
            return Content(userTree.ToJson());
        }

        /// <summary>
        /// cập nhật vai trò người dùng
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <param name="roleList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(string keyValue, string organizeId, List<string> roleList)
        {
            var result = await _userRoleAuthDmn.UpdateUserRole(keyValue, organizeId, roleList, UserIdentity.UserCode);
            return result ? Success("Thao tác thành công (hiệu lực khi người dùng đăng nhập lại)") : Error("Thao tác thất bại");
        }
    }
}
