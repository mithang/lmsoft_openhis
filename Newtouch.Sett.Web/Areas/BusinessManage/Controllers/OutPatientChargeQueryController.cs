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

namespace Newtouch.Sett.Web.Areas.BusinessManage.Controllers
{

    [Area("BusinessManage")]

    public class OutPatientChargeQueryController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        // GET: BusinessManage/OutPatienChargeQuery
        private readonly IOutPatienChargeQueryApp _outPatienChargeQueryApp;
        public OutPatientChargeQueryController(IOutPatienChargeQueryApp outPatienChargeQueryApp)
        {
            this._outPatienChargeQueryApp = outPatienChargeQueryApp;
        }

        /// <summary>
        /// treosốthuphítratra cứu
        /// </summary>
        /// <returns></returns>
        public IActionResult OutPatientChargeQuery()
        {
            return View();
        }
        /// <summary>
        /// treosốthuphídònghạngtratra cứu
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="fph"></param>
        /// <param name="xm"></param>
        /// <param name="CreatorCode"></param>
        /// <param name="CreateTimestart"></param>
        /// <param name="CreateTimeEnd"></param>
        /// <returns></returns>
        public IActionResult SelectRegChargeQuery(string kh = "", string fph="", string xm="", string syy = "", string CreateTimestart="", string CreateTimeEnd="")
        {
            var chargeQueryList = _outPatienChargeQueryApp.SelectRegChargeQuery(kh, fph, xm, syy, CreateTimestart, CreateTimeEnd);
            return Content(chargeQueryList.ToJson());
        }
        /// <summary>
        /// treosốthuphítratra cứurõchi tiết
        /// </summary>
        /// <param name="jsnm"></param>
        /// <returns></returns>
        public IActionResult GetRecordsByJsnm(string jsnm)
        {
            var regChargeDetailList = _outPatienChargeQueryApp.GetRecordsByJsnm(jsnm);
            return Success("", regChargeDetailList);
        }


    }
}
