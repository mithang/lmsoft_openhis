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
        /// 住院费用分析_抬头数据
        /// </summary>
        public IActionResult GetTitleData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_TitleData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 院长查询-住院费用分析_科室费用分析部分
        /// </summary>
        public IActionResult ZYFYFX_KSFYFXDTO(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_KSFYFXDTO(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 院长查询-住院费用分析_出院患者费用分析
        /// </summary>
        public IActionResult GetCYHZFYFData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// 院长查询-住院费用分析_统计图
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
