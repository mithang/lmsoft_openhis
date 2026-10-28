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

namespace HIS.SSO.Areas.SysManage.Controllers
{
    [Area("SysManage")]
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
        public async Task<ActionResult> GetRoleList(string orgId,string userId)
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

            var data = new
            {
                rows = await _sysRoleDmnService.GetPagintionList(OrganizeId, keyword),
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
        public ActionResult submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            _userRoleAuthDmnService.submitRoleUser(roleId, userList, UserIdentity.UserCode);
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
