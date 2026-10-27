using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.Infrastructure.EF.Attributes;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Điều chỉnh giá thuốc hệ thống
    /// </summary>
    [Table("xt_yptj")]
    [SugarTable("xt_yptj")]
    public class SysMedicinePriceAdjustmentEntity : IEntity<SysMedicinePriceAdjustmentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
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
        /// Giá bán buôn
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal pfj { get; set; }

        /// <summary>
        /// Giá bán lẻ
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal lsj { get; set; }

        /// <summary>
        /// Giá bán buôn gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ypfj { get; set; }

        /// <summary>
        /// Giá bán lẻ gốc
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal ylsj { get; set; }

        /// <summary>
        /// Cờ duyệt 0:Chưa duyệt 1:Đã duyệt 2:Đã từ chối 3.Đã thu hồi
        /// </summary>
        public string shzt { get; set; }

        /// <summary>
        /// Loại sửa đổi
        /// </summary>
        public string xglx { get; set; }

        /// <summary>
        /// Thời gian điều chỉnh
        /// </summary>
        public DateTime tzsj { get; set; }

        /// <summary>
        /// Tệp điều chỉnh
        /// </summary>
        public string tzwj { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public DateTime zxsj { get; set; }

        /// <summary>
        /// Người thao tác điều chỉnh
        /// </summary>
        public string tzczy { get; set; }

        /// <summary>
        /// Người thao tác xét
        /// </summary>
        public string shczy { get; set; }

        /// <summary>
        /// Người thao tác thực hiện
        /// </summary>
        public string zxczy { get; set; }

        /// <summary>
        /// Cờ thực hiện    0:Chưa thực hiện 1:Đã thực hiện
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
