namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// cấu hình truy cập
    /// </summary>
    public class AccessConfig
    {
        /// <summary>
        /// trangdomain（lồng trang miền khác，và thực hiệnjs cần đặt thành cùng một miền）
        /// </summary>
        public string? Document_Domain { get; set; }
        /// <summary>
        /// giới hạn tên miền địa chỉ truy cập （tương tự Baiduhttpsẽ tự động chuyển hướng đếnhttps）
        /// </summary>
        public string? AccessDomain { get; set; }
    }
}
