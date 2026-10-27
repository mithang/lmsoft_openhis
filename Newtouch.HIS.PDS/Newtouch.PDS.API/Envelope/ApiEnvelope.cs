using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.PDS.API.Envelope
{
    /// <summary>
    /// Tái tạo envelope của FrameworkBase.API.ApiControllerBase theo bản mirror source
    /// trong chính cây PDS (Newtouch.PDS.API/App_Start/ApiControllerBase.cs) — cùng họ mã
    /// với các API anh em đã port (OR/MRMS/CIS): SUCCESS=10000, ERROR=20000, FAIL=40004.
    ///
    /// KHÁC BIỆT SO VỚI GỐC (ghi rõ, không giấu):
    /// - ResponseBase có thêm trường msg: StockController gốc gán resp.msg (PrepareMedicine,
    ///   WithdrawalPreparation...) nên envelope gốc của PDS chắc chắn có msg; các API anh em
    ///   không dùng tới nên không khai báo. Newtonsoft mặc định serialize cả null nên mọi
    ///   response PDS đều có "msg": null khi action không gán — đúng hành vi formatter gốc.
    /// - RequestBase ở đây là bản shim đã port trong PDS.Infrastructure (không phải bản
    ///   MRMS): Timestamp là DateTime KHÔNG nullable (JSON thiếu Timestamp -> MinValue),
    ///   không có Validate() và không có jsonpCallback. ApiControllerBase bên dưới xử lý
    ///   tương ứng (xem ghi chú ở đó).
    /// </summary>
    public enum ResponseResultCode
    {
        Default = -1,
        SUCCESS = 10000,
        ERROR = 20000,
        INVALIDTOKEN = 40000,
        FAILOfAuth = 40001,
        FAILOfRequestRepeat = 40002,
        FAIL = 40004,
        FAILOfConfigInit = 400040,
        FAILOfPwdError = 400041,
        FAILOfEmpty = 410001,
        FAILOfExists = 410002,
    }

    /// <summary>ResponseBase của API.Common: code + sub_code + sub_msg + data (+ msg của PDS).</summary>
    public class ResponseBase
    {
        public ResponseResultCode code { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public object data { get; set; }
        public string msg { get; set; }
    }

    public class DefaultResponse : ResponseBase
    {
    }

    /// <summary>Identity tối thiểu mà token Redis lưu (theo UserIdentity gốc).</summary>
    public class Identity
    {
        public string AppId { get; set; }
        public string TokenType { get; set; }
        public string UserId { get; set; }
        public string Account { get; set; }
        public string UserCode { get; set; }
        public string OrganizeId { get; set; }
        public string TopOrganizeId { get; set; }
    }

    public class TokenTypeRequiredException : Exception { }
    public class TokenRequiredException : Exception { }
    public class TokenExpiredException : Exception { }
    public class TokenTypeNotMatchException : Exception { }
    public class TokenIllegalException : Exception { }

    public class ArgumentsRequiredException : Exception
    {
        public string Fields { get; }
        public ArgumentsRequiredException(string fields) { Fields = fields; }
    }

    public class FailedException : Exception
    {
        public string Code { get; }
        public string Msg { get; }
        public FailedException(string msg, string code = null) : base(msg) { Msg = msg; Code = code; }
    }

    /// <summary>Đánh dấu controller/action bắt buộc phải có identity (như DefaultAuthorizeAttribute gốc).</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class DefaultAuthorizeAttribute : Attribute { }

    /// <summary>
    /// Đánh dấu action dùng token thô, bỏ qua bước giải mã token toàn cục
    /// (như IgnoreTokenDecrypt gốc). Ở bản port này toàn bộ token đều dùng thô
    /// (xem ghi chú TokenStore về TokenDecryptFilter) nên attribute chỉ còn ý nghĩa
    /// ghi chú trung thực với gốc — không làm thay đổi hành vi.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IgnoreTokenDecryptAttribute : Attribute { }
}
