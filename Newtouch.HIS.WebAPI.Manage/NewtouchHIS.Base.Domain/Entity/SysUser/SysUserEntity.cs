using System.ComponentModel.DataAnnotations;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///bảng người dùng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_User", "SysUserEntity")]
    public partial class SysUserEntity
    {
        public SysUserEntity() { }

        /// <summary>
        /// Desc:khóa chính người dùng
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
        [Required(ErrorMessage = "TopOrganizeId不可为空")]
        [StringLength(50, ErrorMessage = "TopOrganizeId长度限制为50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:tài khoản
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Account不可为空")]
        [StringLength(20, ErrorMessage = "Account长度限制为20")]
        public string Account { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LanguageType长度限制为50")]
        public string LanguageType { get; set; }

        /// <summary>
        /// Desc:thời gian tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:người tạo
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode不可为空")]
        [StringLength(50, ErrorMessage = "CreatorCode长度限制为50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:thời gian sửa đổi lần cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime长度限制为23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:người sửa đổi lần cuối
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "LastModifierCode长度限制为50")]
        public string LastModifierCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zt不可为空")]
        [StringLength(1, ErrorMessage = "zt长度限制为1")]
        public string zt { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

    }
}
