using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_yzpc")]
    public class SysMedicalOrderFrequencyVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int yzpcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        public int zxcs { get; set; }
        public int zxzq { get; set; }
        public string zxzqdw { get; set; }

    }
}
