using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///Bảng mô-đun đăng ký ứng dụng
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
        /// Desc:Loại mô-đun
        /// Default: 1 Cấp nghiệp vụ ModuTypeEnum
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuType không được để trống")]
        public int ModuType { get; set; }

        /// <summary>
        /// Desc:Tên mô-đun
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuName không được để trống")]
        [StringLength(50, ErrorMessage = "ModuName giới hạn độ dài là 50")]
        public string ModuName { get; set; }

        /// <summary>
        /// Desc:Mô tả mô-đun
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuDesc không được để trống")]
        [StringLength(300, ErrorMessage = "ModuDesc giới hạn độ dài là 300")]
        public string ModuDesc { get; set; }

        /// <summary>
        /// Desc:Ứng dụng ủy quyền đã gắnid 
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "AuthId không được để trống")]
        [StringLength(50, ErrorMessage = "AuthId giới hạn độ dài là 50")]
        public string AuthAppId { get; set; }

        /// <summary>
        /// Desc:Đường dẫn mô-đun
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "ModuPath giới hạn độ dài là 200")]
        public string ModuPath { get; set; }

        /// <summary>
        /// Desc:Cấp truy cập mô-đun AuthLevEnum
        /// Default:AuthLevEnum.user
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ModuLev không được để trống")]
        public int ModuLev { get; set; }

        /// <summary>
        /// Desc:Cấp ủy quyền
        /// Default:AuthLevEnum.user
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "LimitLevs không được để trống")]
        [StringLength(1000, ErrorMessage = "LimitLevs giới hạn độ dài là 1000")]
        public string AuthLevs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime không được để trống")]
        [StringLength(23, ErrorMessage = "CreateTime giới hạn độ dài là 23")]
        public DateTime CreateTime { get; set; }

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
        public string zt { get; set; }

    }
}
