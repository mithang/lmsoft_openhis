using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Loại lớn phụ thêm thu phí hệ thống(17Tầng)
    /// </summary>
    [Table("xt_fjsfdl")]
    public class SysChargeAdditionalCategoryEntity : IEntity<SysChargeAdditionalCategoryEntity>
    {
        /// <summary>
        /// Mã loại lớn
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int dlbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại lớn
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
        /// Cờ ngoại trú/nội trú(0 Dùng chung 1 Khám ngoại trú 2Nhập viện)
        /// </summary>
        public string mzzybz { get; set; }

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
