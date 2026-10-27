using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Newtouch.EMR.Web.Controllers
{
    /// <summary>
    /// Home/IndexDữ liệu đệm tải mặc định khi tải.
    /// GetAsyncClientsDataJson mirrors the Framework version: record-element trees
    /// from IBlysRepo, item details from the base controller, and enums scanned from
    /// the EMR Infrastructure assembly (the old GetEnumList("Newtouch.EMR.Infrastructure")).
    /// </summary>
    public class ClientsDataController : ClientsDataBaseController
    {
        private readonly IBlysRepo _blysRepo;

        public ClientsDataController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn, IBlysRepo blysRepo)
            : base(systemApp, sysConfigDmn)
        {
            _blysRepo = blysRepo;
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
        /// Lấy phần đồng bộ
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
        /// Lấy phần không đồng bộ
        /// </summary>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetAsyncClientsDataJson()
        {
            var orgId = OperatorProvider.GetCurrent()?.OrganizeId;
            var data = new
            {
                ysdataList = _blysRepo.GetYsTreeV2(orgId, ""),
                ysmxdataList = _blysRepo.GetYsMX(orgId, ""),
                itemDetails = this.GetItemDetailsList(),
                enums = EnumListFromAssembly("Newtouch.EMR.Infrastructure"),
            };
            return Content(data.ToJson());
        }

        private static List<object> EnumListFromAssembly(string assemblyName)
        {
            var result = new List<object>();
            try
            {
                var asm = Assembly.Load(assemblyName);
                foreach (var type in asm.GetTypes().Where(t => t.IsEnum && t.IsPublic))
                {
                    var items = new List<object>();
                    foreach (var value in Enum.GetValues(type))
                    {
                        var field = type.GetField(value.ToString());
                        var desc = field?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
                        items.Add(new { Name = value.ToString(), Value = Convert.ToInt32(value), Desc = desc });
                    }
                    result.Add(new { Type = type.Name, Items = items });
                }
            }
            catch
            {
                // Enum data is best-effort; pages degrade to empty enum lists.
            }
            return result;
        }
    }
}
