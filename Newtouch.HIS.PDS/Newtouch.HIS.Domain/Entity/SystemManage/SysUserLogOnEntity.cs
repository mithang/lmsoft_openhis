using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_UserLogOn")]
    [Obsolete("please use the view")]
    public class SysUserLogOnEntity : IEntity<SysUserLogOnEntity>
    {
        /// <summary>
        /// người dùng đăng nhập khóa chính
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// người dùng khóa chính
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// người dùng mật khẩu
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 用户秘钥
        /// </summary>
        public string UserSecretkey { get; set; }

        /// <summary>
        /// trên một lần truy cập thời gian
        /// </summary>
        public DateTime? PreviousVisitTime { get; set; }

        /// <summary>
        /// cuối cùng truy cập thời gian
        /// </summary>
        public DateTime? LastVisitTime { get; set; }

        /// <summary>
        /// đăng nhập lần số
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
