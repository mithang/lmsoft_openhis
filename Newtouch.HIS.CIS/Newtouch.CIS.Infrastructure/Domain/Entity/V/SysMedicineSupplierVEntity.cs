using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_ypgys")]
    public class SysMedicineSupplierVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int gysId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string gysmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

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
