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

    public class SysAgInsurChargeCategController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private ISysAgInsurChargeCategApp _sysAgInsurChargeCategApp;

        public SysAgInsurChargeCategController(ISysAgInsurChargeCategApp sysAgInsurChargeCategApp)
        {
            this._sysAgInsurChargeCategApp = sysAgInsurChargeCategApp;
        }
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult SysAgInsurChargeCateg()
        {
            return View();
        }
        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IActionResult GetGridJson(int keyword)
        {
            var data = _sysAgInsurChargeCategApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增或修改Form
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int keyValue)
        {
            var entity = _sysAgInsurChargeCategApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(int keyValue)
        {
            _sysAgInsurChargeCategApp.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysAgriInsuranceChargeCategoryEntity sysAgInsurChargeCategEntity, int keyValue)
        {
            _sysAgInsurChargeCategApp.SubmitForm(sysAgInsurChargeCategEntity, keyValue);
            return Success("Thao tác thành công。");
        }


    }
}
