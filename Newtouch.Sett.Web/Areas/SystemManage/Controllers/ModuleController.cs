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
        public ModuleController(ISysModuleExRepo sysModuleRepo, ISysModuleDmnService sysModuleDmnService)
        {
            _sysModuleRepo = sysModuleRepo;
            _sysModuleDmnService = sysModuleDmnService;
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

    }
}
