namespace NewtouchHIS.WebAPI.Manage.Models.EMR
{
    public class MRQCScoreRequest
    {
        /// <summary>
        /// Tổ chức
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Loại bệnh lý
        /// </summary>
        public string bllxId { get; set; }

    }
}
