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

    public class SysMSItemComparedController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysMSItemComparedApp _SysMSItemComparedApp;
        private readonly ISysDepartmentRepo _sysDepartmentRepo;
        public SysMSItemComparedController(ISysMSItemComparedApp sysMSItemComparedApp, ISysDepartmentRepo sysDepartmentRepo)
        {
            _SysMSItemComparedApp = sysMSItemComparedApp;
            _sysDepartmentRepo = sysDepartmentRepo;
        }


        public IActionResult SysMSItemCompared()
        {
            return View();
        }

        public IActionResult SubmitForm(SysMedicalTechItemMappEntity entity, string keyValue)
        {
            _SysMSItemComparedApp.SubmitForm(entity, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(int keyValue)
        {

            _SysMSItemComparedApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// 获取grid的值
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdzGridJson(string keyword)
        {
            var data = _SysMSItemComparedApp.GetListBySearch(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 收费项目下拉框
        /// </summary>
        /// <returns></returns>
        [Obsolete("please use SysChargeItemController.GetChargeItemSelectData(string keyword)")]
        public IActionResult GetxmSelect()
        {
            return null;
        }

        /// <summary>
        /// 科室下拉框
        /// </summary>
        /// <returns></returns>
        public IActionResult GetksSelect()
        {
            var data = _sysDepartmentRepo.GetList(this.OrganizeId, "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Code;
                treeModel.text = item.Name;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(Guid keyValue)
        {
            var entity = _SysMSItemComparedApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

    }
}
