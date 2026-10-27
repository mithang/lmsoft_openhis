using System.ComponentModel.DataAnnotations.Schema;
using SqlSugar;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("V_S_xt_mz")]
    public class SysNationVEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int mzId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzmc { get; set; }

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
