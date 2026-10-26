using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Chi tiết quyết toán nội trú
    /// </summary>
    [Table("zy_jsmx")]
    public class HospSettlementDetailEntity : IEntity<HospSettlementDetailEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int jsmxbh { get; set; }

        /// <summary>
        /// Tổ chứcId(Bệnh viện có nghiệp vụ cụ thể)
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int jsnm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? xmjfbbh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string fzlxmjfbId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ypjfbbh { get; set; }

        /// <summary>
        /// 1 Thuốc   2 Hạng mục
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// Số tiền giao dịch(dj*sl)
        /// </summary>
        public decimal? jyje { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? jyfwje { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
