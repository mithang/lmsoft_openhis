namespace Newtouch.HIS.Domain.ValueObjects.ReportManage
{
    /// <summary>
    /// Thống kê thu nhập trạm
    /// </summary>
    public class SiteIncomeStatisticsVo
    {
        
        public string itemName { get; set; }

        /// <summary>
        /// Hạng mụccode
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// Tên hạng mục
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Số tiền thu phí ngoại trú
        /// </summary>
        public double opsfje { get; set; }

        /// <summary>
        /// Số tiền thực hiện điều trị ngoại trú
        /// </summary>
        public double opzlzxje { get; set; }

        /// <summary>
        /// Số tiền thu phí nội trú
        /// </summary>
        public double hpsfje { get; set; }

        /// <summary>
        /// Số tiền thực hiện điều trị nội trú
        /// </summary>
        public double hpzlzxje { get; set; }

        public double hssr { get; set; }
        public double ce { get; set; }
        public string tzsm { get; set; }
    }
}
