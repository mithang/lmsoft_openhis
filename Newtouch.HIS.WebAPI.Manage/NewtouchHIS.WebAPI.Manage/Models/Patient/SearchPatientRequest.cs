namespace NewtouchHIS.WebAPI.Manage.Models.Patient
{
    public class SearchPatientRequest
    {
        /// <summary>
        /// số giấy tờ
        /// </summary>
        public string? zjh { get; set; }
        /// <summary>
        /// số thẻ
        /// </summary>
        public string? kh { get; set; }
        /// <summary>
        /// họ tên
        /// </summary>
        public string? xm { get; set; }
        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? csrq { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
        /// <summary>
        /// số nội trú
        /// </summary>
        public string? zyh { get; set; }
        /// <summary>
        /// loại nghiệp vụ EnumBusType
        /// </summary>
        public string? ywlx { get; set; }
    }

    public class SearchPatientResponse
    {
    }
}
