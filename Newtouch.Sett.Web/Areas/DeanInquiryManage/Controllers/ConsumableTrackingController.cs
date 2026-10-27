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

namespace Newtouch.Sett.Web.Areas.DeanInquiryManage.Controllers
{

    [Area("DeanInquiryManage")]

    public class ConsumableTrackingController : Newtouch.Sett.Web.Controllers.ControllerBase
    { 

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public ConsumableTrackingController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Consumablestatistics(string kssj, string jssj)
        {
      
            var result = _deanInquiryDmnService.Consumablestatistics_Lsqs(kssj, jssj, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult DoctorBillingJHCRanking(string kssj, string jssj, string sfxmdm)
        {
           
            var result = _deanInquiryDmnService.DoctorBillingRankingHC_Lsqs(kssj, jssj, sfxmdm, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult ProfitandlossrankingHC()
        {
            var result = _deanInquiryDmnService.ProfitandlossrankingHC_Lsqs("2023-03-01", "2023-09-08", this.OrganizeId);
            return Content(result.ToJson());
        }
    }
}
