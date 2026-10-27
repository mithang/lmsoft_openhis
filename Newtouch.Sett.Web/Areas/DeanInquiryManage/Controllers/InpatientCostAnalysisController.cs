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

    public class InpatientCostAnalysisController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public InpatientCostAnalysisController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// Phân tích chi phí nội trú_Dữ liệu tiêu đề
        /// </summary>
        public IActionResult GetTitleData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_TitleData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-Phân tích chi phí nội trú_khoaphòngphídùngphânphân tíchbộphân
        /// </summary>
        public IActionResult ZYFYFX_KSFYFXDTO(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_KSFYFXDTO(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-Phân tích chi phí nội trú_Phân tích chi phí bệnh nhân xuất viện
        /// </summary>
        public IActionResult GetCYHZFYFData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-Phân tích chi phí nội trú_thốngtínhbiểu đồ
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public IActionResult GetCYHZFYFTJData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFTJData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }

    }
}
