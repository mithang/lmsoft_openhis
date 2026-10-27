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

    public class DailyUpdatesController : Newtouch.Sett.Web.Controllers.ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;
        public DailyUpdatesController(IDeanInquiryDmnService deanInquiryDmnService)
        {
            _deanInquiryDmnService = deanInquiryDmnService;
        }


        // GET: DeanInquiryManage/DailyUpdates
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// tất cảviệnthuvào
        /// </summary>
        public IActionResult GetQysr()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetQysr();
            return Content(list.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-nayngàyđộngtrạng thái-banner 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetJRDTBanner()
        {
            var dto = _deanInquiryDmnService.GetJRDTBanner(this.OrganizeId);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tình hình ngoại trú hôm nay
        /// </summary>
        public IActionResult GetJrdt()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetJrdt(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Đơn thuốc ngoại trú
        /// </summary>
        public IActionResult GetMzcf()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetMzcf(this.OrganizeId);
            return Content(list.ToJson());
        }
         
        /// <summary>
        /// ngoại trúkhámphídùng
        /// </summary>
        public IActionResult GetMzfy()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetMzfy(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tỷ lệ lấp đầy giường nội trú
        /// </summary>
        public IActionResult GetZyzcl()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetZyzcl(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thống kê đăng ký khám ngoại trú
        /// </summary>
        public IActionResult GetMzghtj()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetMzghtj(this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thống kê bệnh nhân nội trú
        /// </summary>
        public IActionResult GetZyhztj()
        {
            var list = _deanInquiryDmnService.DailyUpdates_GetZyhztj(this.OrganizeId);
            return Content(list.ToJson());
        }
    }
}
