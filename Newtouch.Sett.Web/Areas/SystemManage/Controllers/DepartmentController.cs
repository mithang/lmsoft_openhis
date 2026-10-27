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

    /// <summary>
    /// 
    /// </summary>
    [Area("SystemManage")]

    public class DepartmentController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        public DepartmentController(ISysDepartmentRepo sysDepartmentRepo)
        {
            _sysDepartmentRepo = sysDepartmentRepo;
        }


        //组织机构（医院） 科室 下拉 数据源
        [HttpGet]

        public IActionResult GetTreeSelectJson(string organizeId)
        {
            if (string.IsNullOrEmpty(organizeId))
            {
                organizeId = this.OrganizeId;
                if (string.IsNullOrEmpty(organizeId))
                {
                    return null;
                }
            }
            var data = _sysDepartmentRepo.GetList(organizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = item.ParentId == null ? null :
                    data.Where(p => p.Id == item.ParentId).Select(p => p.Code).FirstOrDefault();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
    }
}
