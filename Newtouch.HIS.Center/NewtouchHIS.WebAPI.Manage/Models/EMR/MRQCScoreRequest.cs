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
        /// 病理类型
        /// </summary>
        public string bllxId { get; set; }

    }
}
