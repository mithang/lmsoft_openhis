using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Lý do lãi lỗ thuốc hệ thống
    /// </summary>
    [Table("xt_ypsyyy")]
    [SugarTable("xt_ypsyyy")]
    public class SysMedicineProfitLossReasonEntity : IEntity<SysMedicineProfitLossReasonEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [SugarColumn(IsPrimaryKey = true)]
        public string syyyId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string syyy { get; set; }

        /// <summary>
        /// 0 Báo hỏng,1 Báo thừa
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
