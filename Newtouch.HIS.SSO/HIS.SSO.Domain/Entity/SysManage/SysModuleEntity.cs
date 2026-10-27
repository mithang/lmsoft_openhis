using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace HIS.SSO.Domain.Entity
{
    ///<summary>
    ///mô-đun hệ thống
    ///</summary>
    [Tenant(DBEnum.UnionDb)]
    [SugarTable("Sys_Module", "Mô-đun hệ thống")]
    public partial class SysModuleEntity : IEntity
    {
        /// <summary>
        /// Desc:khóa chính của mô-đun
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:cấp cha
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId giới hạn độ dài 50")]
        public string ParentId { get; set; }

        /// <summary>
        /// Desc:tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(50, ErrorMessage = "Name giới hạn độ dài 50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "EnName giới hạn độ dài 50")]
        public string? EnName { get; set; }

        /// <summary>
        /// Desc:mã hóa
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code giới hạn độ dài 20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:biểu tượng
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Icon giới hạn độ dài 50")]
        public string? Icon { get; set; }

        /// <summary>
        /// Desc:kết nối
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "UrlAddress giới hạn độ dài 500")]
        public string? UrlAddress { get; set; }

        /// <summary>
        /// Desc:mục tiêu
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Target giới hạn độ dài 50")]
        public string? Target { get; set; }

        /// <summary>
        /// Desc:mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:mô tả
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description giới hạn độ dài 500")]
        public string? Description { get; set; }
 
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "AppId giới hạn độ dài 50")]
        public string? AppId { get; set; }

    }
}
