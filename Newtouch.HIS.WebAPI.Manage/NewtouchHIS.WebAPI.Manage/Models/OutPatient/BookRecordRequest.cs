namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class BookRecordRequest
    {
        /// <summary>
        /// số hẹn
        /// </summary>
        public string? BookId { get; set; }

        /// <summary>
        /// ngày khám theo hẹn（ưu tiên cao hơnksrq|jsrq）
        /// </summary>
        public string? OutDate { get; set; }
        /// <summary>
        /// số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// họ tên bệnh nhân
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// số chứng minh nhân dân
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// tên khoa
        /// </summary>
        public string? ksmc { get; set; }
        /// <summary>
        /// mã khoa
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// số hiệu bác sĩ
        /// </summary>
        public string? ysgh { get; set; }
        /// <summary>
        /// ngày bắt đầu
        /// </summary>
        public string? ksrq { get; set; }
        /// <summary>
        /// ngày kết thúc
        /// </summary>
        public string? jsrq { get; set; }
        /// <summary>
        /// trạng thái hẹn
        /// </summary>
        public int? yyzt { get; set; }
    }
}
