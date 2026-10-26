using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Loại lớn thu phí bệnh án hệ thống
    /// </summary>
    [Table("xt_basfdl")]
    [Obsolete("please use the view")]
    public class SysMedicalRecordChargeCategoryEntity : IEntity<SysMedicalRecordChargeCategoryEntity>
    {
        /// <summary>
        /// Mã loại lớn
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dlbh { get; set; }

        /// <summary>
        /// Loại lớn(Phân loại trang chủ tương ứng)
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// Tên loại lớn
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// Pinyin
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Người tạoID
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuốiID
        /// </summary>
        public string LastModifierCode { get; set; }

    }
}
