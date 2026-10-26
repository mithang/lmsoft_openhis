using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc điều chỉnh giá
    /// </summary>
    [Table("xt_yptj")]
    public class SysMedicinePriceAdjustmentEntity : IEntity<SysMedicinePriceAdjustmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string yptjId { get; set; }

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
        public string ypCode { get; set; }

        /// <summary>
        /// giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal pfj { get; set; }

        /// <summary>
        /// giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// giá bán buôn gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ypfj { get; set; }

        /// <summary>
        /// giá bán lẻ gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ylsj { get; set; }

        /// <summary>
        /// duyệt cờ 0:chưa duyệt 1:đã duyệt 2:đã từ chối 3.đã thu hồi
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// sửa loại
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// điều chỉnh thời gian
        /// </summary>
        public DateTime tzsj { get; set; }

        /// <summary>
        /// điều chỉnh tệp
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// thời gian thực hiện
        /// </summary>
        public DateTime zxsj { get; set; }

        /// <summary>
        /// điều chỉnh người thao tác
        /// </summary>
        public string tzczy { get; set; }

        /// <summary>
        /// Người thao tác duyệt
        /// </summary>
        public string shczy { get; set; }

        /// <summary>
        /// thực hiện người thao tác
        /// </summary>
        public string zxczy { get; set; }

        /// <summary>
        /// cờ thực hiện    0:chưa thực hiện 1:đã thực hiện
        /// </summary>
        public string zxbz { get; set; }

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
