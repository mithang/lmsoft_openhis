using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Organize", "Bảng tổ chức")]
    public partial class SysOrganizeEntity : ISysEntity
    {
        /// <summary>
        /// Desc:Tổ chứcKhóa chính
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
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "TopOrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "TopOrganizeId giới hạn độ dài là 50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:Tên
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
        /// Desc:Phân loại
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CategoryCode không được để trống")]
        [StringLength(20, ErrorMessage = "CategoryCode giới hạn độ dài là 20")]
        public string? CategoryCode { get; set; }

        /// <summary>
        /// Desc:Người phụ trách
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ManagerCode giới hạn độ dài là 20")]
        public string? ManagerCode { get; set; }

        /// <summary>
        /// Desc:Điện thoại
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "TelePhone giới hạn độ dài là 20")]
        public string? TelePhone { get; set; }

        /// <summary>
        /// Desc:WeChat
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "WeChat giới hạn độ dài là 50")]
        public string? WeChat { get; set; }

        /// <summary>
        /// Desc:Fax
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "Fax giới hạn độ dài là 20")]
        public string? Fax { get; set; }

        /// <summary>
        /// Desc:Email
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
        /// Desc:Địa chỉ liên hệ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Address giới hạn độ dài là 500")]
        public string? Address { get; set; }

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

   

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjjgdm giới hạn độ dài là 50")]
        public string? gjjgdm { get; set; }

    }
}
