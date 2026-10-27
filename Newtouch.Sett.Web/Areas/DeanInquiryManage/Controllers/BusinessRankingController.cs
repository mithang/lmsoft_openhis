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

    public class BusinessRankingController : Newtouch.Sett.Web.Controllers.ControllerBase
    {
         
        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public BusinessRankingController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// ngoại trúkhámy tếsinhcônglàmlượng
        /// </summary>
        public IActionResult GetMzysgzl(string kssj, string jssj)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_mzysgzl(kssj, jssj, this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// ngoại trúkhámkhoaphòngthuvào
        /// </summary>
        public IActionResult GetMzkssr(string kssj, string jssj)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_mzkssr(kssj, jssj, this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// nhập việnviệnnghiệpvụkhoaphòngthuvào
        /// </summary>
        public IActionResult GetZyywks(string kssj, string jssj)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_zyywks(kssj, jssj, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// y tếsinhnghiệpvụthuvào
        /// </summary>
        public IActionResult GetYsyw(string kssj, string jssj, string bqCode)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_ysyw(kssj, jssj, OrganizeId,bqCode);
            return Content(list.ToJson());
        }

    }
}
