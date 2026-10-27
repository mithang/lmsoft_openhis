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

    public class HospitalizationWorkloadController : Newtouch.Sett.Web.Controllers.ControllerBase
    { 

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public HospitalizationWorkloadController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Consumablestatistics()
        {
            var zzqs = _deanInquiryDmnService.zzqs_Lsqs("2023-03-01", this.OrganizeId);
            var ryqs = _deanInquiryDmnService.ryqs_Lsqs("2023-03-01", this.OrganizeId);
            var cyqs = _deanInquiryDmnService.cyqs_Lsqs("2023-03-01", this.OrganizeId);
            NumberTrendList result = new NumberTrendList();
            result.ryqs = ryqs;
            result.cyqs = cyqs;
            result.zzqs = zzqs;
            return Content(result.ToJson());
        }

        public IActionResult ConsumaPersonNumber(string kssj, string jssj)
        {
            
            var result = _deanInquiryDmnService.Rstj_Lsqs(kssj,jssj, this.OrganizeId);
            return Content(result.ToJson());
        }

        public IActionResult DepartmentWorkload(string kssj, string jssj)
        {

            var result = _deanInquiryDmnService.Ksgzl_Lsqs(kssj, jssj, this.OrganizeId);
            return Content(result.ToJson());
        }


        public IActionResult StaffWorkload(string kssj, string jssj,string bq)
        {
           
            var result = _deanInquiryDmnService.YSgzl_Lsqs(kssj, jssj, bq, this.OrganizeId);
            return Content(result.ToJson());
        }

    }
}
