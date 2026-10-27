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

    public class SysCIRemindContentController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private ISysCIRemindContentApp _SysCIRemindContentApp;
        private ISysChargeItemApp _SysChargeItemApp;

        public SysCIRemindContentController(ISysCIRemindContentApp SysCIRemindContentApp, ISysChargeItemApp SysChargeItemApp)
        {
            this._SysCIRemindContentApp = SysCIRemindContentApp;
            this._SysChargeItemApp = SysChargeItemApp;
        }

        public IActionResult SysCIRemindContent()
        {
            return View();
        }

        public IActionResult SubmitForm(SysChargeItemWarningContentEntity xt_sfxmjsnrEntity, string keyValue)
        {
            _SysCIRemindContentApp.SubmitForm(xt_sfxmjsnrEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        [HttpPost]

        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteForm(int keyValue)
        {

            _SysCIRemindContentApp.DeleteForm(keyValue);
            return Success("Xóa thành công。");
        }

        /// <summary>
        /// Lấygridgiá trị
        /// </summary>
        /// <returns></returns>
        public IActionResult GetnrGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = _SysCIRemindContentApp.GetListBySearch(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// Hộp chọn hạng mục thu phí
        /// </summary>
        /// <returns></returns>
        [Obsolete("please use SysChargeItemController.GetChargeItemSelectData(string keyword)")]
        public IActionResult GetxmSelect()
        {
            return null;
        }

        /// <summary>
        /// Khi sửa thông tin，Mang thông tin sang trang mới
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>

        [HttpGet]

        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _SysCIRemindContentApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }

    }
}
