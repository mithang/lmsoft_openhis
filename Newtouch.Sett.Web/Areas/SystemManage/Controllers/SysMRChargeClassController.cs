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

    public class SysMRChargeClassController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: SystemManage/SysMediRecChargeCateg
        private ISysMRChargeClassApp _sysMRChargeClassApp;

        public SysMRChargeClassController(ISysMRChargeClassApp sysMRChargeClassApp)
        {
            this._sysMRChargeClassApp = sysMRChargeClassApp;
        }
        /// <summary>
        /// khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult SysMRChargeClass()
        {
            return View();
        }
        /// <summary>
        /// đượcđượccóhiệu quảcộtbảng
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysMRChargeClassApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {

            var entity = _sysMRChargeClassApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity)
        {
            _sysMRChargeClassApp.DeleteForm(sysMRChargeClassEntity);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="sysMRChargeClassEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysMedicalRecordChargeCategoryEntity sysMRChargeClassEntity, int? keyValue)
        {
            sysMRChargeClassEntity.zt = sysMRChargeClassEntity.zt == "true" ? "1" : "0";
            _sysMRChargeClassApp.SubmitForm(sysMRChargeClassEntity, keyValue);
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// đượclấytrongvănghépâm
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public IActionResult GetSpell(string strText)
        {
            var py = SettStringUtil.GetChineseSpell(strText);
            return Content(py);
        }
    }
}
