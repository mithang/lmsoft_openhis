namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// APIdanh tính người truy cập
    /// </summary>
    public abstract class Identity
    {
        /// <summary>
        /// ứng dụngId
        /// </summary>
        public string? AppId { get; set; }

        /// <summary>
        /// loại mã thông báo（loại người dùng）
        /// </summary>
        public string? TokenType { get; set; }
        public string? Token { get; set; }

        /// <summary>
        /// người dùngId
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// người dùng hệ thống Code（tài khoản đăng nhập）
        /// </summary>
        public string? UserCode { get; set; }

        /// <summary>
        /// nhân viên hệ thống họ tên
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// tài khoản người dùng
        /// </summary>
        public string? Account { get; set; }
        public string? SignalConnId { get; set; }
    }
}
