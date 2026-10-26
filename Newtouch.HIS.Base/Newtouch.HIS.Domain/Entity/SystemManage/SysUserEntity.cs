using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Người dùng hệ thống (đăng nhập)
    /// </summary>
    [Table("Sys_User")]
    public class SysUserEntity : IEntity<SysUserEntity>
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Id tổ chức (cấp cao nhất, cấu hình hệ thống)
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Tài khoản đăng nhập
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Loại ngôn ngữ: en tiếng Anh
        /// </summary>
        public string LanguageType { get; set; }

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
