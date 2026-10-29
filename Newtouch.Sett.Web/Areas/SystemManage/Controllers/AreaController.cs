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

    public class AreaController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IAreaApp _areaApp;

        public AreaController(IAreaApp areaApp)
        {
            this._areaApp = areaApp;
        }

        [HttpGet]

        public IActionResult GetTreeSelectJson()
        {
            var data = _areaApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (AreaEntity item in data)
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

        public IActionResult GetTreeGridJson(string keyword)
        {
            // T-11: the original (and this port) returned null here, which
            // ASP.NET Core rejects ("Cannot return null from an action
            // method"). Build the tree grid from the area list, following
            // the ItemsTypeController pattern in this same area; keyword
            // filters on the area name. The list service itself is still
            // a stub returning [] (see T-20), so an empty grid is the
            // correct shape until that service is ported.
            var data = _areaApp.GetList() ?? new List<AreaEntity>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                data = data.Where(t => (t.F_FullName ?? "").Contains(keyword)).ToList();
            }
            var treeList = new List<TreeGridModel>();
            foreach (AreaEntity item in data)
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
            var data = _areaApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(AreaEntity areaEntity, string keyValue)
        {
            _areaApp.SubmitForm(areaEntity, keyValue);
            return Success("Thao tác thành công。");
        }
        [HttpPost]

        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _areaApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }
    }
}
