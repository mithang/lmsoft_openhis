using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Duty")]
    [Obsolete("此模块暂不需要")]
    public class DutyEntity : IEntity<DutyEntity>
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Khóa chính tổ chức
        /// </summary>
        public string F_OrganizeId { get; set; }

        /// <summary>
        /// Số hiệu
        /// </summary>
        public string F_EnCode { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string F_FullName { get; set; }

        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string F_Description { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi lần cuối
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người sửa đổi lần cuối
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
