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

    public class OutpatientBenefitsController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public OutpatientBenefitsController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// Chi tiết hiệu quả ngoại trú
        /// </summary>
        public IActionResult GetMzxymx(string orgId, string ksrq, string jsrq)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzxymx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Hiệu quả bác sĩ
        /// </summary>
        public IActionResult GetYsxy(string orgId, string ksrq, string jsrq, string rtype, string type)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Ysxy(this.OrganizeId, ksrq, jsrq, type);
            return Content(list.ToJson());
        }
        /// <summary>
        /// Xu hướng lịch sử
        /// </summary>
        public IActionResult GetLsqs(string ksrq, string jsrq, string rtype, string type)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Lsqs(this.OrganizeId, ksrq, jsrq, rtype, type);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Xếp hạng hiệu quả khoa
        /// </summary>
        public IActionResult GetKSXYPM(string ksrq, string jsrq, string rtype, string type)
        {
            if (type == "1")//lượt người chẩn trị
            {
                var list = _deanInquiryDmnService.GetKSXYPM(this.OrganizeId, ksrq, jsrq);
                return Content(list.ToJson());
            }
            else//Thu nhập chẩn trị thực tế
            {
                var list = _deanInquiryDmnService.GetKSXYPMBySJSY(this.OrganizeId, ksrq, jsrq);
                return Content(list.ToJson());
            }
        }

    }
}
