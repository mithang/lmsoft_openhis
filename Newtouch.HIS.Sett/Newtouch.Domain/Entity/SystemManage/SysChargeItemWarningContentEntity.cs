using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nội dung cảnh báo hạng mục thu phí
    /// </summary>
    [Table("xt_sfxmjsnr")]
    [Obsolete]

    public class SysChargeItemWarningContentEntity : IEntity<SysChargeItemWarningContentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sfxjsnrbh { get; set; }

        public string sfxm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzjsnr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zyjsnr { get; set; }

        /// <summary>
        /// 0 Không nhắc 1 Nhắc nhở thông thường 2 Cảnh báo 3 Cấm sử dụng
        /// </summary>
        public byte mzjsjb { get; set; }

        /// <summary>
        /// 0 Không nhắc 1 Nhắc nhở thông thường 2 Cảnh báo 3 Cấm sử dụng
        /// </summary>
        public byte zyjsjb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
