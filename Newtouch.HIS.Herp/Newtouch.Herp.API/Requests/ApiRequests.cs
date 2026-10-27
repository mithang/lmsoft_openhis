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
    public class AuthGetTokenRequest : RequestBase
    {
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
