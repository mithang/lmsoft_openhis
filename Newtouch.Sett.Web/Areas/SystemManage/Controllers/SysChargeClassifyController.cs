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

    public class SysChargeClassifyController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private ISysChargeClassifyApp _SysChargeClassifyApp;

        public SysChargeClassifyController(ISysChargeClassifyApp SysChargeClassifyApp)
        {
            this._SysChargeClassifyApp = SysChargeClassifyApp;
        }
        public IActionResult SysChargeClassify()
        {
            return View();
        }

        public IActionResult SubmitForm(SysChargeClassificationEntity xt_sfflEntity, string keyValue)
        {
            _SysChargeClassifyApp.SubmitForm(xt_sfflEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        public IActionResult DeleteForm(int keyValue)
        {

            _SysChargeClassifyApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Lấygridgiá trị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetdlGridJson(Pagination Pagination, string keyword)
        {
            var data = new
            {
                rows = _SysChargeClassifyApp.GetListBySearch(Pagination, keyword),
                total = Pagination.total,
                page = Pagination.page,
                records = Pagination.records
            };
            return Content(data.ToJson());
        }


        /// <summary>
        /// Khi sửa thông tin,Mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(string keyValue)
        {
            var entity = new SysChargeClassificationEntity();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity = _SysChargeClassifyApp.GetForm(int.Parse(keyValue));
            }
            else
            {
                entity.fl = SettKeyHelper.GetNewFieldUniqueValue("xt_sffl.fl", this.OrganizeId);
                entity.CreatorCode = this.UserIdentity.UserCode;
                entity.CreateTime = DateTime.Now;
            }
            return Content(entity.ToJson());
        }
    }
}
