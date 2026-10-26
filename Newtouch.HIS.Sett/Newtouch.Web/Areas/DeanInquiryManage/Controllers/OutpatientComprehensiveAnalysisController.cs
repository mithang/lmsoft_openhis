using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class OutpatientComprehensiveAnalysisController : ControllerBase
    {

        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Phân tích tổng hợp ngoại trú_Dữ liệu tiêu đề
        /// </summary>
        public ActionResult GetTitleData(string ksrq,string jsrq,string rtype)
        { 
            var dto = _deanInquiryDmnService.OutpatientComprehensiveAnalysisEntiy_TitleData(this.OrganizeId, ksrq, jsrq,rtype);
            return Content(dto.ToJson());
        }

        /// <summary>
        /// Phân tích tổng hợp ngoại trú_Phân tích bệnh nhân ngoại trú
        /// </summary>
        public ActionResult GetMzhzfx(string ksrq, string jsrq,string rtype)
        {
            var list = _deanInquiryDmnService.OutpatientComprehensiveAnalysisEntiy_Mzhzfx(this.OrganizeId, ksrq, jsrq);
            return Content(list.ToJson());
        }
    }
}