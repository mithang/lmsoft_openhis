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

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    // D-NX27-AUDIT-002: PORT — truoc day CIS khong co SysUserController nen
    // /SystemManage/SysUser/* 404 live, du 2 caller that dang go toi:
    // (1) header "Ca nhan Thong tin" (Views/Home/_IndexHeaderNav.cshtml:23)
    // -> /SystemManage/SysUser/PersonalInfo 404 (probe live 5253);
    // (2) SysRole/Index (trang vai tro, song) mo Selector qua
    // /SystemManage/SysUser/Selector 404 (probe live 5253).
    // View Index/Roles/Selector + 3 action JSON duoc port verbatim tu
    // SysUserBaseController (Framework.Web, khong co subclass nao) theo mau
    // NX26-404 (port UpdateUserRole vao SSO): khong sua caller.
    // PersonalInfo KHONG port form doi mat khau: RevisePassword chi co o
    // HOSP/PDS (Base.Infrastructure / PDS.Infrastructure), khong co trong
    // Center/CIS, va probe co-token SPP bi cam (D-NX24-002) nen khong the
    // chung minh live duong ghi mat khau; action tra notice nhu mau EMR/MRMS/
    // Sett, ghi ro chua co trang tuong duong (ke ca tren cong SSO).
    // UpdateUserRole them [ValidateAntiForgeryToken] theo mau SSO/HOSP
    // (caller Roles.cshtml dung $.submitForm tu chen token, khong doi caller).
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
            return View();
        }

        public IActionResult PersonalInfo()
        {
            ViewBag.PageName = "Thông tin cá nhân";
            return View("~/Areas/SystemManage/Views/Shared/SysManageNotice.cshtml");
        }

        public IActionResult Roles()
        {
            ViewBag.PageName = "Vai trò người dùng";
            return View();
        }

        public IActionResult Selector()
        {
            ViewBag.PageName = "Chọn người dùng";
            return View();
        }

        // KHAC verbatim co chu dich (D-NX27-AUDIT-002): ban goc SysUserBaseController
        // nhan (OLPagination, orgId) nhung view vendor (Index.cshtml, jqGrid) post
        // page/rows/sidx/sord + OrganizeId — OLPagination chi co offset/limit (khong
        // co page/rows) nen binding truc tiep cho offset=0/limit=0 (probe live:
        // {"rows":[],"total":2}) va orgId khong bao gio bind (view post OrganizeId).
        // Theo mau HOSP GetGridJson(Pagination, keyword, OrganizeId): nhan Pagination
        // (jqGrid) roi map sang OLPagination cho app service.
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

        // KHAC verbatim: view Selector.cshtml post organizeId + keyValue (ma vai tro
        // de pre-check thanh vien), khong phai orgId/roleId — doi ten param cho
        // dung view; orgId khong duoc service call dung (service lay org tu envelope).
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
