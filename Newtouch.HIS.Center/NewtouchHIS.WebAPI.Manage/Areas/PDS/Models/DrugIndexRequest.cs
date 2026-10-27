namespace NewtouchHIS.WebAPI.Manage.Areas.PDS.Models
{
    /// <summary>
    /// Chỉ mục truy vấn thuốc
    /// </summary>
    public class DrugIndexRequest
    {
        /// <summary>
        /// Nhóm lớn thuốc
        /// </summary>
        public string? dlcode { get; set; }
        /// <summary>
        /// ThuốcMã
        /// </summary>
        public string? ypcode { get; set; }
        /// <summary>
        /// Tên thuốc
        /// </summary>
        public string? ypmc { get; set; }
        /// <summary>
        /// Tổ chứcId
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// Bộ phận nhà thuốc
        /// </summary>
        public string yfbm { get; set; }

    }
}
