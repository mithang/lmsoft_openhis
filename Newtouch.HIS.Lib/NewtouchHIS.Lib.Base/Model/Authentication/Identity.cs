namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// APIDanh tính người truy cập
    /// </summary>
    public abstract class Identity
    {
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// Loại mã thông báo (loại người dùng)
        /// </summary>
        public string? TokenType { get; set; }
        public string? Token { get; set; }

        /// <summary>
        /// người dùngId
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// Mã người dùng hệ thống (tài khoản đăng nhập)
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// Họ tên nhân viên hệ thống
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Tài khoản người dùng
        /// </summary>
        public string? Account { get; set; }
        public string? SignalConnId { get; set; }
    }
}
