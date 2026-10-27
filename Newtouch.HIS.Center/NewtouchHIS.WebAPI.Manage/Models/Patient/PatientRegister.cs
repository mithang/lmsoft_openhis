namespace NewtouchHIS.WebAPI.Manage.Models.Patient
{
    /// <summary>
    /// Bệnh nhânThông tin cơ sở đăng ký
    /// </summary>
    public class PatientRegisterBasic
    {
        /// <summary>
        /// Số CMND
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// BHYTSố thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public string? xb { get; set; }
        /// <summary>
        /// Phương thức liên hệ
        /// </summary>
        public string? lxdh { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? csrq { get; set; }
    }
    /// <summary>
    /// Bệnh nhânThông tin đăng ký
    /// </summary>
    public class PatientRegisterDTO: PatientRegisterBasic
    {

    }
}
