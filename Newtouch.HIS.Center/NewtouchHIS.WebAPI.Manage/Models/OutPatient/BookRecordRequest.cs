namespace NewtouchHIS.WebAPI.Manage.Models.OutPatient
{
    public class BookRecordRequest
    {
        /// <summary>
        /// 预约号
        /// </summary>
        public string? BookId { get; set; }

        /// <summary>
        /// 预约就诊日期（优先级高于ksrq|jsrq）
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
        /// 身份证号
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string? ksmc { get; set; }
        /// <summary>
        /// 科室Mã
        /// </summary>
        public string? ks { get; set; }
        /// <summary>
        /// 医生工号
        /// </summary>
        public string? ysgh { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string? ksrq { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string? jsrq { get; set; }
        /// <summary>
        /// 预约状态
        /// </summary>
        public int? yyzt { get; set; }
    }
}
