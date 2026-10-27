using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity.SysManage
{
    ///<summary>
    ///明细字典表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_ItemsDetail", "明细字典表")]
    public partial class SysItemsDetailEntity : IEntity
    {
        /// <summary>
        /// Desc:明细Khóa chính
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
        [Required(ErrorMessage = "TopOrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "TopOrganizeId giới hạn độ dài là 50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:主表Khóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ItemId không được để trống")]
        [StringLength(50, ErrorMessage = "ItemId giới hạn độ dài là 50")]
        public string ItemId { get; set; }

        /// <summary>
        /// Desc:父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId giới hạn độ dài là 50")]
        public string? ParentId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(500, ErrorMessage = "Name giới hạn độ dài là 500")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:Mã
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Code không được để trống")]
        [StringLength(20, ErrorMessage = "Code giới hạn độ dài là 20")]
        public string Code { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description giới hạn độ dài là 500")]
        public string? Description { get; set; }

        /// <summary>
        /// Desc:排序码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }


    }
}
