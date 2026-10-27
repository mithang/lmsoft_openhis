using System.ComponentModel.DataAnnotations.Schema;
using SqlSugar;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 全局配置
    /// </summary>
    [SugarTable("V_S_Sys_GlobalConfig")]
    public class SysGlobalConfigVEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
