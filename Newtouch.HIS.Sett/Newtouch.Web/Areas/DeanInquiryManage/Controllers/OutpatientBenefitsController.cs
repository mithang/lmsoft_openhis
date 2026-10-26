using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientBenefitsController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        { 
            return View();
        }

        /// <summary>
        /// Chi tiết hiệu quả ngoại trú
        /// </summary>
        public ActionResult GetMzxymx(string orgId, string ksrq, string jsrq)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Mzxymx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Hiệu quả bác sĩ
        /// </summary>
        public ActionResult GetYsxy(string orgId, string ksrq, string jsrq, string rtype, string type)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Ysxy(this.OrganizeId, ksrq, jsrq, type);
            return Content(list.ToJson());
        }
        /// <summary>
        /// Xu hướng lịch sử
        /// </summary>
        public ActionResult GetLsqs(string ksrq, string jsrq, string rtype, string type)
        {
            var list = _deanInquiryDmnService.OutpatientCostEntiy_Lsqs(this.OrganizeId, ksrq, jsrq, rtype, type);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Xếp hạng hiệu quả khoa
        /// </summary>
        public ActionResult GetKSXYPM(string ksrq, string jsrq, string rtype, string type)
        {
            if (type == "1")//Lượt khám chữa bệnh
            {
                var list = _deanInquiryDmnService.GetKSXYPM(this.OrganizeId, ksrq, jsrq);
                return Content(list.ToJson());
            }
            else//Thu nhập khám chữa bệnh thực tế
            {
                var list = _deanInquiryDmnService.GetKSXYPMBySJSY(this.OrganizeId, ksrq, jsrq);
                return Content(list.ToJson());
            }
        }

    }
}