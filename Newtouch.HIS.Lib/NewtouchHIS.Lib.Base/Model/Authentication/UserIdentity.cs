namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// danh tính người dùng（theo nhu cầu dự án trường mở rộng danh tính người dùng）
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// tổ chứcId（bệnh viện）
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất
        /// </summary>
        public string? TopOrganizeId { get; set; }

    }

}
