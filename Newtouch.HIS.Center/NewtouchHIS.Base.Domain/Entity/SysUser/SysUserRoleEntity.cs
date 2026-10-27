using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Bảng người dùng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_UserRole", "SysUserRoleEntity")]
    public partial class SysUserRoleEntity : ISysEntity
    {

        /// <summary>
        /// Desc:Người dùngKhóa chính
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
        [Required(ErrorMessage = "UserId không được để trống")]
        [StringLength(50, ErrorMessage = "UserId giới hạn độ dài là 50")]
        public string UserId { get; set; }

        /// <summary>
        /// Desc:Tài khoản
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "RoleId không được để trống")]
        public string RoleId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

    }
}
