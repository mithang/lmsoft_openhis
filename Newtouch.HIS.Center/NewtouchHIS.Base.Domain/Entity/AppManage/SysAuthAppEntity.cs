using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///应用注册授权
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Auth_App", "SysAuthAppEntity")]
    public class SysAuthAppEntity
    {
        /// <summary>
        /// Desc:授权ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string AuthId { get; set; }

        /// <summary>
        /// Desc:应用通用Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AppId không được để trống")]
        [StringLength(50, ErrorMessage = "AppId giới hạn độ dài là 50")]
        public string AppId { get; set; }

        /// <summary>
        /// Desc:Tên ứng dụng
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AppName không được để trống")]
        [StringLength(128, ErrorMessage = "AppName giới hạn độ dài là 128")]
        public string AppName { get; set; }

        /// <summary>
        /// Desc:域名地址
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Domain không được để trống")]
        [StringLength(100, ErrorMessage = "Domain giới hạn độ dài là 100")]
        public string Domain { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(256, ErrorMessage = "Host giới hạn độ dài là 256")]
        public string Host { get; set; }

        /// <summary>
        /// Desc:Loại cấp quyền AuthType
        /// Default:Web
        /// Nullable:True
        /// </summary>           
        public int? AuthType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime không được để trống")]
        [StringLength(23, ErrorMessage = "CreateTime giới hạn độ dài là 23")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode không được để trống")]
        [StringLength(50, ErrorMessage = "CreatorCode giới hạn độ dài là 50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime giới hạn độ dài là 23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
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
        public string zt { get; set; } = "1";

    }
}
