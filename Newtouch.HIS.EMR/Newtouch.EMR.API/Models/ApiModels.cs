namespace Newtouch.EMR.API.Models
{
    /// <summary>
    /// API caller identity (port of the original UserIdentity : Identity).
    /// Extra EMR fields: OrganizeId / TopOrganizeId.
    /// </summary>
    public class UserIdentity
    {
        public string AppId { get; set; }
        public string TokenType { get; set; }
        public string UserId { get; set; }
        public string Account { get; set; }
        public string Token { get; set; }
        public string OrganizeId { get; set; }
        public string TopOrganizeId { get; set; }
    }

    /// <summary>
    /// Non-generic API response envelope (port of the binary DefaultResponse).
    /// Codes reuse the vendor values established in the infra layer
    /// (APIRequestHelper.ResponseResultCode): SUCCESS = 10000, ERROR = 20000, FAIL = 40004.
    /// </summary>
    public class DefaultResponse
    {
        public int code { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }

    /// <summary>Marker attribute: skip token decryption/validation for this action (port of the binary IgnoreTokenDecrypt).</summary>
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class IgnoreTokenDecryptAttribute : System.Attribute
    {
    }
}
