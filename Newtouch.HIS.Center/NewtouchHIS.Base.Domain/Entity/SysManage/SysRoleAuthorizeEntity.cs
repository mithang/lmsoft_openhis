using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///角色授权表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_RoleAuthorize", "角色菜单权限")]
    public partial class SysRoleAuthorizeEntity:ISysEntity
    {
        /// <summary>
        /// Desc:角色授权Khóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:对象Khóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "RoleId không được để trống")]
        [StringLength(50, ErrorMessage = "RoleId giới hạn độ dài là 50")]
        public string RoleId { get; set; }

        /// <summary>
        /// Desc:项目Khóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ItemId không được để trống")]
        [StringLength(50, ErrorMessage = "ItemId giới hạn độ dài là 50")]
        public string ItemId { get; set; }

        /// <summary>
        /// Desc:排序码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
