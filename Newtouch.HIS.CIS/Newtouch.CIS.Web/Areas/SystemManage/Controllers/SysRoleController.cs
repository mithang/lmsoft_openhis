using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.Entity.SysManage;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Model;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newtouch.CIS.Web.Areas.SystemManage.Controllers
{
    // NX17-001: port tu Newtouch.Sett.Web SysRoleController (ban nay chinh la
    // port NX7-001 cua HIS.SSO SysRoleController, Areas/SysManage — da dich
    // tieng Viet + sua QA-phase2/FA-001: GetRoleList dung anonymous object cho
    // wdtree, GetGridJson loc keyword + phan trang tai controller, GetFormJson
    // tra "null" khi keyValue rong, SubmitForm/DeleteForm await that).
    // Truoc day CIS khong co controller nay nen /SystemManage/SysRole/Index 404
    // du menu DB (Newtouch_CIS.Sys_Module) co row "Quan ly vai tro" va view
    // Index/Form da co san goi GetGridJson/GetFormJson/SubmitForm/DeleteForm.
    // Vendor goc cung thieu controller nay o CIS (chi co view + menu) — day la
    // vendor gap o tang controller, khong phai view mo coi (khong ap dung
    // tien le xoa D-BA007: co menu row + view goi action that). Quyet dinh
    // D-NX17-001: PORT, khong xoa view. Chi doi namespace + Area; logic
    // action giu nguyen. Dich vu ISysRoleDmnService/IUserRoleAuthDmnService
    // doc UnionDb (52 role) nhu o Sett/SSO — CIS MainDB cung la UnionDb.
    [Area("SystemManage")]
    public class SysRoleController : OrgControllerBase
    {

        private readonly ISysRoleDmnService _sysRoleDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        public SysRoleController(ISysRoleDmnService sysRoleDmnService, IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            _sysRoleDmnService = sysRoleDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
        }
        public override IActionResult Index()
        {
            //ViewBag.OrganizeId = UserIdentity?.OrganizeId;
            //ViewBag.TopOrganizeId = UserIdentity?.TopOrganizeId;
            return View();
        }
        [HandlerAjaxOnly]
        public async Task<ActionResult> GetRoleList(string orgId, string userId)
        {
            var roleList = await _sysRoleDmnService.GetRoleList(orgId);
            if (roleList == null || roleList.Count == 0)
            {
                return Content(new List<BsTreeSelectModel>().ToJson());
            }
            var userRoles = (await _sysRoleDmnService.GetUserRoleList(userId, orgId))!.Select(p => p.Id).ToList();
            // NOTE: the page renders with js/wdtree/tree.js (bootstrap-treeview is
            // commented out in Views/Shared/_Form.cshtml). wdtree's buildnode()
            // calls nd.id.replace(...), so a node without a string id throws
            // "nd.id is undefined" and the tree renders empty. BsTreeSelectModel
            // has no id field, so nodes are emitted as anonymous objects here:
            // id/href carry the same role id, showcheck + checkstate are wdtree's
            // native checkbox fields (selected is kept for existing readers).
            // No permission logic changed: selection still comes from userRoles.
            var treeList = roleList.Select(x =>
            {
                var isSelected = userRoles.Any(y => y == x.Id);
                return new
                {
                    id = x.Id,
                    href = x.Id,
                    text = x.Name,
                    selected = isSelected,
                    showcheck = true,
                    checkstate = isSelected ? 1 : 0
                };
            }).ToList();
            return Content(treeList.ToJson());
        }
        public async Task<ActionResult> GetGridJson(Pagination pagination, string keyword, string OrganizeId)
        {
            pagination ??= new Pagination();
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                OrganizeId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;    //không trả về
            }

            // FA-001: GetPagintionList bỏ qua keyword và không điền
            // pagination.records, nên ô tìm kiếm không lọc và footer lưới
            // luôn báo "trên tổng số 0 trang". Lọc + đếm + cắt trang tại
            // controller (chỉ action này gọi service đó theo cách này);
            // thứ tự hàng giữ nguyên như service trả về.
            var list = await _sysRoleDmnService.GetPagintionList(OrganizeId, keyword) ?? new List<SysRoleEntity>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                list = list.Where(p => (p.Name != null && p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    || (p.Code != null && p.Code.Contains(keyword, StringComparison.OrdinalIgnoreCase))).ToList();
            }
            pagination.records = list.Count;
            var rows = list;
            if (pagination.page.HasValue && pagination.rows.HasValue && pagination.page.Value > 0 && pagination.rows.Value > 0)
            {
                rows = list.Skip((pagination.page.Value - 1) * pagination.rows.Value).Take(pagination.rows.Value).ToList();
            }
            var data = new
            {
                rows,
                pagination.total,
                pagination.page,
                pagination.records
            };
            return Content(data.ToJson());

        }

        public override IActionResult Form()
        {
            //ViewBag.OrganizeId = UserIdentity?.OrganizeId;
            //ViewBag.TopOrganizeId = UserIdentity?.TopOrganizeId;
            return View();
        }

        public async Task<ActionResult> GetFormJson(string keyValue)
        {
            // FA-001: keyValue rỗng làm service GetForm ném "Index was out
            // of range" (data[0] trên danh sách rỗng). UI chỉ gọi khi có
            // keyValue; trả "null" như GetFormJson của SysModule khi không
            // tìm thấy thực thể, thay vì lỗi 500 dạng JSON.
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Content("null");
            }
            var data = await _sysRoleDmnService.GetForm(keyValue);
            return Content(data.ToJson());
        }
        public async Task<ActionResult> SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            //chủ yếu là để có khóa chính duy nhất
            if (!string.IsNullOrEmpty(keyValue))
            {
                roleEntity.ModifiedEntity(OrganizeId, UserIdentity.UserCode, true);
                roleEntity.Id = keyValue;
            }
            else
            {
                roleEntity.NewEntity(OrganizeId, UserIdentity.UserCode);
                roleEntity.Id = Guid.NewGuid().ToString();
            }

            List<SysRoleAuthorizeEntity> roleAuthorizeEntityList = new List<SysRoleAuthorizeEntity>();
            if (permissionIds != null && permissionIds != "")
            {
                foreach (var itemId in permissionIds.Split(','))
                {
                    SysRoleAuthorizeEntity roleAuthorizeEntity = new SysRoleAuthorizeEntity();
                    roleAuthorizeEntity.NewEntity(UserIdentity.UserCode);
                    roleAuthorizeEntity.Id = Guid.NewGuid().ToString();
                    roleAuthorizeEntity.RoleId = roleEntity.Id;
                    roleAuthorizeEntity.ItemId = itemId.ToString();
                    roleAuthorizeEntity.zt = "1";   //luôn luôn1 có hiệu lực
                    roleAuthorizeEntityList.Add(roleAuthorizeEntity);
                }
            }
            // QA-phase2: await de client nhan ket qua that sau khi DB commit
            // (truoc day fire-and-forget: tra success truoc khi task chay xong).
            await _sysRoleDmnService.SubmitForm(roleEntity, roleAuthorizeEntityList, keyValue, UserIdentity.UserCode, OrganizeId);
            return (ActionResult)Success("Thao tác thành công.");
        }
        /// <summary>
        /// lưu quan hệ liên kết vai trò - người dùng
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<ActionResult> submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            await _userRoleAuthDmnService.submitRoleUser(roleId, userList, UserIdentity.UserCode);
            return (ActionResult)Success("Thao tác thành công");
        }
        public async Task<ActionResult> DeleteForm(string keyValue)
        {
            // QA-phase2: await (xem SubmitForm).
            await _sysRoleDmnService.DeleteForm(keyValue);
            return (ActionResult)Success("Xóa thành công.");
        }
    }
}
