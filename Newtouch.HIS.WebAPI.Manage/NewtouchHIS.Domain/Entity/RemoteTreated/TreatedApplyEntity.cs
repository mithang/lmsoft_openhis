using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Domain.Entity.RemoteTreated
{
    ///<summary>
    ///khoa hệ thống
    ///</summary>
    [Tenant(DBEnum.CisDb)]
    [SugarTable("zl_zlsq", "TreatedApplyEntity")]
    public class TreatedApplyEntity : IEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        /// <summary>
        /// Desc:thời gian đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "sqsj长度限制为23")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:thời gian khám bệnh
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "jzsj长度限制为23")]
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:khoa đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "deptCode长度限制为20")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:bác sĩ đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ysgh长度限制为20")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:trạng thái đăng ký
        /// Default:EmunRemoteTreatedStu
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sqzt不可为空")]
        public int sqzt { get; set; }

        /// <summary>
        /// Desc:định danh bệnh nhân
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "patid长度限制为50")]
        public string patid { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        [Required(ErrorMessage = "kh不可为空")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xm不可为空")]
        [StringLength(50, ErrorMessage = "xm长度限制为50")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xb不可为空")]
        [StringLength(1, ErrorMessage = "xb长度限制为1")]
        public string xb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "nl不可为空")]
        [StringLength(50, ErrorMessage = "nl长度限制为50")]
        public string? nl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "zjh长度限制为50")]
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "birth长度限制为10")]
        public DateTime? birth { get; set; }

        /// <summary>
        /// Desc:tính chất bệnh nhân
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "brxz长度限制为20")]
        public string? brxz { get; set; } 

        /// <summary>
        /// Desc:số phòng họp
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "mettingId长度限制为50")]
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:thông tin người đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "sqr长度限制为50")]
        public string sqr { get; set; }

        /// <summary>
        /// Desc:số thứ tự đăng ký（do hệ thống bên thứ ba tạo ra，không được trùng lặp）
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "sqlsh长度限制为50")]
        public string sqlsh { get; set; }
        /// <summary>
        /// đăng kýAppId
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// cơ quan đăng ký
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// tên cơ quan
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// số hiệu nhân viên đăng ký
        /// </summary>
        public string? sqrgh { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get;set; }
    }
}
