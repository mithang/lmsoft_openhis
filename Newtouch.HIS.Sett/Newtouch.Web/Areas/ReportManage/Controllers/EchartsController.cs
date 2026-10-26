/*******************************************************************************
 * Copyright © 2016 Newtouch Bản quyền thuộc về
 * Author: Newtouch
 * Description: 

*********************************************************************************/
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.Tools;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.ReportManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EchartsController : ControllerBase
    {
        private readonly IEchartsDmnService _echartsDmnService;

        //Chỉ số hiệu quả
        public ActionResult PerformanceIndicator()
        {
            return View();
        }

        /// <summary>
        /// Tổng số lượt người Số lượt người bình quân
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPerformanceIndicator(string orgId, string year)
        {
            var list = _echartsDmnService.SelectPerformanceIndicator(orgId, year);
            return Content(list.ToJson());
        }

        /// <summary>
        /// Tổng số lượt người Số lượt người bình quân
        /// </summary>
        /// <returns></returns>
        public ActionResult PerformanceMonthIndicator(string orgId, string year,int month)
        {
            var list = _echartsDmnService.PerformanceMonthIndicator(orgId, year,month < 10 ? "0" + month : month + "");
            return Content(list.ToJson());
        }

    }
}
