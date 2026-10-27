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

    [Area("SystemManage")]

    public class SysChargeMajorClassController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private ISysChargeMajorClassApp _SysChargeMajorClassApp;
        private ISysChargeCategoryRepo _sysChargeCategoryRepo;


        public SysChargeMajorClassController(ISysChargeMajorClassApp SysChargeMajorClassApp
            , ISysChargeCategoryRepo sysChargeCategoryRepo)
        {
            this._SysChargeMajorClassApp = SysChargeMajorClassApp;
            this._sysChargeCategoryRepo = sysChargeCategoryRepo;
        }
        public IActionResult SysChargeMajorClass()
        {
            return View();
        }

        public IActionResult SubmitForm(SysChargeCategoryEntity xt_sfdlEntity, string keyValue)
        {
            _SysChargeMajorClassApp.SubmitForm(xt_sfdlEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(int keyValue)
        {

            _SysChargeMajorClassApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Lấygridgiá trị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdlGridJson(Pagination pagination, string keyword)
        {
            return null;
            //var data = new
            //{
            //    rows = _sysChargeCategoryRepo.GetListBySearch(pagination, keyword),
            //    total = pagination.total,
            //    page = pagination.page,
            //    records = pagination.records
            //};
            //return Content(data.ToJson());
        }

        /// <summary>
        /// thuphílớnloạidướikéokhung
        /// </summary>
        /// <returns></returns>
        public IActionResult GetsfdlSelect()
        {
            var data = _SysChargeMajorClassApp.GetsfdlTreeSelectJson();
            return Content(data);
        }

        /// <summary>
        /// Khi sửa thông tin，Mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = new SysChargeCategoryEntity();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                
                entity = _SysChargeMajorClassApp.GetForm(int.Parse(keyValue));
            }
            else
            {
                entity.dlCode = SettKeyHelper.GetNewFieldUniqueValue("xt_sfdl.dl", this.OrganizeId);
                entity.CreateTime = DateTime.Now;
                entity.CreatorCode = this.UserIdentity.UserCode;
            }
            return Content(entity.ToJson());
        }

    }
}
