using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Bảng tổ hợp tính chất bệnh nhân hệ thống
    /// </summary>
    [Table("xt_brxzzhb")]
    public class SysPatientComprehensiveNatureEntity : IEntity<SysPatientComprehensiveNatureEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int brxzzhbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zbrxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string cbrxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jssx { get; set; }

        /// <summary>
        /// Có loại trừ hạng mục đã tính hay không 1:Loại trừ 0 Không loại trừ
        /// </summary>
        public string sfpc { get; set; }

        /// <summary>
        /// 1:Hiệu lực 0 Không hiệu lực
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
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

    }
}
