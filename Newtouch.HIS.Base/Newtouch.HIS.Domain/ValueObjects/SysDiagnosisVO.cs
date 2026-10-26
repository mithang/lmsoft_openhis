using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chẩn đoán hệ thống
    /// </summary>
    [NotMapped]
    public class SysDiagnosisVO : SysDiagnosisEntity
    {
        /// <summary>
        /// Có cho phép chỉnh sửa hay không
        /// </summary>
        public bool AllowEdit { get; set; }
    }
}
