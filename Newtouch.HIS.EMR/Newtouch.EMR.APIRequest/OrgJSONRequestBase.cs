using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.EMR.APIRequest
{
    /// <summary>
    /// Lớp cơ sở yêu cầu API của cơ sở y tế
    /// </summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        /// <summary>
        /// Id cơ sở y tế
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}