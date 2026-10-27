namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    /// <summary>
    /// Đăng ký khám ngoại trú
    /// </summary>
    public class OutpatientRegistRequest
    {
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Số đặt hẹn
        /// </summary>
        public string? BookId { get; set; }
        /// <summary>
        /// Lịch trựcId
        /// </summary>
        public string? ScheduId { get; set; }
        /// <summary>
        /// Tính chất đăng ký khám Tự chi trả, BHYT v.v. brxz Tương đương
        /// </summary>
        public string? ghxz { get; set; }
        /// <summary>
        /// Phương thức thanh toán xjzffs
        /// </summary>
        public string? PayWay { get; set; }
        /// <summary>
        /// Chi phí đã thanh toán
        /// </summary>
        public string? PayFee { get; set; }
        /// <summary>
        /// Số giao dịch thanh toán
        /// </summary>
        public string? PayLsh { get; set; }

    }
    /// <summary>
    /// Truy vấn hồ sơ đăng ký khám
    /// </summary>
    public class OutpRegistQueryRequest
    {
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
