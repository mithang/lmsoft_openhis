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
        /// thuphíxác nhậnnhận
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// nơiphươnghoànphí
        /// </summary>
        /// <returns></returns>
        public IActionResult Refund()
        {
            return View();
        }

        /// <summary>
        /// nơiphươngcộtbảngtrangmặt
        /// </summary>
        /// <returns></returns>
        public IActionResult PrescriptionForm()
        {
            return View();
        }
    }
}
