using Microsoft.AspNetCore.Mvc;
using Newtouch.Sett.Domain;
using Newtouch.Sett.DomainServices;
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

    public class PatientInHosListController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
        private readonly PatientInHosListService _inHosService;

        public PatientInHosListController(PatientInHosListService inHosService)
        {
            _inHosService = inHosService;
        }

        /// <summary>
        /// Tìm theo điều kiện. T-15: backed by the real inpatient query
        /// (zy_brjbxx) with server-side keyword filtering on name/admission
        /// number — the previous stub behind this endpoint always returned
        /// [], so filtering could never be proven.
        /// </summary>
        /// <param name="patientListVO"></param>
        /// <returns></returns>
        public IActionResult LoadData(PatientListInputVO patientListVO)
        {
            var data = _inHosService.PatientList(this.OrganizeId, patientListVO?.keyword);
            return Content(data.ToJson());
        }
    }
}
