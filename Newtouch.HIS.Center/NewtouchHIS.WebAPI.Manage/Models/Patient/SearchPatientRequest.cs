namespace NewtouchHIS.WebAPI.Manage.Models.Patient
{
    public class SearchPatientRequest
    {
        /// <summary>
        /// 证件号
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// Số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? csrq { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// Loại nghiệp vụ EnumBusType
        /// </summary>
        public string? ywlx { get; set; }
    }

    public class SearchPatientResponse
    {
    }
}
