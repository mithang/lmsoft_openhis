using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_Role")]
    public class SysRoleEntity : IEntity<SysRoleEntity>
    {
        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Id tổ chức (OrganizeId tương ứng với khoa)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Số hiệu
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người dùng tạo
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// Thời gian sửa đổi cuối cùng
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Người dùng sửa đổi cuối cùng
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
