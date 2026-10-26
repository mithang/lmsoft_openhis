
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.ReportManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices.ReportManage
{
    public interface IHighchartsDmnService
    {
        #region 门诊记账1.2 and 1.3版本，不适用多个治疗师和医院的记账逻辑
        /// <summary>
        /// Lấy tổng chi phí điều trị của kỹ thuật viên điều trị
        /// </summary>
        List<TherapistVisit> GetTherapistDischarge(string year, string orgId);

        /// <summary>
        /// Lấy tổng số người điều trị kỹ thuật viên điều trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetTherapistVisit(string year, string orgId);

        /// <summary>
        /// Lấy chi phí bình quân(Tổng chi phí/Tổng lượt người)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetTherapistavgDischarge(string year, string orgId);
        /// <summary>
        /// Lấy lượt người điều trị bình quân kỹ thuật viên điều trị(Tổng lượt người/Tổng số người)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetTherapistavgVisit(string year, string orgId);

        /// <summary>
        /// Lấy lượt người điều trị bình quân tháng kỹ thuật viên điều trị(Tổng lượt người/Tổng số người)
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthVisit> GetMonthTherapistavgVisit(string year, string orgId, string month);

        /// <summary>
        /// Lấy tổng chi phí điều trị của kỹ thuật viên điều trị
        /// </summary>
        List<TherapistMonthCharge> GetMonthTherapistDischarge(string year, string orgId, string month);

        /// <summary>
        /// Lấy số người điều trị tháng kỹ thuật viên điều trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthVisit> GetMonthTherapistVisit(string year, string orgId, string month);


        /// <summary>
        /// Lấy chi phí bình quân tháng(Tổng chi phí/Tổng lượt người)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthCharge> GetMonthTherapistavgDischarge(string year, string orgId, string month);
        #endregion

        #region 门诊 
        /// <summary>
        /// Lấy số người khám ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientVisitNumVO> GetOutpatientVisitNum(string orgId);

        /// <summary>
        /// Lấy lượt người khám ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientVisitNumVO> GetOutpatientVisitPerNum(string orgId);


        /// <summary>
        /// Thống kê thu nhập ngoại trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientSCNumVO> GetOutpatientSalaryNum(string orgId);
        #endregion

        #region 住院
        /// <summary>
        /// Lấy thống kê thu nhập nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<InpatientSCNumVO> GetInpatientSalaryNum(string orgId);

        /// <summary>
        /// Lấy số người nội trú tăng thêm
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<OutpatientVisitNumVO> GetInpatientVisitNum(string orgId);

        /// <summary>
        /// Lấy số người xuất viện
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<InpatientVisitNumVO> GetInpatientDischargeNum(string orgId);

        /// <summary>
        /// Lấy tổng chi phí điều trị nội trú của kỹ thuật viên điều trị
        /// </summary>
        List<TherapistVisit> GetInTherapistDischarge(string year, string orgId);

        /// <summary>
        /// Lấy lượt người điều trị nội trú của kỹ thuật viên điều trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetInTherapistVisit(string year, string orgId);

        /// <summary>
        /// Lấy chi phí nội trú bình quân của điều trị(Tổng chi phí/Tổng lượt người)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetInTherapistavgDischarge(string year, string orgId);
        /// <summary>
        /// Lấy tổng chi phí điều trị nội trú tháng của kỹ thuật viên điều trị
        /// </summary>
        List<TherapistMonthCharge> GetMonthInTherapistDischarge(string year, string orgId, string month);
        /// <summary>
        /// Lấy chi phí điều trị nội trú bình quân tháng của kỹ thuật viên điều trị
        /// </summary>
        List<TherapistMonthCharge> GetMonthInTherapistavgDischarge(string year, string orgId, string month);
        /// <summary>
        /// Lấy tổng lượt thăm điều trị nội trú tháng của kỹ thuật viên điều trị
        /// </summary>
        List<TherapistMonthVisit> GetMonthInTherapistVisit(string year, string month, string orgId);

        /// <summary>
        /// Lấy lượt người điều trị nội trú bình quân tháng của kỹ thuật viên điều trị
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistMonthVisit> GetMonthInTherapistavgVisit(string year, string month, string orgId);

        /// <summary>
        /// Lấy lượt người điều trị nội trú bình quân của kỹ thuật viên điều trị
        /// </summary>
        /// <param name="year"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<TherapistVisit> GetInTherapistavgVisit(string year, string orgId);
        #endregion

        #region Common
        /// <summary>
        /// So sánh thống kê thu nhập ngoại trú/nội trú
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        SCNumBO GetSalaryNumCompared(string orgId);
        #endregion

    }
}
