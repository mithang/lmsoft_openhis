using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("cw_fp")]
    public class FinancialInvoiceEntity : IEntity<FinancialInvoiceEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string fpdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string szm { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long qsfph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long jsfph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long dqfph { get; set; }

        /// <summary>
        /// Nhân viên lĩnh dùng
        /// </summary>
        public DateTime lyrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string lyry { get; set; }

        /// <summary>
        /// 0:Không hiệu lực; 1:Hiệu lực
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

    }
}
