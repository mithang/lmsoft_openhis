using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using Newtouch.HIS.Application.Interface;
using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Application;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Web.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly IRoleApp _roleApp;
        private readonly IModuleApp _moduleApp;
        private readonly IModuleButtonApp _moduleButtonApp;
        private readonly IRoleAuthorizeApp _roleAuthorizeApp;
        private readonly ISysUserExDmnService _sysUserDmnService;
        private readonly IUserRoleAuthDmnService _userRoleAuthDmnService;

        // NX-007: port dung — file goc thieu ctor nen 6 field readonly khong bao
        // gio duoc gan (compile 0 loi nhung runtime NRE ngay call dau). Ca 6
        // service da dang ky Singleton trong Program.cs (:68, :69, :94, :95,
        // :158, :165). Luu y: _moduleApp/_moduleButtonApp/_roleAuthorizeApp/
        // _sysUserDmnService hien khong dung trong file (chi _roleApp va
        // _userRoleAuthDmnService duoc goi) — giu nguyen khai bao goc cua vendor,
        // khong cat bot dependency.
        public RoleController(IRoleApp roleApp, IModuleApp moduleApp, IModuleButtonApp moduleButtonApp, IRoleAuthorizeApp roleAuthorizeApp, ISysUserExDmnService sysUserDmnService, IUserRoleAuthDmnService userRoleAuthDmnService)
        {
            _roleApp = roleApp;
            _moduleApp = moduleApp;
            _moduleButtonApp = moduleButtonApp;
            _roleAuthorizeApp = roleAuthorizeApp;
            _sysUserDmnService = sysUserDmnService;
            _userRoleAuthDmnService = userRoleAuthDmnService;
        }

        //Danh sách vai trò gridjson
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _roleApp.GetPagintionList(Constants.TopOrganizeId, pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _roleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysRoleEntity roleEntity, string permissionIds, string keyValue)
        {
            roleEntity.OrganizeId = "*";    //Vai trò phiên bản này theoTopOrganizeId,không tới được bệnh viện cụ thể
            roleEntity.TopOrganizeId = Constants.TopOrganizeId;
            roleEntity.zt = roleEntity.zt == "true" ? "1" : "0";
            // NX30-ROLEDDL (F-NX30-ROLE-001): permissionIds ve null khi form
            // khong check quyen nao (UI gui String([]) = "") -> Split(',') NRE
            // o dong nay (live-probe temp 5301: SubmitForm keyValue=1, perms
            // rong -> 200 {"state":"error","message":"Object reference..."},
            // stack RoleController.cs:75). Guard: danh sach rong = 0 quyen,
            // SubmitForm se xoa het RA cua role + khong insert dong nao.
            var permissionIdArray = string.IsNullOrEmpty(permissionIds)
                ? System.Array.Empty<string>()
                : permissionIds.Split(',');
            _roleApp.SubmitForm(roleEntity, permissionIdArray, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _roleApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Lưu Sys_UserRole
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitRoleUser(string roleId, string userIds)
        {
            var organizeId = base.GetAuthOrganizeId();  //Mặc định Quyền OrganizeId

            _userRoleAuthDmnService.submitRoleUser(roleId, userIds, organizeId);
            return Success("Thao tác thành công");
        }

    }
}
