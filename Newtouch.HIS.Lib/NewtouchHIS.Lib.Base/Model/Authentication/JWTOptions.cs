namespace NewtouchHIS.Lib.Base.Model
{
    public class JWTOptions
    {
        /// <summary>
        /// bên phát hành
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// người nhận
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// khóa bí mật
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// thời gian hết hạn
        /// </summary>
        public int ExpireHours { get; set; }
        /// <summary>
        /// xác minh hết hạn
        /// </summary>
        public bool? ValidateLifetime { get; set; }
        public List<SSOAuthAppItem> SSOAuthApp { get; set; }
    }
}
