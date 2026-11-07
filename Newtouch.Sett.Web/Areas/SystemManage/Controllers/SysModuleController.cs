using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Framework.Web.Controllers.SysManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{
    // NX7-001: port that — ban goc vendor la HIS.SSO SysModuleController
    // (Areas/SysManage, cung cay, da dich tieng Viet). Truoc day Sett khong
    // co controller nay nen /SystemManage/SysModule/Index 404 du menu DB co
    // row (NX6-003 ghi la vendor gap vi chi tim o Sett; that ra ban goc nam
    // o SSO). Chi doi namespace + [Area("SystemManage")]; logic giu nguyen,
    // tru 2 diem thich nghi co ghi chu ro o duoi. Form.cshtml khoi phuc tu
    // git 16d37a4f^ (ban truoc khi NX5-002 xoa).
    [Area("SystemManage")]
    public class SysModuleController : SysModuleBaseController
    {
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        public SysModuleController(ISysModuleDmnService sysModuleDmnService, ISysConfigDmnService sysConfigDmn, IRoleAuthorizeApp roleAuthorizeApp)
            : base(sysModuleDmnService, sysConfigDmn)
        {
            _roleAuthorizeApp = roleAuthorizeApp;
        }

        public override IActionResult Index()
        {
            // Thich nghi NX7-001: ban SSO tra View(userModel) vi view SSO
            // khai @model HIS.SSO.Models.UserModel va dung Model.SyncAuthed /
            // Model.OrganizeId. View Sett khong co @model ma doc truc tiep
            // OperatorProvider.GetCurrent() (opr), nen tra View() la du.
            return View();
        }
        /// <summary>
        /// cây quyền hệ thống (menu+nút)
        /// </summary>
        /// <param name="roleId">vai trò hiện tại Id</param>
        /// <returns></returns>
        public async Task<ActionResult> GetPermissionTree(string roleId)
        {
            //và menu có hiệu lực
            var moduledata = await _sysModuleDmnService.GetEntitybyorgId(this.OrganizeId);
            var authorizedata = new List<SysRoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = await _roleAuthorizeApp.GetValidList(roleId);
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysModuleEntity item in moduledata)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = moduledata.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.Id);
                tree.hasChildren = true;
                tree.img = item.Icon == "" ? "" : item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        // NX7-001: KHONG port SyncForm/SubmitMenuSync cua ban SSO. Ly do:
        // khong co caller/view nao o Sett goi dong bo menu (Sett SysModule/
        // Index khong co nut dong bo; view SyncForm.cshtml cung khong co o
        // Sett) va SubmitMenuSync la duong ghi tao/sua menu — dong bo menu
        // van la viec cua cong SSO. GetPermissionTree (chi doc) thi giu.
    }
}
