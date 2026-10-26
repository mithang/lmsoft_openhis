using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_UserRole")]
    public class SysUserRoleEntity : IEntity<SysUserRoleEntity>
    {
        /// <summary>
        /// khóa chính ủy quyền vai trò
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// vai trò khóa chính
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// mã sắp xếp
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người tạo
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

    }
}
