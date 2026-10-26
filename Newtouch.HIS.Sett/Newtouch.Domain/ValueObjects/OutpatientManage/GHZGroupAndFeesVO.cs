using FrameworkBase.MultiOrg.Domain.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi phí đăng ký khám
    /// </summary>
    [NotMapped]
    public class GHZGroupAndFeesVO
    {
        /// <summary>
        /// Phí đăng ký khám
        /// </summary>
        public decimal ghfPrice { get; set; }

        /// <summary>
        /// Phí khám chữa bệnh
        /// </summary>
        public decimal zlfPrice { get; set; }

        /// <summary>
        /// Phí thẻ từ
        /// </summary>
        public decimal ckfPrice { get; set; }

        /// <summary>
        /// Phí làm thẻ
        /// </summary>
        public decimal gbfPrice { get; set; }

        /// <summary>
        /// Tổng số tiền
        /// </summary>
        public decimal totalfees { get; set; }

        /// <summary>
        /// Của hạng mục liên kếtEntity
        /// </summary>
        public Dictionary<string, SysChargeItemVEntity> sfxmList { get; set; }
        public Dictionary<string, List<SysChargeItemVEntity>> sfxmzhList { get; set; }
        /// <summary>
        /// Lịch trựcId
        /// </summary>
        public decimal? ScheduId { get; set; }
    }
}
