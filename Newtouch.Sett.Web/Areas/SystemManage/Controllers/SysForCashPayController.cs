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

    public class SysForCashPayController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly ISysForCashPayApp _SysForCashPayApp;
        public SysForCashPayController(ISysForCashPayApp sysForCashPayApp)
        {
            _SysForCashPayApp = sysForCashPayApp;
        }


        public IActionResult SysForCashPay()
        {
            return View();
        }

        public IActionResult SubmitForm(SysCashPaymentModelEntity SysForCashPayEntity, int? keyValue)
        {
            _SysForCashPayApp.SubmitForm(SysForCashPayEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        //[HandlerAuthorize]
        //[ValidateAntiForgeryToken]
        public IActionResult DeleteForm(int keyValue)
        {

            _SysForCashPayApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Lấygridgiá trị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdlGridJson(string keyword)
        {
            var data = _SysForCashPayApp.GetListBySearch(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Hộp chọn loại lớn
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdlSelect()
        {
            var data = _SysForCashPayApp.GetList();
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                //TreeSelectModel treeModel = new TreeSelectModel();
                //treeModel.id = item.dl.ToString();
                //treeModel.text = item.dlmc;
                //treeModel.parentId = "0";
                //treeModel.data = item;
                //treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        /// <summary>
        /// Khi sửa thông tin，Mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _SysForCashPayApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

        /// <summary>
        /// Lấygridgiá trị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetfsGridJson(string keyword)
        {
            var data = _SysForCashPayApp.GetList();
            return Content(data.ToJson());
        }

    }
}
