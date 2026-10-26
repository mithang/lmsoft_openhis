using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.Herp.APIRequest
{
    /// <summary>
    /// Cơ sở y tế API Lớp cơ sở yêu cầu
    /// </summary>
    public class OrgJSONRequestBase : JSONRequestBase
    {
        /// <summary>
        /// Cơ sở y tế Id
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}