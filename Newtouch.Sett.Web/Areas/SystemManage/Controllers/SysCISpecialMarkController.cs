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

    public class SysCISpecialMarkController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly ISysCISpecialMarkApp _SysCISpecialMarkApp;
        private readonly ISysPatiNatureApp _SysPatiNatureApp;
        public SysCISpecialMarkController(ISysCISpecialMarkApp sysCISpecialMarkApp, ISysPatiNatureApp sysPatiNatureApp)
        {
            _SysCISpecialMarkApp = sysCISpecialMarkApp;
            _SysPatiNatureApp = sysPatiNatureApp;
        }


        public IActionResult SysCISpecialMark()
        {
            return View();
        }

        public IActionResult SubmitForm(SysChargeItemSpecialMarkEntity xt_sfxmtsbzEntity, string keyValue)
        {
            _SysCISpecialMarkApp.SubmitForm(xt_sfxmtsbzEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        public IActionResult DeleteForm(int keyValue)
        {

            _SysCISpecialMarkApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Lấygridgiá trị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetbzGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _SysCISpecialMarkApp.GetListBySearch(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Khi sửa thông tin,Mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _SysCISpecialMarkApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

    }
}
