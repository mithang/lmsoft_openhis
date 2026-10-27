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

    public class OutpatientWorkloadController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public OutpatientWorkloadController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// Khối lượng công việc khoa ngoại trú
        /// </summary>
        public IActionResult GetMzksgzl(string ksrq, string jsrq)
        {
            var list = _deanInquiryDmnService.OutpatientWorkloadEntiy_Mzksgzl(ksrq,jsrq,this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Khối lượng công việc bác sĩ
        /// </summary>
        public IActionResult GetYsgzl(string ksrq, string jsrq, string ks)
        {
            var list = _deanInquiryDmnService.OutpatientWorkloadEntiy_Ysgzl(ksrq, jsrq, this.OrganizeId,ks);
            return Content(list.ToJson());
        }

    }
}
