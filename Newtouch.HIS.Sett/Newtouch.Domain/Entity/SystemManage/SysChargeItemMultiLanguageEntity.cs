using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfxm_dyy")]
    [Obsolete("please use the view")]
    public class SysChargeItemMultiLanguageEntity : IEntity<SysChargeItemMultiLanguageEntity>
    {
        /// <summary>
        /// Mã số đa ngôn ngữ của hạng mục thu phí
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int sfxmdyybh { get; set; }

        /// <summary>
        /// Hạng mục thu phí
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Tên hạng mục thu phí
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// Tên phồn thể của hạng mục thu phí
        /// </summary>
        public string sfxmmcFanti { get; set; }

        /// <summary>
        /// Tên tiếng Anh của hạng mục thu phí
        /// </summary>
        public string sfxmmcEnglish { get; set; }

        /// <summary>
        /// Tên tiếng Nhật của hạng mục thu phí
        /// </summary>
        public string sfxmmcJpan { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người lập hồ sơ
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Ngày lập hồ sơ
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người thay đổi
        /// </summary>
        public DateTime? LastModifierCode { get; set; }

        /// <summary>
        /// Ngày thay đổi
        /// </summary>
        public string LastModifyTime { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

    }
}
