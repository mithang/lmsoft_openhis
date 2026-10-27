namespace NewtouchHIS.WebAPI.Manage.Models.InHosPatient
{
    /// <summary>
    /// Truy vấn hóa đơn nội trúRequest
    /// </summary>
    public class InHosPatBillRequest
    {
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Số giấy tờ
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// Số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
    }
}
