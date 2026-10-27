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

    public class SysPatiChargeAddController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysPatiChargeAddApp _sysPatiChargeAddApp;
        private readonly ISysChargeAddCategApp _xt_SysChargeAddCategApp;
        private readonly ISysPatiChargeAddDmnService _sysPatiChargeAddDmnService;
        public SysPatiChargeAddController(ISysPatiChargeAddApp sysPatiChargeAddApp, ISysChargeAddCategApp xt_SysChargeAddCategApp, ISysPatiChargeAddDmnService sysPatiChargeAddDmnService)
        {
            _sysPatiChargeAddApp = sysPatiChargeAddApp;
            _xt_SysChargeAddCategApp = xt_SysChargeAddCategApp;
            _sysPatiChargeAddDmnService = sysPatiChargeAddDmnService;
        }


        /// <summary>
        /// khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult SysPatiChargeAdd()
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
            var data = _sysPatiChargeAddApp.GetSysPatiChargeAddVoList(keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// Hiển thị thêm loại lớn
        /// </summary>
        /// <returns></returns>
        public IActionResult GetfjsfdlList(string keyValue)
        {
            var data = _sysPatiChargeAddApp.GetfjsfdlList(keyValue);
            return Json(data);
        }
        /// <summary>
        /// Thêm mới hoặc sửaForm
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeAddApp.GetSysPatiChargeAddVoList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(SysPatientChargeAdditionalEntity SysPatiChargeAddEntity)
        {
            _sysPatiChargeAddApp.DeleteForm(SysPatiChargeAddEntity);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Lưu
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysPatientChargeAdditionalEntity SysPatiChargeAddEntity, int? keyValue)
        {
            SysPatiChargeAddEntity.zt = SysPatiChargeAddEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeAddApp.SubmitForm(SysPatiChargeAddEntity, keyValue);
            return Success("Thao tác thành công。");
        }

    }
}
