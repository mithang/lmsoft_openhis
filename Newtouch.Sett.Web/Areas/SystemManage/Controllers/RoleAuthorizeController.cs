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

    [Area("SystemManage")]

    public class RoleAuthorizeController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public RoleAuthorizeController(IRoleAuthorizeApp roleAuthorizeApp
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._roleAuthorizeApp = roleAuthorizeApp;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        public IActionResult GetPermissionTree(string roleId)
        {
            var moduledata = _sysModuleDmnService.GetOpenMenuListByTopOrg(SysConfigReader.String("Top_OrganizeId"));
            var buttondata = _sysModuleDmnService.GetOpenMenuButtonListByTopOrg(SysConfigReader.String("Top_OrganizeId"));
            var authorizedata = new List<SysRoleAuthorizeEntity>();
            if (!string.IsNullOrEmpty(roleId))
            {
                authorizedata = _roleAuthorizeApp.GetValidList(roleId);
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
            foreach (SysModuleButtonEntity item in buttondata)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ModuleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authorizedata.Count(t => t.ItemId == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }
    }
}
