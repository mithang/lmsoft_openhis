namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    /// <summary>
    /// đăng ký khám ngoại trú
    /// </summary>
    public class OutpatientRegistRequest
    {
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// số hẹn
        /// </summary>
        public string? BookId { get; set; }
        /// <summary>
        /// lịch trựcId
        /// </summary>
        public string? ScheduId { get; set; }
        /// <summary>
        /// tính chất đăng ký khám tự trả, bảo hiểm y tế, v.v. brxz tương đương
        /// </summary>
        public string? ghxz { get; set; }
        /// <summary>
        /// phương thức thanh toán xjzffs
        /// </summary>
        public string? PayWay { get; set; }
        /// <summary>
        /// chi phí đã thanh toán
        /// </summary>
        public string? PayFee { get; set; }
        /// <summary>
        /// số thứ tự thanh toán
        /// </summary>
        public string? PayLsh { get; set; }

    }
    /// <summary>
    /// truy vấn hồ sơ đăng ký khám
    /// </summary>
    public class OutpRegistQueryRequest
    {
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }
}
