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

namespace Newtouch.Sett.Web.Areas.PatientManage.Controllers
{

    [Area("PatientManage")]

    public class PatientOutOfHosListController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private IPatientListPrintDmnService _printService;

        public PatientOutOfHosListController(IPatientListPrintDmnService printService)
        {
            this._printService = printService;
        }

        /// <summary>
        /// Tìm theo điều kiện
        /// </summary>
        /// <param name="patientListVO"></param>
        /// <returns></returns>
        public IActionResult LoadData(PatientListInputVO patientListVO)
        {
            var data = _printService.PatientList(patientListVO);
            if (data != null)
            {
                return Content(data.ToJson());
            }
            return View();
        }
    }
}
