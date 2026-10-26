using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Khoa
    /// </summary>
    [Table("xt_ks")]
    [Obsolete("please use the view")]
    public class SysDepartmentEntity : IEntity<SysDepartmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ksbh { get; set; }

        /// <summary>
        /// Khoa ngoại trú/nội trú gồm các chữ số,Các khoa hành chính khác bắt đầu bằng chữ cái
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 0 Dùng chung 1 Chỉ ngoại trú 2 Chỉ nội trú 3 Ngoại trú và nội trú đều không khả dụng(Khoa hành chính)
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 0 Khoa không phải kỹ thuật y tế 1 Khoa kỹ thuật y tế 
        /// </summary>
        public string yjbz { get; set; }

        /// <summary>
        /// 0 Không khả dụng 1 Khả dụng
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 0.Chưa kiểm soát 1.Kiểm soát
        /// </summary>
        public byte? xbkz { get; set; }

        /// <summary>
        /// 0.Chưa thiết lập 1.Khoa nhi
        /// </summary>
        public byte? tsksdy { get; set; }

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
