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

    public class SysChargeAddCategController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly ISysChargeAddCategApp _SysChargeAddCategApp;
        private readonly ISysPatiChargeAddApp _SysPatiChargeAddApp;
        public SysChargeAddCategController(ISysChargeAddCategApp sysChargeAddCategApp, ISysPatiChargeAddApp sysPatiChargeAddApp)
        {
            _SysChargeAddCategApp = sysChargeAddCategApp;
            _SysPatiChargeAddApp = sysPatiChargeAddApp;
        }


        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult SysPatiChargeAdd()
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
            var data = _SysChargeAddCategApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(int keyValue)
        {
            _SysChargeAddCategApp.DeleteForm(keyValue);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysChargeAddCategEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysChargeAdditionalCategoryEntity sysChargeAddCategEntity, int keyValue)
        {
            _SysChargeAddCategApp.SubmitForm(sysChargeAddCategEntity, keyValue);
            return Success("Thao tác thành công。");
        }
        //附加显示大类下拉框
        public IActionResult GetSysChargeAddCategSelect()
        {
            var treeList = _SysChargeAddCategApp.GetSysChargeAddCategSelect();
            return Content(treeList);
        }
    }
}
