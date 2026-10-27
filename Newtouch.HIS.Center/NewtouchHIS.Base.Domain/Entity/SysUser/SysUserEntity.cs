using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Bảng người dùng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_User", "SysUserEntity")]
    public partial class SysUserEntity
    {
        public SysUserEntity() { }

        /// <summary>
        /// Desc:Người dùngKhóa chính
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
        /// Desc:Tài khoản
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Account không được để trống")]
        [StringLength(20, ErrorMessage = "Account giới hạn độ dài là 20")]
        public string Account { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LanguageType giới hạn độ dài là 50")]
        public string LanguageType { get; set; }

        /// <summary>
        /// Desc:Thời gian tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime không được để trống")]
        [StringLength(23, ErrorMessage = "CreateTime giới hạn độ dài là 23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:Người tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode không được để trống")]
        [StringLength(50, ErrorMessage = "CreatorCode giới hạn độ dài là 50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:Thời gian sửa đổi cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime giới hạn độ dài là 23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:Người sửa đổi cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode giới hạn độ dài là 50")]
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt không được để trống")]
        [StringLength(1, ErrorMessage = "zt giới hạn độ dài là 1")]
        public string zt { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

    }
}
