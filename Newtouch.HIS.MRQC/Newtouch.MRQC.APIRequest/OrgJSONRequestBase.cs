using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.MRQC.APIRequest
{
    /// <summary>
    /// cơ sở y tếAPI lớp cơ sở của yêu cầu
    /// </summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        /// <summary>
        /// cơ sở y tếId
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}