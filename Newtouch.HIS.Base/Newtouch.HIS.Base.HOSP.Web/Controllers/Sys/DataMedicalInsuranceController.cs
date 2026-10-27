using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Newtouch.HIS.Base.HOSP.Web.Controllers
{
    public class DataMedicalInsuranceController : ControllerBase
    {
        // GET: DataMedicalInsurance
        public IActionResult Index()
        {
            return View();
        }
    }
}