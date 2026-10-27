using System.ComponentModel;

namespace NewtouchHIS.Lib.Base.Model
{
    public class JWTTokenRequest
    {
        public AuthType authType { get; set; }
        public AuthIdentity? authIdentity { get; set; }
    }
    /// <summary>
    /// Loại ủy quyền
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
        /// Thông tin ứng dụng truy cập
        /// </summary>
        public string? AppAuthKey { get; set; }
        /// <summary>
        /// AppId by Ứng dụng cấp token ủy quyền
        /// </summary>
        public string? KeyLicense { get; set; }
    }


    public class JWTToken
    {
        public string? Token { get; set; }
    }

    /// <summary>
    /// Xác thực danh tính
    /// </summary>
    public class SSOAuthIdentity : SSOAuthAppItem
    {
        /// <summary>
        /// Đơn vị phát hành
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Đơn vị nhận
        /// </summary>
        public string Audience { get; set; }
    }
    /// <summary>
    /// Thông tin ứng dụng ủy quyền
    /// </summary>
    public class SSOAuthAppItem
    {
        /// <summary>
        /// Tên miền hệ thống
        /// </summary>
        public string? Domain { get; set; }
        /// <summary>
        /// hệ thốngId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// Khóa bí mật hệ thống
        /// </summary>
        public string AppSecret { get; set; }
    }
}
