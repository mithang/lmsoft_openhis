using Microsoft.AspNetCore.Mvc;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Framework.Filter;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Web.Controllers
{
    /// <summary>
    /// Home/Index加载时 默认加载的 缓存数据.
    /// Redis OperationDic cache dropped (see plan): GetClientsDataJson calls
    /// ICommonDmnService.GetOperations directly every time. The Framework
    /// ISysUserDmnService.GetStaffByDutyCode is replaced by the local
    /// ICommonDmnService.GetStaffByDutyCode (same V_C_Sys_StaffDuty +
    /// V_S_Sys_Staff query the MRQC pilot verified). Enums are reflected
    /// locally from EnumDB because the Core base GetEnumList loads assemblies
    /// by name and filters on EnumExtend/ClientEnum types, which OR's enums
    /// are not.
    /// </summary>
    public class ClientsDataController : ClientsDataBaseController
    {
        private readonly ICommonDmnService _commonDmnService;

        public ClientsDataController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn,
            ICommonDmnService commonDmnService)
            : base(systemApp, sysConfigDmn)
        {
            _commonDmnService = commonDmnService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetModuleDataJson()
        {
            // The menu list comes from the BaseAPI center service; if that
            // host is unreachable, degrade to an empty menu instead of a 500
            // (standalone pages do not depend on authorizeMenu).
            object menu = new System.Collections.Generic.List<object>();
            try
            {
                menu = await this.GetMenuList();
            }
            catch
            {
            }
            var data = new
            {
                authorizeMenu = menu,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取同步的部分
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetClientsDataJson()
        {
            var operationsList = _commonDmnService.GetOperations("", OrganizeId ?? "1");
            var data = new
            {
                OperationsList = operationsList,
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取异步的部分
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public IActionResult GetAsyncClientsDataJson()
        {
            var opr = OperatorProvider.GetCurrent();
            var orgId = opr?.OrganizeId ?? "1";
            object doctorInHosBookkeep = _commonDmnService.GetStaffByDutyCode(orgId, "Doctor");
            var data = new
            {
                itemDetails = this.GetItemDetailsList(),
                doctorInHosBookkeep = doctorInHosBookkeep,
                SysFailedCodeMessageMapList = _commonDmnService.FailMessage(orgId, null),
                enums = GetEnumListLocal(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Local enum reflection over Newtouch.OR.ManageSystem.Infrastructure
        /// (EnumDB), in the shape {Type, Items[{Name, Value, Desc}]}.
        /// </summary>
        private static List<object> GetEnumListLocal()
        {
            var result = new List<object>();
            var asm = typeof(EnumSqzt).Assembly;
            foreach (var type in asm.GetTypes().Where(t => t.IsEnum && t.Namespace == "Newtouch.OR.ManageSystem.Infrastructure"))
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
            return result;
        }
    }
}
