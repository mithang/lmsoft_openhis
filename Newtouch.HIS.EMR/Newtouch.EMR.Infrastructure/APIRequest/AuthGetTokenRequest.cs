
using Newtouch.HIS.API.Common;
namespace Newtouch.EMR.APIRequest
{
    /// <summary>
    /// Bằng thông tin quan trọng（Tên đăng nhập、mật khẩu, v.v.）Lấy token truy cập
    /// </summary>
    public class AuthGetTokenRequest : RequestBase
    {
        public string Account { get; set; }

        /// <summary>Mật khẩu dạng md5 (như web login), KHÔNG BAO GIỜ log.</summary>
        public string Password { get; set; }

        /// <summary>
        /// Loại token (RequestBase của EMR không có TokenType nên khai ở đây
        /// để thống nhất shape với các API khác).
        /// </summary>
        public string TokenType { get; set; }
    }
}
