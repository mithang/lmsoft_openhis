using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///bảng vai trò
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Role", "系统角色")]
    public partial class SysRoleEntity : IEntity
    {
        /// <summary>
        /// Desc:khóa chính vai trò
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }


        /// <summary>
        /// Desc:tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name不可为空")]
        [StringLength(50, ErrorMessage = "Name长度限制为50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:số hiệu
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code长度限制为20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:mô tả
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description长度限制为500")]
        public string? Description { get; set; }

        /// <summary>
        /// Desc:mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

       
    }
}
