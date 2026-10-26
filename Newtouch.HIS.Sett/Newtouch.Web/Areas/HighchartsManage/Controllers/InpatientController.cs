using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.IDomainServices.ReportManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.HighchartsManage.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class InpatientController : ControllerBase
    {
        private readonly IHighchartsDmnService _highchartsDmnService;


        #region 页面初始化
        /// <summary>
        /// Phân tích số lượt khám nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberofInpatientVisits()
        {
            return View();
        }
        /// <summary>
        /// Phân tích thu nhập nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult NumberofInpatientSalary()
        {
            return View();
        }

        public ActionResult InPatientVisitPerTherapist()
        {
            return View();
        }
        #endregion

        /// <summary>
        ///Lấy số người khám nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInpatientVisitNum()
        {
            //Lấy số người nội trú mới tăng
            List<OutpatientVisitNumVO> outpatientlist = _highchartsDmnService.GetInpatientVisitNum(OrganizeId);
            //Lấy số người xuất viện
            List<InpatientVisitNumVO> inpatientList = _highchartsDmnService.GetInpatientDischargeNum(OrganizeId);
            //Đặt vào một đối tượng，Trả về trang
            var data = new VisitNumBO
            {
                OutpatientList = outpatientlist,
                InpatientList = inpatientList
            };
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        /// <summary>
        /// Lấy thống kê thu nhập nội trú
        /// </summary>
        /// <returns></returns>
        public ActionResult GetInpatientSalaryNum()
        {
            var data = _highchartsDmnService.GetInpatientSalaryNum(OrganizeId);
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = data }.ToJson());
        }

        #region 住院费用报表
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
            if (string.IsNullOrWhiteSpace(orgId))
            {
                throw new FailedException("请选择组织机构！");
            }
            if (datatype == "avg")
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetInTherapistavgDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetInTherapistavgVisit(year, orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetInTherapistDischarge(year, orgId);
                PatientVisitPerThe.visitper = _highchartsDmnService.GetInTherapistVisit(year, orgId);
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }

        public ActionResult GetMonthVisitNum(string year, string datatype, string orgId, int month)
        {
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
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthTherapistavgDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthInTherapistavgVisit(year,month < 10 ? "0" + month : month + "", orgId);
            }
            else
            {
                PatientVisitPerThe.discharge = _highchartsDmnService.GetMonthInTherapistDischarge(year, orgId, month < 10 ? "0" + month : month + "");
                PatientVisitPerThe.visitper = _highchartsDmnService.GetMonthInTherapistVisit(year, month < 10 ? "0" + month : month + "", orgId);
            }

            return Content(new AjaxResult { state = ResultType.success.ToString(), message = null, data = PatientVisitPerThe }.ToJson());
        }
        #endregion
    }
}