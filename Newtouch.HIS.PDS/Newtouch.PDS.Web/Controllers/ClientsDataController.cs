using NewtouchHIS.Lib.Framework.Attributes;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Lib.Framework.Filter;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.Entity;
using NewtouchHIS.Framework.Web.Controllers;
using NewtouchHIS.Framework.Web.Implementation;
using NewtouchHIS.Lib.Framework.Operator;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Threading.Tasks;


namespace Newtouch.HIS.Web.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : ClientsDataBaseController
    {
        private readonly Newtouch.HIS.Domain.IDomainServices.ISysModuleDmnService _sysModuleDmnService;
        private readonly ISysDepartmentRepo _sysDepartRepo;
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IItemDmnService _itemDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        public ClientsDataController(ISystemAppService systemApp, ISysConfigDmnService sysConfigDmn,
            Newtouch.HIS.Domain.IDomainServices.ISysModuleDmnService sysModuleDmnService,
            ISysDepartmentRepo sysDepartRepo,
            ISysFailedCodeMessageMappRepo sysFailedCodeMessageMappRepo,
            ISysDepartmentRepo sysDepartmentRepo,
            IItemDmnService itemDmnService,
            ISysConfigRepo sysConfigRepo) : base(systemApp, sysConfigDmn)
        {
            _sysModuleDmnService = sysModuleDmnService;
            _sysDepartRepo = sysDepartRepo;
            _sysFailedCodeMessageMappRepo = sysFailedCodeMessageMappRepo;
            _sysDepartmentRepo = sysDepartmentRepo;
            _itemDmnService = itemDmnService;
            _sysConfigRepo = sysConfigRepo;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetClientsDataJson()
        {
            //年月
            var yearArr = new List<int>();
            var yearMin = DateTime.Now.Year - 5;
            var yearMax = DateTime.Now.Year + 1;
            var opr = OperatorProvider.GetCurrent();
            if (opr != null)
            {
                var configVal = _sysConfigRepo.GetValueByCode("Hosp_Year_Start", opr.OrganizeId);
                if (!int.TryParse(configVal, out yearMin))
                {
                    yearMin = DateTime.Now.Year - 5;
                }
            }
            for (var i = yearMin; i <= yearMax; i++)
            {
                yearArr.Add(i);
            }

            //var opr = OperatorProvider.GetCurrent();
            object sysDepartList = null;
            if (!string.IsNullOrWhiteSpace(opr.OrganizeId))
            {
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId);//Khoa
            }
            var data = new
            {
                //字典 包括无效的
                dataItems = GetDataItemList(),
                //获取所有科室 包括无效的
                sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py" }),    //Khoa
                authorizeMenu = await GetMenuList(),
                //
                SysFailedCodeMessageMapList = _sysFailedCodeMessageMappRepo.GetList(opr.OrganizeId),
            };
            return Content(data.ToJson());
        }

        private object GetDataItemList()
        {
            // Core port of the FrameworkBase helper: dictionary types + their items.
            try
            {
                var types = _itemDmnService.GetValidItemsList();
                var topOrgId = Constants.TopOrganizeId;
                var details = string.IsNullOrWhiteSpace(topOrgId) ? null : _itemDmnService.GetValidItemsDetailListByTopOrg(topOrgId);
                return types.Select(k => new
                {
                    Type = k.Code,
                    Items = details == null ? new List<object>() : details.Where(d => d.ItemId == k.Id).Select(d => (object)new { Name = d.Name, Code = d.Code, zt = d.zt }).ToList(),
                }).ToList();
            }
            catch
            {
                return null;
            }
        }

        private string ToMenuJson(IList<SysModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<SysModuleEntity> entitys = data.Where(t => t.ParentId == parentId).ToList();
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
    }
}
