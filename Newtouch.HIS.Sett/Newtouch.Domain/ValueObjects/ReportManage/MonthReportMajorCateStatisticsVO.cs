namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Báo cáo tháng Theo loại lớn Thống kê VO
    /// </summary>
    public class MonthReportMajorCateStatisticsVO
    {
        /// <summary>
        /// Loại bệnh nhân inpatient/outpatient
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Số bệnh án
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// Số nhập viện/Số khám ngoại trú
        /// </summary>
        public string zyhmzh { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// Loại lớn báo cáoCode(Loại lớn thu phí)
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Loại lớn Tổng số tiền
        /// </summary>
        public decimal dlje { get; set; }

        /// <summary>
        /// Ghi chú(Giải thích bù chênh lệch)
        /// </summary>
        public string bz { get; set; }

    }
}
