using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// Thông tin thuốc mở rộng
    /// </summary>
    public class SysMedicineExVEntity: SysMedicineVEntity
    {
        /// <summary>
        /// Quy cách
        /// </summary>
        public string gg { get; set; }
    }
}