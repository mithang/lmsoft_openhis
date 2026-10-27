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
    ///Bảng từ điển chi tiết
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Items", "Bảng từ điển")]
    public partial class SysItemsEntity : ISysEntity
    {
        /// <summary>
        /// Desc:Chi tiếtKhóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:Cấp cha
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId giới hạn độ dài là 50")]
        public string? ParentId { get; set; }

        /// <summary>
        /// Desc:Tên
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
