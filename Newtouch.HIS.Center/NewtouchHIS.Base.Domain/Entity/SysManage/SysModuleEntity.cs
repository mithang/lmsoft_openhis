using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Mô-đun hệ thống
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Module", "Mô-đun hệ thống")]
    public partial class SysModuleEntity : IEntity
    {
        /// <summary>
        /// Desc:Mô-đunKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Desc:Cấp cha
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId giới hạn độ dài là 50")]
        public string ParentId { get; set; }

        /// <summary>
        /// Desc:Tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(50, ErrorMessage = "Name giới hạn độ dài là 50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "EnName giới hạn độ dài là 50")]
        public string? EnName { get; set; }

        /// <summary>
        /// Desc:Mã
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code giới hạn độ dài là 20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:Biểu tượng
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Icon giới hạn độ dài là 50")]
        public string? Icon { get; set; }

        /// <summary>
        /// Desc:nối
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "UrlAddress giới hạn độ dài là 500")]
        public string? UrlAddress { get; set; }

        /// <summary>
        /// Desc:Mục tiêu
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Target giới hạn độ dài là 50")]
        public string? Target { get; set; }

        /// <summary>
        /// Desc:Mã sắp xếp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:Mô tả
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description giới hạn độ dài là 500")]
        public string? Description { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "AppId giới hạn độ dài là 50")]
        public string? AppId { get; set; }

    }
}
