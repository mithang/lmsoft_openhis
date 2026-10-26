using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class InpatientCostAnalysisController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View(); 
        }

        /// <summary>
        /// Phân tích chi phí nội trú_Dữ liệu tiêu đề
        /// </summary>
        public ActionResult GetTitleData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_TitleData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-Phân tích chi phí nội trú_Phần phân tích chi phí khoa
        /// </summary>
        public ActionResult ZYFYFX_KSFYFXDTO(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.ZYFYFX_KSFYFXDTO(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-Phân tích chi phí nội trú_Phân tích chi phí bệnh nhân xuất viện
        /// </summary>
        public ActionResult GetCYHZFYFData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }
        /// <summary>
        /// Tra cứu của viện trưởng-Phân tích chi phí nội trú_Biểu đồ thống kê
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="rtype"></param>
        /// <returns></returns>
        public ActionResult GetCYHZFYFTJData(string ksrq, string jsrq, string rtype)
        {
            var dto = _deanInquiryDmnService.GetCYHZFYFTJData(this.OrganizeId, ksrq, jsrq, rtype);
            return Content(dto.ToJson());
        }

    }
}