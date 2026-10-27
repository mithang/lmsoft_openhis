using System;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_sfxm_dyy")]
    public class SysChargeItemMultiLanguageVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int sfxmdyyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmcFanti { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmcEnglish { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmmcJpan { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
