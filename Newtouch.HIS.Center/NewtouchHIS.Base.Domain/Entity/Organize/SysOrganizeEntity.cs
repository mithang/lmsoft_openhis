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
        /// Desc:组织Khóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:父级
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ParentId giới hạn độ dài là 50")]
        public string? ParentId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "TopOrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "TopOrganizeId giới hạn độ dài là 50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(256, ErrorMessage = "Name giới hạn độ dài là 256")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:Mã
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Code giới hạn độ dài là 20")]
        public string? Code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(256, ErrorMessage = "ShortName giới hạn độ dài là 256")]
        public string? ShortName { get; set; }

        /// <summary>
        /// Desc:分类
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CategoryCode không được để trống")]
        [StringLength(20, ErrorMessage = "CategoryCode giới hạn độ dài là 20")]
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Desc:负责人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ManagerCode giới hạn độ dài là 20")]
        public string? ManagerCode { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "TelePhone giới hạn độ dài là 20")]
        public string? TelePhone { get; set; }

        /// <summary>
        /// Desc:微信
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "WeChat giới hạn độ dài là 50")]
        public string? WeChat { get; set; }

        /// <summary>
        /// Desc:传真
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Fax giới hạn độ dài là 20")]
        public string? Fax { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Email giới hạn độ dài là 50")]
        public string? Email { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "AreaId giới hạn độ dài là 50")]
        public string? AreaId { get; set; }

        /// <summary>
        /// Desc:联系地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Address giới hạn độ dài là 500")]
        public string? Address { get; set; }

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

   

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjjgdm giới hạn độ dài là 50")]
        public string? gjjgdm { get; set; }

    }
}
