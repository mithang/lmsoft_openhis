using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nhóm thu phí lớn, loại nhóm lớn, bảng liên kết
    /// </summary>
    [Table("xt_sfdl_lx")]
    public class SysChargeCategoryTypeRelationEntity : IEntity<SysChargeCategoryTypeRelationEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Cơ sở y tế
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại, liên kết với mục từ điển ChargeCateType
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Mã nhóm thu phí lớn
        /// </summary>
        public string dlCode { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

       

    }
}
