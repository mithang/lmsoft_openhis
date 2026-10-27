using System;
using SqlSugar;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 科室备药药品损益原因
    /// </summary>
    [SugarTable("xt_ksbysyyy")]

    public class SysMedicineProfitLossReasonEntity
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
        /// 0 报损，1 报溢
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
