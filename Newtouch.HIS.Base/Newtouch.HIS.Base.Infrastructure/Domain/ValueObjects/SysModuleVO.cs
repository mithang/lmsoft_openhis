using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 
    /// </summary>
    [NotMapped]
    public class SysModuleVO : SysModuleEntity
    {
        /// <summary>
        /// Có đối vớiOrgMở
        /// </summary>
        public bool IsOpenToOrg { get; set; }

    }

}
