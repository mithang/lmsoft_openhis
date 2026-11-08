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
    // NX31-A5 (4): gỡ [HandlerLogin] (skip-auth) — khớp 7/10 ClientsDataController
    // khác; filter toàn cục giờ chặn anon (non-AJAX → 302 /Login/Index, AJAX →
    // SESSION_TIMEOUT JSON) đúng D-SHELL-008. Guard opr==null phía dưới giữ lại
    // làm defense-in-depth (không còn tới được qua anon).
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
            //Năm tháng
            var yearArr = new List<int>();
            var yearMin = DateTime.Now.Year - 5;
            var yearMax = DateTime.Now.Year + 1;
            var opr = OperatorProvider.GetCurrent();
            // NX31-A5 (4): guard defense-in-depth — từ khi gỡ [HandlerLogin] thì
            // filter toàn cục đã chặn anon trước khi vào đây; guard chỉ còn phòng
            // trường hợp filter bị bypass trong tương lai.
            if (opr == null)
            {
                return Content(new
                {
                    dataItems = new Dictionary<string, object>(),
                    authorizeMenu = (object)null,
                }.ToJson());
            }
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
                //Từ điển Gồm cả không hiệu lực
                dataItems = GetDataItemList(),
                //Lấy tất cả khoa Gồm cả không hiệu lực
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
