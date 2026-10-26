using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientWorkloadController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// Khối lượng công việc khoa ngoại trú
        /// </summary>
        public ActionResult GetMzksgzl(string ksrq, string jsrq)
        {
            var list = _deanInquiryDmnService.OutpatientWorkloadEntiy_Mzksgzl(ksrq,jsrq,this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Khối lượng công việc bác sĩ
        /// </summary>
        public ActionResult GetYsgzl(string ksrq, string jsrq, string ks)
        {
            var list = _deanInquiryDmnService.OutpatientWorkloadEntiy_Ysgzl(ksrq, jsrq, this.OrganizeId,ks);
            return Content(list.ToJson());
        }

    }
}