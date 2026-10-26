namespace NewtouchHIS.WebAPI.Manage.Models.Patient
{
    /// <summary>
    /// thông tin cơ sở đăng ký bệnh nhân
    /// </summary>
    public class PatientRegisterBasic
    {
        /// <summary>
        /// số chứng minh nhân dân
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// họ tên
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// số thẻ bảo hiểm y tế
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// giới tính
        /// </summary>
        public string? xb { get; set; }
        /// <summary>
        /// thông tin liên hệ
        /// </summary>
        public string? lxdh { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? csrq { get; set; }
    }
    /// <summary>
    /// thông tin đăng ký bệnh nhân
    /// </summary>
    public class PatientRegisterDTO: PatientRegisterBasic
    {

    }
}
