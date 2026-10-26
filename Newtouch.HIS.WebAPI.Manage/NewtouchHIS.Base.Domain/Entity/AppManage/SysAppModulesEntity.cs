using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///bảng mô-đun đăng ký ứng dụng
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_App_Modules", "SysAppModulesEntity")]
    public partial class SysAppModulesEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:loại mô-đun
        /// Default: 1 cấp nghiệp vụ ModuTypeEnum
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuType不可为空")]
        public int ModuType { get; set; }

        /// <summary>
        /// Desc:tên mô-đun
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuName不可为空")]
        [StringLength(50, ErrorMessage = "ModuName长度限制为50")]
        public string ModuName { get; set; }

        /// <summary>
        /// Desc:mô tả mô-đun
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuDesc不可为空")]
        [StringLength(300, ErrorMessage = "ModuDesc长度限制为300")]
        public string ModuDesc { get; set; }

        /// <summary>
        /// Desc:liên kết ứng dụng ủy quyềnid 
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AuthId不可为空")]
        [StringLength(50, ErrorMessage = "AuthId长度限制为50")]
        public string AuthAppId { get; set; }

        /// <summary>
        /// Desc:đường dẫn mô-đun
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "ModuPath长度限制为200")]
        public string ModuPath { get; set; }

        /// <summary>
        /// Desc:cấp truy cập mô-đun AuthLevEnum
        /// Default:AuthLevEnum.user
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuLev不可为空")]
        public int ModuLev { get; set; }

        /// <summary>
        /// Desc:cấp ủy quyền
        /// Default:AuthLevEnum.user
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "LimitLevs不可为空")]
        [StringLength(1000, ErrorMessage = "LimitLevs长度限制为1000")]
        public string AuthLevs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime不可为空")]
        [StringLength(23, ErrorMessage = "CreateTime长度限制为23")]
        public DateTime CreateTime { get; set; }

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
        public string zt { get; set; }

    }
}
