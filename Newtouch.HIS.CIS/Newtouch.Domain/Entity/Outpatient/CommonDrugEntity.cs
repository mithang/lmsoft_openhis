using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("mz_cyyp")]
    public class CommonDrugEntity : IEntity<CommonDrugEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string cyypId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// Enum 1 Thường dùng của khoa 2 Thường dùng cá nhân
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng khoa của CSDL
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng nhân viên của CSDL
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// Liên kếtbaseBảng thuốc hệ thống của CSDL
        /// </summary>
        public string zdCode { get; set; }

        /// <summary>
        /// Trường dư thừa
        /// </summary>
        public string zdmc { get; set; }

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
