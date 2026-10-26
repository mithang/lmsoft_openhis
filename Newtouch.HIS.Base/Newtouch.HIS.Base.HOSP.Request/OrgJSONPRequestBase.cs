using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Base.HOSP.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class OrgJSONPRequestBase : JSONRequestBase
    {
        /// <summary>
        /// Id tổ chức (đã xác định đến bệnh viện cụ thể)
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}
