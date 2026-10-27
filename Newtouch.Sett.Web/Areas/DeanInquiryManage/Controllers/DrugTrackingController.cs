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

    public class DrugTrackingController : Newtouch.Sett.Web.Controllers.ControllerBase
    { 

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public DrugTrackingController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetDrugTracking(string kssj, string jssj,string type)
        {
            var result = _deanInquiryDmnService.DrugTrackingEntity_Lsqs(kssj,jssj,this.OrganizeId,type);
            return Content(result.ToJson());
        }

        public IActionResult Profitandlossranking(string kssj, string jssj)
        {
            
            var result = _deanInquiryDmnService.Profitandlossranking_Lsqs(kssj,jssj, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult DoctorBillingRanking(string kssj, string jssj,string Ypcode)
        {
            var result = _deanInquiryDmnService.DoctorBillingRanking_Lsqs(kssj,jssj,Ypcode, this.OrganizeId);
            return Content(result.ToJson());
        }
    }
}
