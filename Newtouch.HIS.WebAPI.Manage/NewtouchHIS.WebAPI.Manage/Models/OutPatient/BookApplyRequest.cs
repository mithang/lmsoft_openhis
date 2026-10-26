namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    /// <summary>
    /// tham số yêu cầu đăng ký hẹn
    /// </summary>
    public class BookApplyRequest
    {
        /// <summary>
        /// tính chất đăng ký khám tự trả, bảo hiểm y tế, v.v. brxz tương đương
        /// </summary>
        public string ghxz { get; set; }
        /// <summary>
        /// mz_ghpb_schedule lịch hẹn đã địnhId
        /// </summary>
        public string ScheduId { get; set; }
        /// <summary>
        /// số thẻ
        /// </summary>
        public string kh { get; set; }
    }
    /// <summary>
    /// tham số yêu cầu hủy hẹn
    /// </summary>
    public class BookCancelRequest
    {
        /// <summary>
        /// số hẹn
        /// </summary>
        public string BookId { get; set; }
        /// <summary>
        /// số thẻ
        /// </summary>
        public string kh { get; set; }
        /// <summary>
        /// nguyên nhân hủy hẹn
        /// </summary>
        public string? CancelReason { get; set; }
        /// <summary>
        /// thông tin liên hệ
        /// </summary>
        public string? lxdh { get; set; }
    }
}
