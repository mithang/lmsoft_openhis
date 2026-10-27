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

    public class SysPatiChargeRangeController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: SystemManage/SysPatiChargeRange
        private readonly ISysPatiChargeRangeApp _sysPatiChargeRangeApp;
        private readonly ISysMedicinApp _sysMedicinApp;
        public SysPatiChargeRangeController(ISysPatiChargeRangeApp sysPatiChargeRangeApp, ISysMedicinApp sysMedicinApp)
        {
            _sysPatiChargeRangeApp = sysPatiChargeRangeApp;
            _sysMedicinApp = sysMedicinApp;
        }


        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult SysPatiChargeRange()
        {
            return View();
        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(string keyword)
        {
            var data = _sysPatiChargeRangeApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysPatiChargeRangeApp.GetEffectiveList(null, keyValue).FirstOrDefault();
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(int keyValue)
        {
            _sysPatiChargeRangeApp.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysPatientChargeRangeEntity sysPatiChargeRangeEntity, int? keyValue)
        {
            sysPatiChargeRangeEntity.zt = sysPatiChargeRangeEntity.zt == "true" ? "1" : "0";
            _sysPatiChargeRangeApp.SubmitForm(sysPatiChargeRangeEntity, keyValue );
            return Success("Thao tác thành công。");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywrod"></param>
        /// <returns></returns>
        public IActionResult GetYpSelectJsonData(string keywrod)
         {
            var list = _sysMedicinApp.GetYpList(keywrod);
            return Content(list.ToJson());
        }

    }
}
