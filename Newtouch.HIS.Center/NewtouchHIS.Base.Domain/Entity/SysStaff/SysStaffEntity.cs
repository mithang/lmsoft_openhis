using System.ComponentModel.DataAnnotations;
using SqlSugar;
using Newtonsoft.Json;
using NewtouchHIS.Lib.Base.EnumExtend;

namespace NewtouchHIS.Base.Domain.Entity
{
    ///<summary>
    ///职工信息表
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("Sys_Staff", "SysStaffEntity")]
    public partial class SysStaffEntity
    {
        public SysStaffEntity() { }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:组织Khóa chính
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "OrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "OrganizeId giới hạn độ dài là 50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "TopOrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "TopOrganizeId giới hạn độ dài là 50")]
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "gh không được để trống")]
        [StringLength(20, ErrorMessage = "gh giới hạn độ dài là 20")]
        public string gh { get; set; }

        /// <summary>
        /// Desc:Họ tên
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Name không được để trống")]
        [StringLength(50, ErrorMessage = "Name giới hạn độ dài là 50")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "py giới hạn độ dài là 50")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "DepartmentCode giới hạn độ dài là 20")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Desc:职称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("Title")]
        [StringLength(50, ErrorMessage = "zc giới hạn độ dài là 50")]
        public string zc { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(512, ErrorMessage = "HeadIcon giới hạn độ dài là 512")]
        public string HeadIcon { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "Gender giới hạn độ dài là 1")]
        public bool? Gender { get; set; }

        /// <summary>
        /// Desc:生日
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "Birthday giới hạn độ dài là 23")]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Desc:手机
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "MobilePhone giới hạn độ dài là 20")]
        public string MobilePhone { get; set; }

        /// <summary>
        /// Desc:邮箱
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "Email giới hạn độ dài là 50")]
        public string Email { get; set; }

        /// <summary>
        /// Desc:微信
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "WeChat giới hạn độ dài là 50")]
        public string WeChat { get; set; }

        /// <summary>
        /// Desc:主管Khóa chính
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ManagerCode giới hạn độ dài là 20")]
        public string ManagerCode { get; set; }

        /// <summary>
        /// Desc:排序码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(500, ErrorMessage = "Description giới hạn độ dài là 500")]
        public string Description { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreateTime không được để trống")]
        [StringLength(23, ErrorMessage = "CreateTime giới hạn độ dài là 23")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Desc:创建用户
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "CreatorCode không được để trống")]
        [StringLength(50, ErrorMessage = "CreatorCode giới hạn độ dài là 50")]
        public string CreatorCode { get; set; }

        /// <summary>
        /// Desc:最后修改时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastModifyTime giới hạn độ dài là 23")]
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// Desc:最后修改用户
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
        [StringLength(50, ErrorMessage = "kflb giới hạn độ dài là 50")]
        public string kflb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "mbqx giới hạn độ dài là 10")]
        public string mbqx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "zjlx giới hạn độ dài là 10")]
        public string zjlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "zjh giới hạn độ dài là 50")]
        public string zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjybdm giới hạn độ dài là 50")]
        public string gjybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "MsEmail giới hạn độ dài là 100")]
        public string MsEmail { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "yszydm giới hạn độ dài là 50")]
        public string yszydm { get; set; }

    }
}
