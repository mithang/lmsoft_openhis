using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Newtouch.MRMS.API.Envelope
{
    /// <summary>
    /// Tái tạo envelope của FrameworkBase.API.ApiControllerBase (chỉ có bản mirror source
    /// bên PDS + DLL Framework, không có source gốc). Mã số lấy theo họ ResponseResultCode
    /// của Lib.Base (SUCCESS=10000, ERROR=20000, FAIL=40004) — cùng họ mã BaseAPI dùng.
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

    /// <summary>ResponseBase của API.Common: code + sub_code + sub_msg + data.</summary>
    public class ResponseBase
    {
        public ResponseResultCode code { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public object data { get; set; }
    }

    public class DefaultResponse : ResponseBase
    {
    }

    /// <summary>
    /// RequestBase của API.Common (suy từ mirror PDS + cách CommonExecute dùng):
    /// Token/TokenType/Timestamp/AppId + Validate() kiểm tra [Required].
    /// </summary>
    public class RequestBase
    {
        public string Token { get; set; }
        public string TokenType { get; set; }
        public DateTime? Timestamp { get; set; }
        public string AppId { get; set; }

        /// <summary>
        /// Ném ArgumentsRequiredException khi có property [Required] bị trống,
        /// đúng hành vi Validate() gốc (CommonExecute map thành {FIELD}_IS_REQUIRED).
        /// </summary>
        public virtual void Validate()
        {
            foreach (var prop in GetType().GetProperties())
            {
                if (!Attribute.IsDefined(prop, typeof(RequiredAttribute))) continue;
                var val = prop.GetValue(this);
                if (val == null || (val is string s && string.IsNullOrWhiteSpace(s)))
                    throw new ArgumentsRequiredException(prop.Name);
            }
        }
    }

    public class JSONRequestBase : RequestBase
    {
        public string jsonpCallback { get; set; }
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
    /// (như IgnoreTokenDecrypt gốc; UA GetUserInfo là trường hợp duy nhất ở đây).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class IgnoreTokenDecryptAttribute : Attribute { }
}
