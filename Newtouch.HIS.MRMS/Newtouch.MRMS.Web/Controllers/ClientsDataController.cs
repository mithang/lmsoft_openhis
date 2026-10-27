using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;

namespace Newtouch.MRMS.Web.Controllers
{
    /// <summary>
    /// Home/Index加载 giờ 默认加载的 缓存数据 (ported from the binary
    /// FrameworkBase ClientsDataController base; Core equivalent is
    /// ClientsDataBaseController with async menu/enum lists).
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
        /// 获取异步的部分
        /// </summary>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetAsyncClientsDataJson()
        {
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                enums = await this.GetEnumList("Newtouch.MRMS.Infrastructure"),
            };
            return Content(data.ToJson());
        }
    }
}
