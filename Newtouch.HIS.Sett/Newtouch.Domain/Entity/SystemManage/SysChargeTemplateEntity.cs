using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("xt_sfmb")]
    public class SysChargeTemplateEntity : IEntity<SysChargeTemplateEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string sfmbbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmb { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfmbmc { get; set; }

        /// <summary>
        /// 0  Toàn bộ  1 Khám ngoại trú 2 Nhập viện
        /// </summary>
        public byte mzzybz { get; set; }

        /// <summary>
        /// 0 Mẫu toàn viện   1 Mẫu khoa   2 Mẫu cá nhân   
        /// </summary>
        public byte kffw { get; set; }

        /// <summary>
        /// Khoa
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

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
        public int? px { get; set; }

        public string py { get; set; }
        public string yfdm { get; set; }
        public string yfmc { get; set; }

    }
}
