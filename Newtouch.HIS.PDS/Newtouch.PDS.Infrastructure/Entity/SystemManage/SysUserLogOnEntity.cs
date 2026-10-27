using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_UserLogOn")]
    [SugarTable("Sys_UserLogOn")]
    [Obsolete("please use the view")]
    public class SysUserLogOnEntity : IEntity<SysUserLogOnEntity>
    {
        /// <summary>
        /// Khóa chính đăng nhập người dùng
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Khóa chính người dùng
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Mật khẩu người dùng
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// Khóa bí mật người dùng
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// Thời gian truy cập lần trước
        /// </summary>
        public DateTime? PreviousVisitTime { get; set; }

        /// <summary>
        /// Thời gian truy cập cuối
        /// </summary>
        public DateTime? LastVisitTime { get; set; }

        /// <summary>
        /// Số lần đăng nhập
        /// </summary>
        public int? LogOnCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
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
