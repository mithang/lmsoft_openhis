using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysChargeItemVO : SysChargeItemEntity
    {
        /// <summary>
        /// Có cho phép sửa không
        /// </summary>
        public string sfdlmc { get; set; }
        /// <summary>
        /// Có đồng bộ BHYT không
        /// </summary>
        public string isSynch { get; set; }
        public string gjybmc { get; set; }
    }
}
