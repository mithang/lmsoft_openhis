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

namespace Newtouch.HIS.Base.Web.Controllers
{
    public class ItemsDataController : ControllerBase
    {
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;
        private readonly IItemDmnService _itemDmnService;
        public ItemsDataController(ISysItemsDetailRepository sysItemsDetailRepository,
            IItemDmnService itemDmnService)
        {
            this._sysItemsDetailRepository = sysItemsDetailRepository;
            this._itemDmnService = itemDmnService;
        }

        //Thả xuống by Code
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetSelectJson(string code)
        {
            var data = _itemDmnService.GetValidListByItemCode(code,"");
            var list = new List<object>();
            foreach (SysItemsDetailEntity item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(string itemId, string keyword)
        {
            var data = _sysItemsDetailRepository.GetCommonList(itemId, keyword);
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _sysItemsDetailRepository.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailEntity.TopOrganizeId = "*";
            itemsDetailEntity.OrganizeId = "*";

            itemsDetailEntity.zt = itemsDetailEntity.zt == "true" ? "1" : "0";
            _sysItemsDetailRepository.SubmitForm(itemsDetailEntity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}