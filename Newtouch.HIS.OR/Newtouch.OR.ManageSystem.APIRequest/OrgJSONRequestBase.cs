using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.OR.ManageSystem.APIRequest
{
    /// <summary>
    /// Cơ sở y tếAPILớp cơ sở của yêu cầu
    /// </summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        /// <summary>
        /// Cơ sở y tếId
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}