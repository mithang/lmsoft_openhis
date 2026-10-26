using Newtouch.HIS.Domain.IDomainServices.DeanInquiryManage;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.DeanInquiryManage.Controllers
{
    public class BusinessRankingController : ControllerBase
    {
         
        private readonly IDeanInquiryDmnService _deanInquiryDmnService;

        // GET: DeanInquiryManage/DailyUpdates
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Khối lượng công việc của bác sĩ ngoại trú
        /// </summary>
        public ActionResult GetMzysgzl(string kssj, string jssj)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_mzysgzl(kssj, jssj, this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thu nhập khoa ngoại trú
        /// </summary>
        public ActionResult GetMzkssr(string kssj, string jssj)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_mzkssr(kssj, jssj, this.OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thu nhập khoa nghiệp vụ nội trú
        /// </summary>
        public ActionResult GetZyywks(string kssj, string jssj)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_zyywks(kssj, jssj, OrganizeId);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Thu nhập nghiệp vụ của bác sĩ
        /// </summary>
        public ActionResult GetYsyw(string kssj, string jssj, string bqCode)
        {
            var list = _deanInquiryDmnService.BusinessRankingEntiy_ysyw(kssj, jssj, OrganizeId,bqCode);
            return Content(list.ToJson());
        }

    }
}