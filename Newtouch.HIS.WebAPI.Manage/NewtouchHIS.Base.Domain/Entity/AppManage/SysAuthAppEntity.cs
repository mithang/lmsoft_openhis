using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///ủy quyền đăng ký ứng dụng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Auth_App", "SysAuthAppEntity")]
    public class SysAuthAppEntity
    {
        /// <summary>
        /// Desc:ủy quyềnID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string AuthId { get; set; }

        /// <summary>
        /// Desc:ứng dụng chungId
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AppId不可为空")]
        [StringLength(50, ErrorMessage = "AppId长度限制为50")]
        public string AppId { get; set; }

        /// <summary>
        /// Desc:tên ứng dụng
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AppName不可为空")]
        [StringLength(128, ErrorMessage = "AppName长度限制为128")]
        public string AppName { get; set; }

        /// <summary>
        /// Desc:địa chỉ tên miền
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Domain不可为空")]
        [StringLength(100, ErrorMessage = "Domain长度限制为100")]
        public string Domain { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(256, ErrorMessage = "Host长度限制为256")]
        public string Host { get; set; }

        /// <summary>
        /// Desc:loại ủy quyền AuthType
        /// Default:Web
        /// Nullable:True
        /// </summary>           
        public int? AuthType { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode不可为空")]
        [StringLength(50, ErrorMessage = "CreatorCode长度限制为50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime长度限制为23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:
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
        public string zt { get; set; } = "1";

    }
}
