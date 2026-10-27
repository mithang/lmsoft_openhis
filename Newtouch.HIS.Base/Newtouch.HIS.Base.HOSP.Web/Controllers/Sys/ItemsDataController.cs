using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Extension;
using Newtouch.Tools;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
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

        //下拉 by Code
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetSelectJson(string code,string keyword, string OrganizeId)
        {
			List<object> list = new List<object>();
			if (code == "tsypbz")
			{
				foreach (var item in System.Enum.GetValues(typeof(EnumYpsx)))
				{
					object name = item.GetType().GetField(item.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true)[0];
					list.Add(new { id = (int)item, text = (name as DescriptionAttribute).Description });
				}
				return Content(list.ToJson());
			}
			var data = _itemDmnService.GetValidListByItemCode(code,keyword, OrganizeId);
            foreach (SysItemsDetailEntity item in data)
            {
                list.Add(new { id = item.Code, text = item.Name });
            }
            return Content(list.ToJson());
        }

        //grid json
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetGridJson(string itemId, string keyword, string OrganizeId)
        {
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                OrganizeId = this.OrganizeId;
            }
            if (string.IsNullOrWhiteSpace(OrganizeId))
            {
                return null;    //不返回
            }
            var data = _sysItemsDetailRepository.GetListByOrgId(OrganizeId, itemId, keyword);
            foreach (var item in data)
            {
                item.AllowEdit = item.OrganizeId != "*";
            }
            return Content(data.ToJson());
        }

        //get
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetFormJson(string keyValue)
        {
            var data = _sysItemsDetailRepository.FindEntity(keyValue);
            return Content(data.ToJson());
        }

        //保存、修改
        [HttpPost]
        [HandlerAjaxOnly]
        //[ValidateAntiForgeryToken]
        public IActionResult SubmitForm(SysItemsDetailEntity itemsDetailEntity, string keyValue)
        {
            itemsDetailEntity.TopOrganizeId = "*";

            itemsDetailEntity.zt = itemsDetailEntity.zt == "true" ? "1" : "0";
            _sysItemsDetailRepository.SubmitForm(itemsDetailEntity, keyValue);
            return Success("Thao tác thành công.");
        }

    }
}