using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc kiểm kê thông tin chi tiết
    /// </summary>
    [Table("xt_yp_pdxxmx")]
    public class SysMedicineInventoryDetailEntity : IEntity<SysMedicineInventoryDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string pdmxId { get; set; }

        /// <summary>
        /// phiếu kiểm kê ID
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
        /// số lượng lý thuyết  đơn vị nhỏ nhất
        /// </summary>
        public int Llsl { get; set; }

        /// <summary>
        /// số thực tế  đơn vị nhỏ nhất
        /// </summary>
        public int Sjsl { get; set; }

        /// <summary>
        /// giá bán buôn (đơn vị bộ phận)
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Pfj { get; set; }

        /// <summary>
        /// giá bán lẻ (đơn vị bộ phận)
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Lsj { get; set; }

        /// <summary>
        /// giá bán buôn kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Ykpfj { get; set; }

        /// <summary>
        /// giá bán lẻ kho thuốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal Yklsj { get; set; }

        /// <summary>
        /// hệ số chuyển đổi
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
        /// mã truy xuất
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// có tách lẻ không
        /// 1: có
        /// 2: không
        /// </summary>
        public int? sfcl { get; set; }

    }
}
