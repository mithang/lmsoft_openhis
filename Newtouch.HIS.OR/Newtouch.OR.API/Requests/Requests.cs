using System.ComponentModel.DataAnnotations;
using Newtouch.OR.API.Envelope;

namespace Newtouch.OR.API.Requests
{
    /// <summary>DTO gốc Newtouch.OR.ManageSystem.APIRequest, port nguyên trường.</summary>
    public class DefaultQueryRequest : JSONRequestBase
    {
        public string keyword { get; set; }
    }

    public class QueryRequest : RequestBase
    {
        public string OrganizeId { get; set; }
        public string ksrq { get; set; }
        public string jsrq { get; set; }
        public string zyh { get; set; }
        /// <summary>1: Chờ đăng ký, 2: Đã đăng ký, 3: Đăng ký đã hủy.</summary>
        public string djzt { get; set; }
    }

    /// <summary>Đổi token lấy thông tin user (UA).</summary>
    public class UAGetUserInfoRequest : RequestBase
    {
        public string access_token { get; set; }
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
