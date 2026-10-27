namespace NewtouchHIS.Lib.Base.Model
{
    public class JWTOptions
    {
        /// <summary>
        /// Đơn vị phát hành
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Đơn vị nhận
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Khóa bí mật
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Thời gian hết hạn
        /// </summary>
        public int ExpireHours { get; set; }
        /// <summary>
        /// Kiểm tra hết hạn
        /// </summary>
        public bool? ValidateLifetime { get; set; }
        public List<SSOAuthAppItem> SSOAuthApp { get; set; }
    }
}
