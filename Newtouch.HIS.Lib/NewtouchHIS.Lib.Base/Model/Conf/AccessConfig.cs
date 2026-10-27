namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// Cấu hình truy cập
    /// </summary>
    public class AccessConfig
    {
        /// <summary>
        /// Domain trang (nhúng trang của miền khác, và khi thực thi js cần đặt thành cùng một miền)
        /// </summary>
        public string? Document_Domain { get; set; }
        /// <summary>
        /// Giới hạn tên miền địa chỉ truy cập (tương tự như http của Baidu sẽ tự động chuyển hướng đến https)
        /// </summary>
        public string? AccessDomain { get; set; }
    }
}
