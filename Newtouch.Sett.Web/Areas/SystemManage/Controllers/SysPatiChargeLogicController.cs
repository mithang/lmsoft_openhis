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

    public class SysPatiChargeLogicController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysPatiChargeLogicApp _SysPatiChargeLogicApp;
        private readonly ISysPatiNatureApp _SysPatiNatureApp;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        public SysPatiChargeLogicController(ISysPatiChargeLogicApp sysPatiChargeLogicApp, ISysPatiNatureApp sysPatiNatureApp, ISysChargeCategoryRepo sysChargeCategoryRepo)
        {
            _SysPatiChargeLogicApp = sysPatiChargeLogicApp;
            _SysPatiNatureApp = sysPatiNatureApp;
            _sysChargeCategoryRepo = sysChargeCategoryRepo;
        }


        #region bệnh nhân Thu phíThuật toán

        /// <summary>
        /// 获取病人收费Thuật toángrid值
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="ogrId"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetsfGridJson(Pagination pagination, string keyword, string ogrId)
        {
            var data = new
            {
                rows = _SysPatiChargeLogicApp.GetList(pagination, keyword, ogrId),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        public IActionResult SysPatiChargeLogic()
        {
            return View();
        }


        public IActionResult SubmitForm(PatiChargeLogicVO PatiChargeLogicVO, string keyValue)
        {
            _SysPatiChargeLogicApp.SubmitForm(PatiChargeLogicVO, keyValue,OrganizeId);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(int keyValue)
        {
            _SysPatiChargeLogicApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// 修改信息时，把选中行对象带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = _SysPatiChargeLogicApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }


        /// <summary>
        /// 病人性质下拉框 
        /// 病人收费Thuật toán病人性质下拉框已不再使用，
        /// 该方法在收费减免处也使用到，如果减免修改了病人性质展现方式，不再使用该方法，请记得清除
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetbrxzSelect()
        {
            var treeList = _SysPatiNatureApp.GetbrxzSelect("");
            return Content(treeList);
        }

        /// <summary>
        /// 大类下拉框
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdlSelect()
        {
            var data = _sysChargeCategoryRepo.GetList(this.OrganizeId, zt: "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// 根据大类获取项目code
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="dlCode"></param>
        /// <returns></returns>
        public IActionResult GetSFXMItemInfoByDlCode(string keyword, string dlCode)
        {
            var data = _SysPatiChargeLogicApp.GetSFXMItemInfoByDlCode(keyword, dlCode, this.OrganizeId);
            return Content(data.ToJson());
        }

        #endregion
    }
}
