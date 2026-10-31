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
            _roleApp.SubmitForm(roleEntity, permissionIds.Split(','), keyValue);
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
        /// LưuSys_UserRole
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult submitRoleUser(string roleId, string userIds)
        {
            var organizeId = base.GetAuthOrganizeId();  //Mặc định QuyềnOrganizeId

            _userRoleAuthDmnService.submitRoleUser(roleId, userIds, organizeId);
            return Success("Thao tác thành công");
        }

    }
}
