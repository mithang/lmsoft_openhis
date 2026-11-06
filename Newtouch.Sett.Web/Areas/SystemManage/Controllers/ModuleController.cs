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
        public ModuleController(ISysModuleExRepo sysModuleRepo, ISysModuleDmnService sysModuleDmnService, Newtouch.Sett.Domain.IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            _sysModuleRepo = sysModuleRepo;
            _sysModuleDmnService = sysModuleDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
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
            return Content(treeList.TreeViewJson());
        }

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
