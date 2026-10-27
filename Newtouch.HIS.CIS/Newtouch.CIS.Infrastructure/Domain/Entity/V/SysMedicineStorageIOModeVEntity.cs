using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_ypcrkfs")]
    public class SysMedicineStorageIOModeVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int crkfsId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkfsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkfsmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string crkbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
