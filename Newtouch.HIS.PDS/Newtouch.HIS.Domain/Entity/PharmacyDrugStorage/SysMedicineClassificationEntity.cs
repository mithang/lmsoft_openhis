using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống phân loại thuốc
    /// </summary>
    [Table("xt_ypfl")]
    [Obsolete("please use the view")]
    public class SysMedicineClassificationEntity : IEntity<SysMedicineClassificationEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int ypflId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string flid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string flmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 1: còn hiệu lực 0: không hiệu lực
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
