using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Cấu hình ủy quyền menu nhanh vai trò
    /// </summary>
    [Table("Sys_RoleShortcutMenu")]
    public class SysRoleShortcutMenuEntity : IEntity<SysRoleShortcutMenuEntity>
    {
        /// <summary>
        /// Ủy quyền vai trò menu nhanh Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Khóa chính vai trò
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// Menu nhanh
        /// </summary>
        public string SCMId { get; set; }

        /// <summary>
        /// Mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatorCode { get; set; }

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
        public string zt { get; set; }

        /// <summary>
        /// Cơ sở y tếId
        /// </summary>
        public string OrganizeId { get; set; }

    }
}
