using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết đơn hàng nội trú--2023-9-19 chl
    /// </summary>
    [Table("order_zy")]
    public class OrderZyEntity : IEntity<OrderZyEntity>
    {
        [Key]
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// Số đơn hàng
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// Loại lớn thu phí
        /// </summary>
        public string dlcode { get; set; }
        /// <summary>
        /// Tên loại lớn thu phí
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// Hạng mục thu phí đơn thuốc
        /// </summary>
        public string sfxm { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal? dj { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal? sl { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>
        public decimal? je { get; set; }
        /// <summary>
        /// Đơn vị tính phí
        /// </summary>
        public string jfdw { get; set; }
        /// <summary>
        /// Tính chất tự chi trả
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// Dấu chuyển sang tự chi trả
        /// </summary>
        public string zzfbz { get; set; }
        /// <summary>
        /// Chi tiết hạng mục Tạm thời không dùng
        /// </summary>
        public string sfxmmx { get; set; }
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

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
    }
}
