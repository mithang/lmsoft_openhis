using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///Bảng vai trò
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Role", "Vai trò hệ thống")]
    public partial class SysRoleEntity : IEntity
    {
        /// <summary>
        /// Desc:Vai tròKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }


        /// <summary>
        /// Desc:Tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(50, ErrorMessage = "Name giới hạn độ dài là 50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:Số hiệu
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code giới hạn độ dài là 20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:Mô tả
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description giới hạn độ dài là 500")]
        public string? Description { get; set; }

        /// <summary>
        /// Desc:Mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
