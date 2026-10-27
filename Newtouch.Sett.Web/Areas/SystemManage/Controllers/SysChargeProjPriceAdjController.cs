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

    public class SysChargeProjPriceAdjController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: SystemManage/SysChargeProjPriceAdj
        private readonly ISysChargeProjPriceAdjApp _sysChargeProjPriceAdjApp;
        public SysChargeProjPriceAdjController(ISysChargeProjPriceAdjApp sysChargeProjPriceAdjApp)
        {
            _sysChargeProjPriceAdjApp = sysChargeProjPriceAdjApp;
        }


        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public IActionResult SysChargeProjPriceAdj()
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
            var data = _sysChargeProjPriceAdjApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 新增 项目调价不允许修改
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var entity = _sysChargeProjPriceAdjApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity)
        {
            _sysChargeProjPriceAdjApp.DeleteForm(sysChargeProjPriceAdjEntity);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult SubmitForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity, int? keyValue)
        {
            _sysChargeProjPriceAdjApp.SubmitForm(sysChargeProjPriceAdjEntity, keyValue);
            return Success("Thao tác thành công。");
        }
    }
}
