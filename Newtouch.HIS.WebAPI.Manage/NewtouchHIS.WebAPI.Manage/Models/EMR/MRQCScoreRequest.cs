namespace NewtouchHIS.WebAPI.Manage.Models.EMR
{
    public class MRQCScoreRequest
    {
        /// <summary>
        /// tổ chức
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// số nội trú
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// loại bệnh lý
        /// </summary>
        public string bllxId { get; set; }

    }
}
