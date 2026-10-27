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

    public class OutpatientCostAnalysisController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public OutpatientCostAnalysisController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// Phân tích chi phí ngoại trú theo loại
        /// </summary>
        public IActionResult GetMzfyflfx(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzfyflfx(this.OrganizeId,ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Phân tích chi phí ngoại trú theo loại_Biểu đồ
        /// </summary>
        public IActionResult GetMzfyflfx_tb(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzfyflfx_tb(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Phân tích chi phí bình quân bệnh nhân
        /// </summary>
        public IActionResult GetHzrjfyfx(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Hzrjfyfx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Phân tích chi phí bình quân bệnh nhân_Biểu tượng
        /// </summary>
        public IActionResult GetHzrjfyfx_tb(string ksrq, string jsrq, string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Hzrjfyfx_tb(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(list.ToJson());
        }

    }
}
