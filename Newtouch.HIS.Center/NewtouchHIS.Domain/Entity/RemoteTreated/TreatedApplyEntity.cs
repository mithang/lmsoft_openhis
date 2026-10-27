using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Domain.Entity.RemoteTreated
{
    ///<summary>
    ///Khoa hệ thống
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
        /// Desc:Đăng kýThời gian
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "sqsj giới hạn độ dài là 23")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:Thời gian khám
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "jzsj giới hạn độ dài là 23")]
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:Đăng kýKhoa
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "deptCode giới hạn độ dài là 20")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:Đăng kýBác sĩ
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ysgh giới hạn độ dài là 20")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:Đăng kýTrạng thái
        /// Default:EmunRemoteTreatedStu
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sqzt không được để trống")]
        public int sqzt { get; set; }

        /// <summary>
        /// Desc:Mã định danh bệnh nhân
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "patid giới hạn độ dài là 50")]
        public string patid { get; set; }
        /// <summary>
        /// Khám bệnhSố thẻ
        /// </summary>
        [Required(ErrorMessage = "kh không được để trống")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xm không được để trống")]
        [StringLength(50, ErrorMessage = "xm giới hạn độ dài là 50")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xb không được để trống")]
        [StringLength(1, ErrorMessage = "xb giới hạn độ dài là 1")]
        public string xb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "nl không được để trống")]
        [StringLength(50, ErrorMessage = "nl giới hạn độ dài là 50")]
        public string? nl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "zjh giới hạn độ dài là 50")]
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "birth giới hạn độ dài là 10")]
        public DateTime? birth { get; set; }

        /// <summary>
        /// Desc:Tính chất bệnh nhân
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "brxz giới hạn độ dài là 20")]
        public string? brxz { get; set; } 

        /// <summary>
        /// Desc:Cuộc họpSố
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "mettingId giới hạn độ dài là 50")]
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:Đăng kýthông tin người
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "sqr giới hạn độ dài là 50")]
        public string sqr { get; set; }

        /// <summary>
        /// Desc:Đăng kýSố giao dịch(Do hệ thống thứ ba tạo,Không được trùng lặp)
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "sqlsh giới hạn độ dài là 50")]
        public string sqlsh { get; set; }
        /// <summary>
        /// Đăng kýAppId
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// Đăng kýTổ chức
        /// </summary>
        public string? ApplyOrg { get; set; }
        /// <summary>
        /// Tổ chứcTên
        /// </summary>
        public string? ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng kýMã số nhân viên
        /// </summary>
        public string? sqrgh { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get;set; }
    }
}
