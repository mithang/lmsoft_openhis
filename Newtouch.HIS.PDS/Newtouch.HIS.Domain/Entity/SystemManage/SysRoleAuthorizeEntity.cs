using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_RoleAuthorize")]
    public class SysRoleAuthorizeEntity : IEntity<SysRoleAuthorizeEntity>
    {
        /// <summary>
        /// khóa chính ủy quyền vai trò
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// đối tượng khóa chính
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// hạng mục khóa chính
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// hạng mục loại 1 - mô-đun, 2 - nút, 3 - danh sách
        /// </summary>
        public int ItemType { get; set; }

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
