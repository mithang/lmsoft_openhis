using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// Phương thức xuất nhập kho thuốc
    /// </summary>
    [Table("xt_ypcrkfs")]
    [SugarTable("xt_ypcrkfs")]
    public class XtypcrkfsEntity : IEntity<XtypcrkfsEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public int crkfsId { get; set; }

        /// <summary>
        /// Mã phương thức xuất nhập kho
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// Tên phương thức xuất nhập kho
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// Ghi chú xuất nhập kho 0:Nhập kho  1:Xuất kho
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// Trạng thái 1:Hiệu lực  0:Không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// Sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// đang sửa
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi    
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}