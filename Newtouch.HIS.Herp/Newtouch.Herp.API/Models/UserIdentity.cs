using Newtouch.Herp.API.Common;

namespace Newtouch.Herp.API.Models
{
    /// <summary>
    /// Port of Newtouch.Herp.API.Models.UserIdentity: the shared Identity plus
    /// the Herp extension fields (medical-organisation ids).
    /// </summary>
    public class UserIdentity : Identity
    {
        /// <summary>Organisation id (original field name kept as an id).</summary>
        public string OrganizeId { get; set; }

        /// <summary>Top-level organisation id (original field name kept as an id).</summary>
        public string TopOrganizeId { get; set; }
    }
}
