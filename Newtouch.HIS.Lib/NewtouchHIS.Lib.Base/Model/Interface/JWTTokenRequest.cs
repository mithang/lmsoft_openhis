using System.ComponentModel;

namespace NewtouchHIS.Lib.Base.Model
{
    public class JWTTokenRequest
    {
        public AuthType authType { get; set; }
        public AuthIdentity? authIdentity { get; set; }
    }
    /// <summary>
    /// loại ủy quyền
    /// </summary>
    public enum AuthType
    {
        [Description("Web")]
        Web = 1,
        [Description("WebAPI")]
        WebApi = 2
    }

    public class AuthIdentity : UserIdentity
    {
        /// <summary>
        /// thông tin ứng dụng được truy cập
        /// </summary>
        public string? AppAuthKey { get; set; }
        /// <summary>
        /// AppId by cấp ủy quyềntokenứng dụng
        /// </summary>
        public string? KeyLicense { get; set; }
    }


    public class JWTToken
    {
        public string? Token { get; set; }
    }

    /// <summary>
    /// chứng thực danh tính xác thực
    /// </summary>
    public class SSOAuthIdentity : SSOAuthAppItem
    {
        /// <summary>
        /// bên phát hành
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// người nhận
        /// </summary>
        public string Audience { get; set; }
    }
    /// <summary>
    /// thông tin ứng dụng được ủy quyền
    /// </summary>
    public class SSOAuthAppItem
    {
        /// <summary>
        /// tên miền hệ thống
        /// </summary>
        public string? Domain { get; set; }
        /// <summary>
        /// hệ thốngId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// khóa hệ thống
        /// </summary>
        public string AppSecret { get; set; }
    }
}
