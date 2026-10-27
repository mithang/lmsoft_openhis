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

    public class ItemsTypeController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IItemsApp _itemsApp;

        public ItemsTypeController(IItemsApp itemsApp)
        {
            this._itemsApp = itemsApp;
        }

        [HttpGet]

        public IActionResult GetTreeSelectJson()
        {
            var data = _itemsApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (ItemsEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.F_FullName;
                treeModel.parentId = item.F_ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }
        [HttpGet]

        public IActionResult GetTreeJson()
        {
            var data = _itemsApp.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (ItemsEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.F_FullName;
                tree.value = item.F_EnCode;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }
        [HttpGet]

        public IActionResult GetTreeGridJson()
        {
            var data = _itemsApp.GetList();
            var treeList = new List<TreeGridModel>();
            foreach (ItemsEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.F_ParentId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var data = _itemsApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(ItemsEntity itemsEntity, string keyValue)
        {
            itemsEntity.zt = itemsEntity.zt == "true" ? ((int)EnumZT.Valid).ToString() : ((int)EnumZT.Invalid).ToString();
            _itemsApp.SubmitForm(itemsEntity, keyValue);
            return Success("Thao tác thành công。");
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _itemsApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }
    }
}
