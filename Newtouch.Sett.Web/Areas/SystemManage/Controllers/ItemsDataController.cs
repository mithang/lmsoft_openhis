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

    public class ItemsDataController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly IItemsDetailApp _itemsDetailApp;

        public ItemsDataController(IItemsDetailApp itemsDetailApp)
        {
            this._itemsDetailApp = itemsDetailApp;
        }

        [HttpGet]

        public IActionResult GetGridJson(string itemId, string keyword)
        {
            var data = _itemsDetailApp.GetList(itemId, keyword);
            return Content(data.ToJson());
        }
        [HttpGet]

        public IActionResult GetSelectJson(string enCode)
        {
            var data = _itemsDetailApp.GetItemList(enCode);
            List<object> list = new List<object>();
            foreach (ItemsDetailEntity item in data)
            {
                list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
            }
            return Content(list.ToJson());
        }
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var data = _itemsDetailApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(ItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailEntity.zt = itemsDetailEntity.zt == "true" ? ((int)EnumZT.Valid).ToString() : ((int)EnumZT.Invalid).ToString();
            _itemsDetailApp.SubmitForm(itemsDetailEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(string keyValue)
        {
            _itemsDetailApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }
    }
}
