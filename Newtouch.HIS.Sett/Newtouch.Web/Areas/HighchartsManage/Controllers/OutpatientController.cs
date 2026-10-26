using Newtouch.Common;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using System.Web.Mvc;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Web.Areas.HighchartsManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OutpatientController : ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;

        #region 页面初始化
        /// <summary>
        /// Phân tích số người lần người khám bệnh ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberofOutpatientVisits()
        {
            return View();
        }
        /// <summary>
        /// Phân tích thu nhập ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberofOutpatientSalary()
        {
            return View();
        }
        #endregion

        #region 门诊费用报表
        public ActionResult PatientVisitPerTherapist()
        {
            return View();
        }

        /// <summary>
        /// Lấy chi phí khám bệnh
        /// </summary>
        public ActionResult GetVisitNum(string year, string datatype, string orgId)
        {
            var PatientVisitPerThe = new PatientVisitPerThe();
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("请选择年份！");
            }
            if (string.IsNullOrWhiteSpace(datatype))
            {
                throw new FailedException("请选择类型！");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("请选择组织机构！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetTherapistavgDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetTherapistavgVisit(year, orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetTherapistDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetTherapistVisit(year, orgId);
            }


            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }

        public ActionResult GetMonthVisitNum(string year, string datatype, string orgId,int month) {
            var PatientVisitPerThe = new PatientMonthVisitPerThe();
            if (string.IsNullOrWhiteSpace(year))
            {
                throw new FailedException("请选择年份！");
            }
            if (string.IsNullOrWhiteSpace(datatype))
            {
                throw new FailedException("请选择类型！");
            }
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("请选择组织机构！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthTherapistavgVisit(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistavgDischarge(year, orgId, month < 10 ? "0" + month : month + "");
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthTherapistVisit(year, orgId, month < 10 ? "0" + month : month + "");
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }
        #endregion


        /// <summary>
        ///Lấy số người khám ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOutpatientVisitNum()
        {
            var data = _highchartsDmnService.GetOutpatientVisitNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        /// <summary>
        /// Lấy lần người khám bệnh ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOutpatientVisitPerNum()
        {
            var data = _highchartsDmnService.GetOutpatientVisitPerNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }


        /// <summary>
        /// Lấy thống kê thu nhập ngoại trú
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOutpatientSalaryNum()
        {
            var data = _highchartsDmnService.GetOutpatientSalaryNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

    }
}