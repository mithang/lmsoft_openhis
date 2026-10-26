using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysOrganizeVO : SysOrganizeEntity
    {
        /// <summary>
        /// Có cho phép chỉnh sửa hay không
        /// </summary>
        public bool AllowEdit { get; set; }
    }
}
