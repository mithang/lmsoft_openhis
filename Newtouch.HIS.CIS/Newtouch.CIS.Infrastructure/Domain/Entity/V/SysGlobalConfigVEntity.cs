using System.ComponentModel.DataAnnotations.Schema;
using SqlSugar;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// toàncụccấu hình
    /// </summary>
    [SugarTable("V_S_Sys_GlobalConfig")]
    public class SysGlobalConfigVEntity
    {
        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
