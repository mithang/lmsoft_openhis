using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Nhóm thu phí lớn Loại danh mục lớn Bảng liên kết
    /// </summary>
    [SugarTable("xt_sfdl_lx")]
    public class SysChargeCategoryTypeRelationEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Cơ sở y tế
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Loại Liên kết từ điểnChargeCateTypemục từ điển của
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
