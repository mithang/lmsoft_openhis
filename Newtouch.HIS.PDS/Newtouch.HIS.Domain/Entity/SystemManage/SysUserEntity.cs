using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_User")]
    [Obsolete("please use the view")]
    public class SysUserEntity : IEntity<SysUserEntity>
    {
        /// <summary>
        /// người dùng khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// khóa chính tổ chức
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }

        /// <summary>
        /// tài khoản
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowEdit { get; set; }

        /// <summary>
        /// thời gian tạo
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// người tạo
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
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
