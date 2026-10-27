using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Newtouch.HIS.Base.Web.Controllers
{
    public class ItemsTypeController : ControllerBase
    {
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;
        private readonly ISysItemsRepository _sysItemsRepository;
        private readonly IItemDmnService _itemDmnService;
        public ItemsTypeController(ISysItemsDetailRepository sysItemsDetailRepository
            , ISysItemsRepository sysItemsRepository
            , IItemDmnService itemDmnService)
        {
            this._sysItemsDetailRepository = sysItemsDetailRepository;
            this._sysItemsRepository = sysItemsRepository;
            this._itemDmnService = itemDmnService;
        }
        
        //下拉选择 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeSelectJson()
        {
            var data = _sysItemsRepository.GetValidList();
            var treeList = new List<TreeSelectModel>();
            foreach (SysItemsEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }

        //左导航
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeJson()
        {
            var data = _sysItemsRepository.GetValidList();
            var treeList = new List<TreeViewModel>();
            foreach (SysItemsEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Any(t => t.ParentId == item.Id);
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        //gridjson
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetTreeGridJson(string keyword)
        {
            var data = _sysItemsRepository.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.ToList().TreeWhere(t => t.Name.Contains(keyword) || t.Code.Contains(keyword), parentId: "ParentId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (SysItemsEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Any(t => t.ParentId == item.Id);
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
            var data = _sysItemsRepository.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysItemsEntity entity, string keyValue)
        {
            entity.ParentId = (string.IsNullOrWhiteSpace(entity.ParentId) || entity.ParentId == "0")
               ? null : entity.ParentId
               ;
            entity.zt = entity.zt == "true" ? "1" : "0";
            _sysItemsRepository.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}