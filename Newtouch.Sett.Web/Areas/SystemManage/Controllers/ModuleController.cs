using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.Infrastructure;
using Newtouch.Sett.Web.Controllers;
using Newtouch.Sett.Web.Helpers;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Newtouch.Sett.Web.Areas.SystemManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class ModuleController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysModuleExRepo _sysModuleRepo;
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly Newtouch.Sett.Domain.IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly IModuleApp _moduleApp;
        public ModuleController(ISysModuleExRepo sysModuleRepo, ISysModuleDmnService sysModuleDmnService, Newtouch.Sett.Domain.IUserRoleAuthDmnService userRoleAuthDmnService, IModuleApp moduleApp)
        {
            _sysModuleRepo = sysModuleRepo;
            _sysModuleDmnService = sysModuleDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
            _moduleApp = moduleApp;
        }


        /// <summary>
        /// khi tải,đượclấytráibêncâytiếtđiểm
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetModuleTreeJsonByName(string name)
        {
            var data = _sysModuleRepo.GetModuleTreeJsonByName(name);
            {
                var authedList = _sysModuleDmnService.GetMenuList(this.OrganizeId
                    , this.UserIdentity.RoleIdList, this.UserIdentity.IsRoot, this.UserIdentity.IsAdministrator);
                if ((authedList == null || authedList.Count == 0) && this.UserIdentity != null)
                {
                    // NX10-002 (D-NX10-002, mau BHOSP-001): RoleIdList trong
                    // session duoc xay tu Union DB, con GetMenuList chay tren
                    // Sett DB nen GUID Union khong khop quyen Sett -> cay 0
                    // node. Re-resolve quyen tren chinh Sett DB cua user nay
                    // (chi doc, chi quyen Sett that cua chinh user) roi thu lai.
                    var settRoles = _userRoleAuthDmnService.GetUserRoleList(this.UserIdentity.UserId ?? string.Empty);
                    if (settRoles != null && settRoles.Count > 0)
                    {
                        var settRoleIds = settRoles.Select(p => p.Id).ToList();
                        authedList = _sysModuleDmnService.GetMenuList(this.OrganizeId
                            , settRoleIds, this.UserIdentity.IsRoot, this.UserIdentity.IsAdministrator);
                    }
                }
                data = data.TreeWhere(p => authedList.Any(i => i.Id == p.Id), parentId:"ParentId");
            }
            var treeList = new List<TreeViewModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.UrlAddress;
                tree.parentId = "0";
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            // NX10-002-B: phai truyen "0" tuong minh. TreeViewJson() khong doi
            // so cua Sett (Newtouch.Sett.Domain.TreeViewExtensions) coi root
            // la parentId null/rong, trong khi vong lap tren gan moi node
            // parentId="0" theo mau vendor (Framework TreeViewJson mac dinh
            // "0") — goi khong doi so tra "[]" du data da co 7 node (da
            // chung minh bang endpoint diagnostic tam, da revert).
            return Content(treeList.TreeViewJson("0"));
        }

        #region Quản lý menu (NX14-001)
        // Port 5 action tu PDS ModuleController — truoc day controller Sett
        // chi co GetModuleTreeJsonByName + Organizes, ca 5 duong nay 404
        // (F-NX13-001; vendor Sett goc cung thieu). Theo quy uoc Sett:
        // KHONG [HandlerAjaxOnly]/[ValidateAntiForgeryToken] nhu PDS
        // (NX8-002: view Sett post tran khong gui token); topOrgId lay tu
        // SysConfigReader nhu ModuleButtonController/RoleAuthorizeController.

        /// <summary>
        /// Thả xuống (Chọn thực đơn Chỉnh sửa Cấp trên) — Form.cshtml:97
        /// bind ParentId vao day.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTreeSelectJson()
        {
            var data = _moduleApp.GetValidList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// Grid cây menu — Index.cshtml:64.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysModuleDmnService.GetTreeGridList(SysConfigReader.String("Top_OrganizeId"), keyword);
            var treeList = new List<TreeGridModel>();
            foreach (SysModuleVO item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        [HttpGet]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _moduleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            moduleEntity.zt = moduleEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.SubmitForm(moduleEntity, keyValue, SysConfigReader.String("Top_OrganizeId"));
            return Success("Thao tác thành công。");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _moduleApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        #endregion

        #region Phân quyền Cơ sở
        // NX8-002: port tu PDS ModuleController (itemType = 1, module) + theo
        // mau anh em Sett ModuleButtonController (type 2): KHONG gan
        // [HandlerAjaxOnly]/[ValidateAntiForgeryToken] nhu PDS — view Sett
        // post $.submitForm tran (khong gui token) giong ModuleButton.
        // Dich vu: Sett SysModuleDmnService (Sett DB) — xem D-NX8-002.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Organizes()
        {
            return View();
        }

        /// <summary>
        /// Phân quyền cho tổ chức chỉ định
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgList"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAuthOrganizeList(string keyValue, string orgList)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.UpdateAuthOrganizeList(keyValue, orgList, 1);
            }
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Phân quyền cho tất cả tổ chức
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AuthAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthAllOrganize(keyValue, 1);
            }
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Hủy tất cả phân quyền (Tổ chức)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AuthCancelAllOrganize(string keyValue)
        {
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _sysModuleDmnService.AuthCancelAllOrganize(keyValue, 1);
            }
            return Success("Thao tác thành công。");
        }

        #endregion

    }
}
