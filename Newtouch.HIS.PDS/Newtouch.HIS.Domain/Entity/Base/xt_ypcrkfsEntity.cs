using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain
{
    /// <summary>
    /// thuốc phương thức nhập/xuất kho
    /// </summary>
    [Table("xt_ypcrkfs")]
    public class XtypcrkfsEntity : IEntity<XtypcrkfsEntity>
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        [Key]
        public int crkfsId { get; set; }

        /// <summary>
        /// phương thức nhập/xuất kho mã
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// phương thức nhập/xuất kho tên
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// ra nhập kho ghi chú 0:nhập kho  1: xuất kho
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// trạng thái 1: còn hiệu lực  0: không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// tạo người
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi    
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
    }
}