using System.ComponentModel.DataAnnotations;
using Newtouch.Herp.API.Common;

namespace Newtouch.Herp.API.Requests
{
    /// <summary>
    /// Port of Newtouch.Herp.APIRequest.AuthGetTokenRequest. NOTE: the original
    /// is an empty subclass of RequestBase — it carries NO UserId / Account /
    /// OrganizeId fields (unlike the Base.HOSP and PDS variants, which do).
    /// That is exactly why AuthController cannot verify anyone and must not
    /// mint a token here (see AuthController). Fields are NOT invented.
    /// </summary>
    /// <summary>
    /// Request cấp token (shape thống nhất giai đoạn GetToken chạy thật):
    /// Account + Password (dạng md5, đúng contract web login gửi lên BaseAPI
    /// CheckPwd) + TokenType lấy từ RequestBase. Identity lấy từ kết quả
    /// CheckPwd, không tin trường do client tự khai.
    /// </summary>
    public class AuthGetTokenRequest : RequestBase
    {
        public string Account { get; set; }

        /// <summary>Mật khẩu dạng md5 (như web login), KHÔNG BAO GIỜ log.</summary>
        public string Password { get; set; }
    }

    /// <summary>Port of Newtouch.Herp.APIRequest.DefaultQueryRequest.</summary>
    public class DefaultQueryRequest : JSONRequestBase
    {
        public string keyword { get; set; }
    }

    /// <summary>Port of Newtouch.Herp.APIRequest.OrgJSONRequestBase (not used by
    /// the Auth/Default controllers, but part of the original APIRequest set).</summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        [Required]
        public string OrganizeId { get; set; }
    }
}
