using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///bảng ủy quyền vai trò
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_RoleAuthorize", "角色菜单权限")]
    public partial class SysRoleAuthorizeEntity:ISysEntity
    {
        /// <summary>
        /// Desc:khóa chính ủy quyền vai trò
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:khóa chính đối tượng
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "RoleId不可为空")]
        [StringLength(50, ErrorMessage = "RoleId长度限制为50")]
        public string RoleId { get; set; }

        /// <summary>
        /// Desc:khóa chính hạng mục
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ItemId不可为空")]
        [StringLength(50, ErrorMessage = "ItemId长度限制为50")]
        public string ItemId { get; set; }

        /// <summary>
        /// Desc:mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
