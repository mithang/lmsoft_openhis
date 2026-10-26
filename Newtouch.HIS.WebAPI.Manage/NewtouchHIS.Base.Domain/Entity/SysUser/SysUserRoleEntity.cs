using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///bảng người dùng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_UserRole", "SysUserRoleEntity")]
    public partial class SysUserRoleEntity : ISysEntity
    {

        /// <summary>
        /// Desc:khóa chính người dùng
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "UserId不可为空")]
        [StringLength(50, ErrorMessage = "UserId长度限制为50")]
        public string UserId { get; set; }

        /// <summary>
        /// Desc:tài khoản
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "RoleId不可为空")]
        public string RoleId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

    }
}
