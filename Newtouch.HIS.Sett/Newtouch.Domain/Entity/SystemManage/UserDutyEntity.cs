using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Sys_UserDuty")]
    [Obsolete("暂用不到")]
    public class UserDutyEntity : IEntity<UserDutyEntity>
    {
        /// <summary>
        /// Khóa chính ủy quyền vị trí
        /// </summary>
        [Key]
        public string Id { get; set; }


        /// <summary>
        /// Khóa chính người dùng
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Khóa chính vị trí
        /// </summary>
        public string DutyId { get; set; }

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

    }
}
