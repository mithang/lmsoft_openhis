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
    /// Request cấp token gốc — KHÔNG còn dùng để cấp token thật.
    /// Giữ lại để endpoint stub trả lỗi đúng dạng envelope.
    /// </summary>
    public class AuthGetTokenRequest : RequestBase
    {
    }

    /// <summary>Base request có OrganizeId bắt buộc (gốc: OrgJSONRequestBase).</summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        [Required]
        public string OrganizeId { get; set; }
    }
}
