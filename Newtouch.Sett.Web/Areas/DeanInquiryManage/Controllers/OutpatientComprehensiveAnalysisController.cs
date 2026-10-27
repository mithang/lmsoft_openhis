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

    public class OutpatientComprehensiveAnalysisController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public OutpatientComprehensiveAnalysisController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 门诊综合分析_抬头数据
        /// </summary>
        public IActionResult GetTitleData(string ksrq,string jsrq,string rtype)
        { 
            var dto = _deanInquiryDmnService.OutpatientComprehensiveAnalysisEntiy_TitleData(this.OrganizeId, ksrq, jsrq,rtype);
            return Content(dto.ToJson());
        }

        /// <summary>
        /// 门诊综合分析_门诊患者分析
        /// </summary>
        public IActionResult GetMzhzfx(string ksrq, string jsrq,string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientComprehensiveAnalysisEntiy_Mzhzfx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }
    }
}
