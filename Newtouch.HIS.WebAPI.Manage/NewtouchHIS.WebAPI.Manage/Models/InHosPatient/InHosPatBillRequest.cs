namespace NewtouchHIS.WebAPI.Manage.Models.InHosPatient
{
    /// <summary>
    /// truy vấn hóa đơn nội trúRequest
    /// </summary>
    public class InHosPatBillRequest
    {
        /// <summary>
        /// số nội trú
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// số giấy tờ
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// số đơn hàng
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// số tiền đơn hàng
        /// </summary>
        public decimal? OrderAmt { get; set; }
    }
}
