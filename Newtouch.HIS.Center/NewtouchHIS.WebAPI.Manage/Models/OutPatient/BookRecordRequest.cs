namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class BookRecordRequest
    {
        /// <summary>
        /// Số đặt hẹn
        /// </summary>
        public string? BookId { get; set; }

        /// <summary>
        /// Ngày khám theo hẹn(Ưu tiên cao hơnksrq|jsrq)
        /// </summary>
        public string? OutDate { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// Số CMND
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// Tên khoa
        /// </summary>
        public string? ksmc { get; set; }
        /// <summary>
        /// KhoaMã
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// Mã nhân viên bác sĩ
        /// </summary>
        public string? ysgh { get; set; }
        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        public string? ksrq { get; set; }
        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        public string? jsrq { get; set; }
        /// <summary>
        /// Trạng thái đặt hẹn
        /// </summary>
        public int? yyzt { get; set; }
    }
}
