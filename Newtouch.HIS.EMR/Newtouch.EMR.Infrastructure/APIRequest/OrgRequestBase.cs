using System.ComponentModel.DataAnnotations;
using Newtouch.HIS.API.Common;

namespace Newtouch.EMR.APIRequest
{
    public class OrgRequestBase : JSONRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public new string OrganizeId { get; set; }
    }
}
