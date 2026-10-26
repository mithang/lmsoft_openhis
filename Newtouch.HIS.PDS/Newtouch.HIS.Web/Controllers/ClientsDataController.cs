using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using System;
using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Domain.IRepository;

namespace Newtouch.HIS.Web.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : FrameworkBase.MultiOrg.Web.Controllers.ClientsDataController
    {
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly ISysDepartmentRepo _sysDepartRepo;
        private readonly ISysFailedCodeMessageMappRepo _sysFailedCodeMessageMappRepo;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        private readonly IItemDmnService _itemDmnService;
        private readonly ISysConfigRepo _sysConfigRepo;

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            //năm tháng
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
                sysDepartList = _sysDepartmentRepo.GetList(opr.OrganizeId);//khoa
            }
            var data = new
            {
                //từ điển bao gồm không hiệu lực của
                dataItems = GetDataItemList(),
                //lấy tất cả khoa bao gồm không hiệu lực của
                sysDepartList = sysDepartList == null ? null : sysDepartList.ToJson(new[] { "Name", "Code", "yjbz", "py" }),    //khoa
                authorizeMenu = GetMenuList(),
                //
                SysFailedCodeMessageMapList = _sysFailedCodeMessageMappRepo.GetList(opr.OrganizeId),
            };
            return Content(data.ToJson());
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
