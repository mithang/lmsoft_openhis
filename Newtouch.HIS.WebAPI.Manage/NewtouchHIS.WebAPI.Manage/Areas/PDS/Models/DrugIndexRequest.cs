namespace NewtouchHIS.WebAPI.Manage.Areas.PDS.Models
{
    /// <summary>
    /// chỉ mục truy vấn thuốc
    /// </summary>
    public class DrugIndexRequest
    {
        /// <summary>
        /// nhóm lớn thuốc
        /// </summary>
        public string? dlcode { get; set; }
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string? ypcode { get; set; }
        /// <summary>
        /// tên thuốc
        /// </summary>
        public string? ypmc { get; set; }
        /// <summary>
        /// cơ quanId
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// khoa dược
        /// </summary>
        public string yfbm { get; set; }

    }
}
