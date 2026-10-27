using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_ypfl")]
    public class SysMedicineClassificationVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int ypflId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypflCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
