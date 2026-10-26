using Newtouch.Core.Common;
using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects.TherapeutistCompleteManage;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ITherapeutistCompleteDmnService
    {
        #region 治疗师工作列表
        IList<TherapeutistListGridVO> GetTherapeutistListWorkedList(Pagination pagination, string BeginDate, string EndDate, string hzxm, string zls, string dl, string orgId);
        #endregion

        #region 治疗师工作时间
        /// <summary>
        /// Danh sách sắp xếp thời gian làm việc kỹ thuật viên điều trị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="ysgh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<TherapeutistWorkPlanGridVO> GetTherapeutistWorkPlanList(Pagination pagination, int? year, int? month, string ysgh, string orgId);

        /// <summary>
        /// Lấy tất cả kỹ thuật viên điều trị và số ngày mặc định
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<AddStaffPlanVO> GetAllRehabDoctor(string orgId);

        /// <summary>
        /// Khi thêm lịch trực mới,Gửi vào CSDL
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="orgId"></param>
        void SubmitPlan(Dictionary<string, List<AddStaffPlanVO>> vo, string orgId);

        /// <summary>
        /// Xem thông tin lịch trực một tháng của một kỹ thuật viên điều trị
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        EditPlanVO GetFormJson(string keyvalue, string orgId);

        /// <summary>
        /// Biên tập tình hình một thời gian của một kỹ thuật viên điều trị
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyvalue"></param>
        void EditRehabDoctorRange(DoctorWorkingDaysPlanEntity entity, string keyvalue, string orgId);
        /// <summary>
        /// Xóa
        /// </summary>
        /// <param name="keyvalue"></param>
        void DelArrange(string keyvalue);
        #endregion

        #region 时长调整

        /// <summary>
        /// Khi điều chỉnh thời lượng,Thống kê thời lượng kỹ thuật viên điều trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        List<AdjustStaffHourVO> GetStaffWorkedHour(string orgId, string gh, string year, string month);

        /// <summary>
        /// Lấy tổng thời lượng hàng tháng của kỹ thuật viên điều trị
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="gh"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        IList<AdjustStaffHourVO> GetStaffEachMonthHour(Pagination pagination, string orgId, string gh, string year,
            string month);

        void UpdateTime(Dictionary<string, List<submitTimeVO>> vo);

        #endregion

        #region 治疗师工作统计
        IList<StaffReportVO> GetStaffReport(Pagination pagination, string orgId, string ysgh, string year, string month);
        #endregion

        #region 治疗师工作效率柱状图
        SCNumBO GetVisitSC(string orgId);

        /// <summary>
        /// Lấy chi tiết hiệu suất làm việc kỹ thuật viên điều trị
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        IList<AdjustStaffHourVO> GetVisitDetailSC(Pagination pagination,string orgId, string type, string time);
        #endregion

    }
}
