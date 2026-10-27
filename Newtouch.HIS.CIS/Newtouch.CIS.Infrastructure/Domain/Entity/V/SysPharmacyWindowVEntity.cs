using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_yfck")]
    public class SysPharmacyWindowVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int yfckId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfckCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfckmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string kqbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
