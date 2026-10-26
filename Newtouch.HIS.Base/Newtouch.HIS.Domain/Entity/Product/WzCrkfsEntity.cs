using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    [Table("wz_crkfs")]
    public class WzCrkfsEntity : IEntity<WzCrkfsEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Tên phương thức nhập/xuất kho (bắt buộc)
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        ///  Cờ nhập/xuất kho     0: Nhập kho  1: Xuất kho
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// Trạng thái  0: Hủy bỏ; 1. Có hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối cùng
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
