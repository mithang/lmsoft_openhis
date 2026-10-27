using System.ComponentModel.DataAnnotations;
using Newtouch.MRMS.API.Envelope;

namespace Newtouch.MRMS.API.Requests
{
    /// <summary>DTO gốc Newtouch.MR.ManageSystem.APIRequest, port nguyên trường.</summary>
    public class DefaultQueryRequest : JSONRequestBase
    {
        public string keyword { get; set; }
    }

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

    /// <summary>Base request có OrganizeId bắt buộc (gốc: OrgJSONRequestBase).</summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        [Required]
        public string OrganizeId { get; set; }
    }
}
