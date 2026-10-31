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
    /// Vai tròController
    /// </summary>
    [Area("SystemManage")]

    public class RoleController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IRoleApp _roleApp;
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly ISysUserDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly ISysUserRoleRepo _sysUserRoleRepo;

        /// <summary>
        /// cơ quantạothưsố
        /// </summary>
        /// <param name="roleApp"></param>
        /// <param name="moduleApp"></param>
        /// <param name="moduleButtonApp"></param>
        /// <param name="roleAuthorizeApp"></param>
        /// <param name="sysUserDmnService"></param>
        /// <param name="userRoleAuthDmnService"></param>
        public RoleController(IRoleApp roleApp, IModuleApp moduleApp, IModuleButtonApp moduleButtonApp, IRoleAuthorizeApp roleAuthorizeApp
            , ISysUserDmnService sysUserDmnService, IUserRoleAuthDmnService userRoleAuthDmnService
            , ISysRoleRepo sysRoleRepo, ISysUserRoleRepo sysUserRoleRepo)
        {
            this._roleApp = roleApp;
            this._moduleApp = moduleApp;
            this._moduleButtonApp = moduleButtonApp;
            this._roleAuthorizeApp = roleAuthorizeApp;
            this._sysUserDmnService = sysUserDmnService;
            this._userRoleAuthDmnService = userRoleAuthDmnService;
            this._sysRoleRepo = sysRoleRepo;
            this._sysUserRoleRepo = sysUserRoleRepo;
        }

        //Vai tròDanh sách gridjson
        [HttpGet]

        public IActionResult GetGridJson(Pagination pagination, string keyword, string OrganizeId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                OrganizeId = this.GetAuthOrganizeId();
            }
            var data = new
            {
                rows = _roleApp.GetPagintionList(OrganizeId, pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var data = _roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]

        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.TopOrganizeId = SysConfigReader.String("Top_OrganizeId");
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            
            _roleApp.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _roleApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// LưuVai tròdùnghộliên quanliênliên quanhệ
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitRoleUser(string roleId, List<FirstSecond> userList)
        {
            _userRoleAuthDmnService.submitRoleUser(roleId, userList);
            return Success("Thao tác thành công");
        }

        /// <summary>
        /// Vai trò(cây)   (dùnghộchínhđộngliên quanliênVai trò)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>

        public IActionResult GetRoleList(string userId, string organizeId)
        {
            var roleList = _sysRoleRepo.IQueryable(p => p.zt == "1" && p.OrganizeId == organizeId).ToList();
            var treeList = new List<TreeViewModel>();
            var userCurrentRoleEntityList = new List<string>();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                userCurrentRoleEntityList = _sysUserRoleRepo.GetRoleIdListByUserId(userId).ToList();
            }
            foreach (var role in roleList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = role.Id;
                tree.text = role.Name;
                tree.value = role.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = userCurrentRoleEntityList.Count(t => t == role.Id);
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

    }
}
