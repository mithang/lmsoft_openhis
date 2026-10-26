using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Items")]
    [Obsolete("please use the view")]
    public class SysItemsEntity : IEntity<SysItemsEntity>
    {
        /// <summary>
        /// chính bảng khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// cấp cha
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ngày tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// khóa chính người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
