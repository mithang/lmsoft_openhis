using System.Collections.Generic;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Microsoft.AspNetCore.Mvc;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Linq;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class ModuleController : ControllerBase
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly IModuleApp _moduleApp;

        public ModuleController(IModuleApp moduleApp, ISysModuleDmnService sysModuleDmnService)
        {
            this._moduleApp = moduleApp;
            this._sysModuleDmnService = sysModuleDmnService;
        }

        /// <summary>
        /// 下拉（选择菜单 编辑 上级）
        /// </summary>
        /// <param name="justTop">仅顶级</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson(bool justTop = true)
        {
            var data = _sysModuleDmnService.GetMenuListByOrg(this.OrganizeId)
                .Where(p => p.zt == "1").ToList();
            if (justTop)
            {
                data = data.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).ToList();
            }
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

            var data =_sysModuleDmnService.GetMenuListByOrg(this.OrganizeId);
            List<SysModuleEntity> newList = data.ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                newList = data.ToList().TreeWhere(t => t.Name.Contains(keyword), parentId: "ParentId");
                if (newList.Count == 1)
                {
                    //如果有子节点，包含之
                    newList.AddRange(data.Where(p => p.ParentId == newList[0].Id));
                }
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysModuleEntity item in newList)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = newList.Count(t => t.ParentId == item.Id) == 0 ? false : true;
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
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysModuleEntity moduleEntity, string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                if (!this.UserIdentity.IsRoot && !this.UserIdentity.IsAdministrator)
                {
                    if (string.IsNullOrEmpty(this.OrganizeId))
                    {
                        return Error("Thao tác thất bại.");
                    }
                    moduleEntity.OrganizeId = this.OrganizeId;
                }
            }
            else
            {
                //是修改行为
                if (this.OrganizeId != Constants.TopOrganizeId
                    && this.OrganizeId != moduleEntity.OrganizeId)
                {
                    {
                        return Error("Thao tác thất bại.Lỗi dữ liệu");
                    }
                }
            }

            moduleEntity.zt = moduleEntity.zt == "true" ? "1" : "0";
            _sysModuleDmnService.SubmitForm(moduleEntity, keyValue);
            return Success("Thao tác thành công.");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _moduleApp.DeleteForm(keyValue);
            return Success("Xóa thành công.");
        }
    }
}