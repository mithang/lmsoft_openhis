namespace Newtouch.HIS.Domain.ValueObjects.SystemManage
{
    /// <summary>
    /// Bảng thống kê thu chi của trạm Của thông tin thu nhập gridlist
    /// </summary>
    public class jgssEarningGridVO
    {
        public decimal mzsfje { get; set; }
        public decimal mzzxje { get; set; }
        public decimal zysfje { get; set; }
        public decimal zyzxje { get; set; }
        public decimal zsr { get; set; }
        public string dlmc { get; set; }
        public string dlCode { get; set; }
        /// <summary>
        /// Xác minh thu nhập
        /// </summary>
        public decimal hssr { get; set; }
        public decimal ce { get; set; }
        public string tzsm { get; set; }
        public decimal fcbl { get; set; }
    }
}
