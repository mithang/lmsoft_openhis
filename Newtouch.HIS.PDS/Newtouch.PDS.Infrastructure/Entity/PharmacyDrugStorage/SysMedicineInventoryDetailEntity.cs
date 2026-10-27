using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết thông tin kiểm kê thuốc hệ thống
    /// </summary>
    [Table("xt_yp_pdxxmx")]
    [SugarTable("xt_yp_pdxxmx")]
    public class SysMedicineInventoryDetailEntity : IEntity<SysMedicineInventoryDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string pdmxId { get; set; }

        /// <summary>
        /// Đơn kiểm kêID
        /// </summary>
        public string pdId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? Yxq { get; set; }

        /// <summary>
        /// Số lý thuyết  Đơn vị nhỏ nhất
        /// </summary>
        public int Llsl { get; set; }

        /// <summary>
        /// Số thực tế  Đơn vị nhỏ nhất
        /// </summary>
        public int Sjsl { get; set; }

        /// <summary>
        /// Giá bán buôn（Đơn vị bộ phận）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ（Đơn vị bộ phận）
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// Giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// Giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// Hệ số quy đổi
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

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

        /// <summary>
        /// Mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// Có chia lẻ không
        /// 1： Có
        /// 2： Không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
