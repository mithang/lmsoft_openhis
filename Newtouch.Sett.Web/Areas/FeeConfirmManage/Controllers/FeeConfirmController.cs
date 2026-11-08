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

namespace Newtouch.Sett.Web.Areas.FeeConfirmManage.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Area("FeeConfirmManage")]

    public class FeeConfirmController : Controller
    {
        // GET: FeeConfirmManage/FeeConfirm
        /// <summary>
        /// thu phí xác nhận nhận
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// nơi phương hoàn phí
        /// </summary>
        /// <returns></returns>
        public IActionResult Refund()
        {
            return View();
        }

        /// <summary>
        /// nơi phương cột bảng trang mặt
        /// </summary>
        /// <returns></returns>
        public IActionResult PrescriptionForm()
        {
            return View();
        }
    }
}
