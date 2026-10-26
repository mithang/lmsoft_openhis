using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_daysettlemx")]
    public class DaySettleMxEntity : IEntity<DaySettleMxEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zffs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? je { get; set; }

        /// <summary>
        /// Tính chất y lệnh 1Tạm thời 2Dài hạn
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Loại y lệnh 1Thuốc 2Hạng mục điều trị
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
