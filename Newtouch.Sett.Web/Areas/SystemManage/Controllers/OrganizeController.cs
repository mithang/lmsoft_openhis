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

    public class OrganizeController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysModuleDmnService _sysModuleDmnService;

        public OrganizeController(ISysOrganizeDmnService sysOrganizeDmnService
            , ISysModuleDmnService sysModuleDmnService)
        {
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        //Hộp chọn Nguồn dữ liệu
        [HttpGet]

        public IActionResult GetChildTreeSelectJson(string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                orgId = base.GetAuthOrganizeId();
            }
            var data = _sysOrganizeDmnService.GetValidListByParentOrg(orgId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.Id == orgId ? null : item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetAuthOrganizeList(string keyValue, string authType = "module")
        {
            var orgList = _sysOrganizeDmnService.GetValidTopOrgList();
            var treeList = new List<TreeViewModel>();
            IList<SysOrganizeVEntity> currentOrgEntityList = new List<SysOrganizeVEntity>();
            if (authType == "module")
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    currentOrgEntityList = _sysModuleDmnService.GetAuthedOrgListByModuleId(keyValue, 1);
                }
            }
            else if (authType == "modulebutton")
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    currentOrgEntityList = _sysModuleDmnService.GetAuthedOrgListByModuleId(keyValue, 2);
                }
            }
            foreach (var item in orgList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = currentOrgEntityList.Count(t => t.Id == item.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}
