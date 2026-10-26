using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// hệ thống thuốc nguyên nhân lãi/lỗ
    /// </summary>
    [Table("xt_ypsyyy")]
    public class SysMedicineProfitLossReasonEntity : IEntity<SysMedicineProfitLossReasonEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string syyyId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// 0 báo hỏng, 1 báo thừa
        /// </summary>
        public string sybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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

    }
}
