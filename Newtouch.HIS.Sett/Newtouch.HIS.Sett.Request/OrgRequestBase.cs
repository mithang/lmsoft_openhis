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
        /// ID tổ chức(Đ ã定 vị đ ến 具体 bệnh viện)
        /// </summary>
        [Required]
        public string OrganizeId { get; set; }

    }
}
