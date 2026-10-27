using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chứng từ thuốc hệ thống
    /// </summary>
    [Table("xt_yp_dj")]
    [SugarTable("xt_yp_dj")]
    public class SysMedicineReceiptEntity : IEntity<SysMedicineReceiptEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string ypdjId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djqz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int djh { get; set; }

        /// <summary>
        /// 0:Cho phép 1:Không cho phép -1:Chưa chọn
        /// </summary>
        public int xgdjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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

    }
}
