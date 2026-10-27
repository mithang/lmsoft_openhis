namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// Danh tính người dùng (mở rộng trường danh tính người dùng theo nhu cầu dự án)
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>
        /// Id tổ chức (bệnh viện)
        /// </summary>
        public string? OrganizeId { get; set; }

        /// <summary>
        /// Tổ chức cấp cao nhất
        /// </summary>
        public string? TopOrganizeId { get; set; }

    }

}
