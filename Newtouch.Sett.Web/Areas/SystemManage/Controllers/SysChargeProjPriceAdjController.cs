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
        /// khung nhìn
        /// </summary>
        /// <returns></returns>
        public IActionResult SysChargeProjPriceAdj()
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
            var data = _sysChargeProjPriceAdjApp.GetEffectiveList(keyword);
            return Content(data.ToJson());
        }
        /// <summary>
        /// Thêm mới hạngmụcđiềugiákhôngcho phépcho phépsửasửa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult GetFormJson(int? keyValue)
        {
            var entity = _sysChargeProjPriceAdjApp.GetForm(keyValue);
            return Content(entity.ToJson());
        }
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IActionResult DeleteForm(SysChargeItemPriceAdjustEntity sysChargeProjPriceAdjEntity)
        {
            _sysChargeProjPriceAdjApp.DeleteForm(sysChargeProjPriceAdjEntity);
            return Success("Thao tác thành công。");
        }
        /// <summary>
        /// Lưu
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
