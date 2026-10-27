namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    /// <summary>
    /// Đặt hẹnĐăng kýTham số yêu cầu
    /// </summary>
    public class BookApplyRequest
    {
        /// <summary>
        /// Tính chất đăng ký khám Tự chi trả, BHYT v.v. brxz Tương đương
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// mz_ghpb_schedule Lịch trình đã định của đặt hẹnId
        /// </summary>
        public string ScheduId { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
    }
    /// <summary>
    /// Tham số yêu cầu hủy đặt hẹn
    /// </summary>
    public class BookCancelRequest
    {
        /// <summary>
        /// Số đặt hẹn
        /// </summary>
        public string BookId { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// Nguyên nhân hủy đặt hẹn
        /// </summary>
        public string? CancelReason { get; set; }
        /// <summary>
        /// Phương thức liên hệ
        /// </summary>
        public string? lxdh { get; set; }
    }
}
