using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Bảng ủy quyền vai trò
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_RoleAuthorize", "Quyền menu vai trò")]
    public partial class SysRoleAuthorizeEntity:ISysEntity
    {
        /// <summary>
        /// Desc:Ủy quyền vai tròKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:Đối tượngKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "RoleId không được để trống")]
        [StringLength(50, ErrorMessage = "RoleId giới hạn độ dài là 50")]
        public string RoleId { get; set; }

        /// <summary>
        /// Desc:Hạng mụcKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ItemId không được để trống")]
        [StringLength(50, ErrorMessage = "ItemId giới hạn độ dài là 50")]
        public string ItemId { get; set; }

        /// <summary>
        /// Desc:Mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
