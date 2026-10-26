using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.MR.ManageSystem.APIRequest
{
    /// <summary>
    /// Lớp cơ sở yêu cầu API của cơ sở y tế
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