using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Phương thức nhập/xuất kho
    /// </summary>
    [SugarTable("wz_crkfs")]
    public class WzCrkfsEntity : EntityBase
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Tên phương thức nhập/xuất(Bắt buộc nhập)
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        ///  Cờ xuất/nhập kho     0:Nhập kho  1:Xuất kho
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// Trạng thái  0:Hủy bỏ;1.Hiệu lực
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
        /// Thời gian sửa cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}
