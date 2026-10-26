using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysModuleButtonVO : SysModuleButtonEntity
    {
        /// <summary>
        /// Có đối với hay khôngOrgMở
        /// </summary>
        public bool IsOpenToOrg { get; set; }

    }
}
