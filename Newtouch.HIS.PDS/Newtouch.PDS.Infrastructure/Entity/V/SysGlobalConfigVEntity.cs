using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// Cấu hình toàn cục
    /// </summary>
    [Table("V_S_Sys_GlobalConfig")]
    [SugarTable("V_S_Sys_GlobalConfig")]
    public class SysGlobalConfigVEntity
    {
        /// <summary>
        /// Mã
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// giá trị
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
