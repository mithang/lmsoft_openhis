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
    /// tạo lập:Trương Đông Lâm
    /// ngày kỳ:2017-10-16 11:07
    /// mô tả kể:Menu nhanh
    /// </summary>
    [Area("SystemManage")]

    public class SysShortcutMenuController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysShortcutMenuRepo _sysShortcutMenuRepo;
        private readonly ISysRoleRepo _sysRoleRepo;
        private readonly ISysRoleShortcutMenuDmnService _sysRoleShortcutMenuDmnService;
        public SysShortcutMenuController(ISysShortcutMenuRepo sysShortcutMenuRepo, ISysRoleRepo sysRoleRepo, ISysRoleShortcutMenuDmnService sysRoleShortcutMenuDmnService)
        {
            _sysShortcutMenuRepo = sysShortcutMenuRepo;
            _sysRoleRepo = sysRoleRepo;
            _sysRoleShortcutMenuDmnService = sysRoleShortcutMenuDmnService;
        }


        /// <summary>
        /// đượclấythựcthểcộtbảng
        /// </summary>
        /// <param name="keyword">Từ khóa lọc</param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var list = _sysShortcutMenuRepo.GetList(keyword);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Lấy thực thể
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _sysShortcutMenuRepo.FindEntity(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lưu biểu mẫu(Thêm mới,Sửa)
        /// </summary>
        /// <param name="entity">Đối tượng thực thể</param>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysShortcutMenuEntity entity, string keyValue)
        {
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysShortcutMenuRepo.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="keyValue">Giá trị khóa chính</param>
        /// <returns></returns>
        public IActionResult DeleteForm(string keyValue)
        {
            _sysShortcutMenuRepo.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// Phân quyền vai trò
        /// </summary>
        /// <returns></returns>
        public IActionResult Roles()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetAuthRoleList(string keyValue)
        {
            var roleList = _sysRoleRepo.IQueryable(p => p.zt == "1" && p.OrganizeId == this.OrganizeId).ToList();

            var treeList = new List<TreeViewModel>();

            var authedRoleIdList = _sysRoleShortcutMenuDmnService.GetAuthedRoleIdList(keyValue, this.OrganizeId);

            foreach (var item in roleList)
            {
                TreeViewModel tree = new TreeViewModel();
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = authedRoleIdList.Any(t => t == item.Id) ? 1 : 0;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]

        public IActionResult UpdateAuthRoleList(string keyValue, string roleList)
        {
            _sysRoleShortcutMenuDmnService.UpdateAuthRoleList(keyValue, roleList, this.OrganizeId);

            return Success("Thao tác thành công。");
        }


    }
}
