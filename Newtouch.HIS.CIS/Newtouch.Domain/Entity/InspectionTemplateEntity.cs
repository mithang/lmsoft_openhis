using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("jyjc_mb")]
    public class InspectionTemplateEntity : IEntity<InspectionTemplateEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string mbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 1 Xét nghiệm 2 Kiểm tra
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mbmc { get; set; }

        /// <summary>
        /// Tương ứng jyjc_dl Nhóm lớn không thu phí
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zxks { get; set; }

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
