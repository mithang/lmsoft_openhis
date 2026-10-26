using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    /// <summary>
    /// toàn cục cấu hình
    /// </summary>
    [Table("V_S_Sys_GlobalConfig")]
    public class SysGlobalConfigVEntity
    {
        /// <summary>
        /// mã
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
