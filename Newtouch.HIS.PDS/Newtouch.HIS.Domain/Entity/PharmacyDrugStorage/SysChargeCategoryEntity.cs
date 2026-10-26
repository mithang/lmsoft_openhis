using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thu phí nhóm lớn
    /// </summary>
    [Table("xt_sfdl")]
    [Obsolete("please use the view")]
    public class SysChargeCategoryEntity : IEntity<SysChargeCategoryEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int dlId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 0 chung 1 ngoại trú 2 nội trú
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

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
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
