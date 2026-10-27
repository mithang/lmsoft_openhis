using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Common;
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
    public class ModuleController : ControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;


        //Thả xuống（Chọn thực đơn Chỉnh sửa Cấp trên）
        [HttpGet]
        [HandlerAjaxOnly]
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
            return Content(treeList.TreeSelectJson(null));
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysModuleDmnService.GetMenuListByTopOrg(Constants.TopOrganizeId);
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.ToList().TreeWhere(t => t.Name.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysModuleEntity item in data)
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
            return Content(treeList.TreeGridJson(null));
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _moduleApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            moduleEntity.zt = moduleEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.SubmitForm(moduleEntity, keyValue, Constants.TopOrganizeId);
            return Success("Thao tác thành công。");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _moduleApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        #region Cơ sở ủy quyền

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Organizes()
        {
            return View();
        }

        /// <summary>
        /// Ủy quyền cho tổ chức chỉ định
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="orgList"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
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
        /// Ủy quyền cho tất cả tổ chức
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
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
        /// Thu hồi tất cả ủy quyền（Tổ chức）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
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
