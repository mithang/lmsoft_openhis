namespace Newtouch.HIS.Domain.ValueObjects.ReportManage
{
    /// <summary>
    /// 
    /// </summary>
    public class PerformanceIndicatorVO
    {
        /// <summary>
        /// Ngày tháng Ví dụ:201701
        /// </summary>
        public string col { get; set; }
        /// <summary>
        /// Tổng số người
        /// </summary>
        public int zrs { get; set; }
        /// <summary>
        /// Tổng lượt người
        /// </summary>
        public int zrc { get; set; }
        /// <summary>
        /// Lượt người bình quân
        /// </summary>
        public decimal pjrc { get; set; }
    }
}
