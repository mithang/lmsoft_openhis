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

    public class SysPatiChargeWaiverController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysPatiChargeWaiverApp _sysPatiChargeWaiverApp;
        public SysPatiChargeWaiverController(ISysPatiChargeWaiverApp sysPatiChargeWaiverApp)
        {
            _sysPatiChargeWaiverApp = sysPatiChargeWaiverApp;
        }


        /// <summary>
        /// khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult SysPatiChargeWaiver()
        {
            return View();
        }
        /// <summary>
        /// Lấy danh sách hiệu lực
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysPatiChargeWaiverApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeWaiverApp.GetEffectiveList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(int keyValue)
        {
            _sysPatiChargeWaiverApp.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysPatientChargeWaiverEntity sysPatiChargeWaiverEntity, int? keyValue)
        {
            sysPatiChargeWaiverEntity.zt = sysPatiChargeWaiverEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeWaiverApp.SubmitForm(sysPatiChargeWaiverEntity, keyValue);
            return Success("Thao tác thành công。");
        }

    }
}
