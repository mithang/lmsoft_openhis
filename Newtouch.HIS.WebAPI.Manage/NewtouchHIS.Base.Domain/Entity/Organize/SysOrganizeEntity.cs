using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Organize", "组织机构表")]
    public partial class SysOrganizeEntity : ISysEntity
    {
        /// <summary>
        /// Desc:khóa chính tổ chức
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
        [StringLength(50, ErrorMessage = "ParentId长度限制为50")]
        public string? ParentId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "TopOrganizeId不可为空")]
        [StringLength(50, ErrorMessage = "TopOrganizeId长度限制为50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name不可为空")]
        [StringLength(256, ErrorMessage = "Name长度限制为256")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:mã
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code长度限制为20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(256, ErrorMessage = "ShortName长度限制为256")]
        public string? ShortName { get; set; }

        /// <summary>
        /// Desc:phân loại
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CategoryCode不可为空")]
        [StringLength(20, ErrorMessage = "CategoryCode长度限制为20")]
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Desc:người phụ trách
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ManagerCode长度限制为20")]
        public string? ManagerCode { get; set; }

        /// <summary>
        /// Desc:điện thoại
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "TelePhone长度限制为20")]
        public string? TelePhone { get; set; }

        /// <summary>
        /// Desc:WeChat
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "WeChat长度限制为50")]
        public string? WeChat { get; set; }

        /// <summary>
        /// Desc:fax
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Fax长度限制为20")]
        public string? Fax { get; set; }

        /// <summary>
        /// Desc:hộp thư
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Email长度限制为50")]
        public string? Email { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "AreaId长度限制为50")]
        public string? AreaId { get; set; }

        /// <summary>
        /// Desc:địa chỉ liên hệ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Address长度限制为500")]
        public string? Address { get; set; }

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

   

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjjgdm长度限制为50")]
        public string? gjjgdm { get; set; }

    }
}
