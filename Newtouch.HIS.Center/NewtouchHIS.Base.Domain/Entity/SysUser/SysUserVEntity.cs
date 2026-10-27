using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain
{
    /// <summary>
    /// Người dùng hệ thống
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("V_C_Sys_User")]
    [Description("View người dùng hệ thống")]
    public class SysUserVEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Required, MaxLength(50)]
        public string Id { get; set; }

        /// <summary>
        /// Cấp cao nhấtTổ chức
        /// </summary>
        [Required, MaxLength(50)]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Tài khoản
        /// </summary>
        [Required, MaxLength(20)]
        public string Account { get; set; }

        /// <summary>
        /// Loại ngôn ngữ
        /// </summary>
        [Required, MaxLength(50)]
        public string? LanguageType { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        [Required, MaxLength(50)]
        public string UserPassword { get; set; }

        /// <summary>
        /// Mật khẩuKey
        /// </summary>
        [Required, MaxLength(50)]
        public string UserSecretkey { get; set; }

        /// <summary>
        /// Có bị khóa hay không.trueKhóa,nếu không là chưa khóa
        /// </summary>
        [MaxLength(1)]
        public bool? Locked { get; set; }

        /// <summary>
        /// Cờ hiệu lực
        /// </summary>
        [Required, MaxLength(1)]
        public string zt { get; set; }

    }
}
