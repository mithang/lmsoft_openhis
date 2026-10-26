using Newtouch.HIS.API.Common;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class OrgRequestBase : RequestBase
    {
        /// <summary>
        /// ID tổ chức (đã định vị đến bệnh viện cụ thể)
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}
