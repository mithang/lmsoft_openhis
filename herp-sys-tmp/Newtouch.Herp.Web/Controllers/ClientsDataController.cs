using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.Herp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Newtouch.Herp.Web.Controllers
{
    /// <summary>
    /// Home/Index加载时 默认加载的 缓存数据
    /// </summary>
    public class ClientsDataController : ClientsDataBaseController
    {
        public ClientsDataController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn)
            : base(systemApp, sysConfigDmn)
        {
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetModuleDataJson()
        {
            var data = new
            {
                authorizeMenu = await this.GetMenuList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取同步的部分
        /// </summary>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetClientsDataJson()
        {
            var data = new
            {
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取异步的部分 (enums served by Herp area pages (added with their areas))
        /// </summary>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetAsyncClientsDataJson()
        {
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
            };
            return Content(data.ToJson());
        }

        private static List<object> EnumItems<T>() where T : Enum
        {
            var list = new List<object>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var field = typeof(T).GetField(value.ToString());
                var desc = field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
                list.Add(new { Name = value.ToString(), Value = Convert.ToInt32(value), Desc = desc });
            }
            return list;
        }
    }
}
