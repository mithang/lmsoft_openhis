using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Newtouch.HIS.Domain.Entity
{
    [Table("Sys_StaffWard")]
    public class SysStaffWardEntity : IEntity<SysStaffWardEntity>
    {
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// Id tổ chức
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StaffId { get; set; }
        /// <summary>
        /// Mã khu bệnh
        /// </summary>
        public string bqCode { get; set; }
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
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
