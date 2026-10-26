using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// người dùng hệ thống
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_C_Sys_User")]
    [Description("系统用户视图")]
    public class SysUserVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required, MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// tổ chức cấp cao nhất
        /// </summary>
        [Required, MaxLength(50)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// tài khoản
        /// </summary>
        [Required, MaxLength(20)]
        public string Account { get; set; }

        /// <summary>
        /// loại ngôn ngữ
        /// </summary>
        [Required, MaxLength(50)]
        public string? LanguageType { get; set; }

        /// <summary>
        /// mật khẩu
        /// </summary>
        [Required, MaxLength(50)]
        public string UserPassword { get; set; }

        /// <summary>
        /// mật khẩuKey
        /// </summary>
        [Required, MaxLength(50)]
        public string UserSecretkey { get; set; }

        /// <summary>
        /// có bị khóa hay không。truekhóa，nếu không thì chưa khóa
        /// </summary>
        [MaxLength(1)]
        public bool? Locked { get; set; }

        /// <summary>
        /// cờ hiệu lực
        /// </summary>
        [Required, MaxLength(1)]
        public string zt { get; set; }

    }
}
