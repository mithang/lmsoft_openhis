using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Newtouch.Herp.API.Common
{
    /// <summary>
    /// Port of Newtouch.HIS.API.Common.ResponseResultCode (binary-only vendor
    /// assembly). Numeric values were read from the original DLL metadata so
    /// the wire format matches the Framework host exactly:
    /// SUCCESS = 10000, ERROR = 20000, FAIL = 40004.
    /// </summary>
    public enum ResponseResultCode
    {
        SUCCESS = 10000,
        ERROR = 20000,
        FAIL = 40004
    }

    /// <summary>Port of Newtouch.HIS.API.Common response base (property names are
    /// lowercase in the original and are kept lowercase on the wire).</summary>
    public class ResponseBase
    {
        public ResponseResultCode code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public object data { get; set; }
    }

    /// <summary>Default (non-generic) API response, as used by Auth/Default.</summary>
    public class DefaultResponse : ResponseBase
    {
    }

    /// <summary>
    /// Port of Newtouch.HIS.API.Common.RequestBase. Property names come from the
    /// original DLL metadata: AppId, TokenType, Token, Timestamp, ResponseColumns.
    /// Validate() mirrors the original behaviour relied on by ApiControllerBase:
    /// a [Required] property that is null/blank throws ArgumentsRequiredException.
    /// </summary>
    public class RequestBase
    {
        public string AppId { get; set; }
        public string TokenType { get; set; }
        public string Token { get; set; }
        public DateTime? Timestamp { get; set; }
        public string ResponseColumns { get; set; }

        public virtual void Validate()
        {
            foreach (var prop in GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.GetCustomAttribute<RequiredAttribute>() == null) continue;
                var value = prop.GetValue(this);
                if (value == null || (value is string s && string.IsNullOrWhiteSpace(s)))
                {
                    throw new ArgumentsRequiredException { Fields = prop.Name };
                }
            }
        }
    }

    /// <summary>Port of Newtouch.HIS.API.Common.JSONRequestBase.</summary>
    public class JSONRequestBase : RequestBase
    {
        public string jsonpCallback { get; set; }
    }

    /// <summary>
    /// Port of Newtouch.HIS.API.Common.Models.Identity (property names from the
    /// original DLL metadata). Herp's UserIdentity adds OrganizeId/TopOrganizeId.
    /// </summary>
    public class Identity
    {
        public string AppId { get; set; }
        public string TokenType { get; set; }
        public string UserId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
    }

    /// <summary>Marker mirroring Newtouch.HIS.API.Common.Attributes.DefaultAuthorizeAttribute:
    /// an endpoint carrying it requires a valid token. Neither Herp API controller
    /// applies it in the original, so unauthenticated calls reach the action and
    /// only a supplied-but-invalid token is rejected (see ApiControllerBase).</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class DefaultAuthorizeAttribute : Attribute
    {
    }

    // --- Token / argument exceptions (names + handling mirror the PDS source
    // mirror of FrameworkBase.API.ApiControllerBase; messages translated below
    // in ApiControllerBase.CommonExecute) ---

    public class TokenTypeRequiredException : Exception { }
    public class TokenRequiredException : Exception { }
    public class TokenExpiredException : Exception { }
    public class TokenTypeNotMatchException : Exception { }
    public class TokenIllegalException : Exception { }

    public class ArgumentsRequiredException : Exception
    {
        public string Fields { get; set; }
    }

    public class FailedException : Exception
    {
        public string Code { get; set; }
        public string Msg { get; set; }
    }

    /// <summary>Resolves a token to an identity (originally Redis-backed).</summary>
    public interface IUserIdentityResolver<TIdentity> where TIdentity : Identity
    {
        TIdentity GetIdentity(string token);
        TIdentity GetIdentity(string token, string tokenType);
    }
}
