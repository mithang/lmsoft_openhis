using NewtouchHIS.Base.Domain.Entity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NewtouchHIS.Domain.Enum.HisEnum;

namespace NewtouchHIS.Domain.InterfaceObjets.CIS
{
    public class TreatedApplyVO : IEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Id { get; set; }

        /// <summary>
        /// Desc:thời gian đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "申请时间不可为空")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:thời gian khám bệnh
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:khoa đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "科室不可为空")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:bác sĩ đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "医生工号不可为空")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:trạng thái đăng ký
        /// Default:EmunRemoteTreatedStu
        /// Nullable:False
        /// </summary>           
        public int sqzt { get; set; }

        /// <summary>
        /// Desc:định danh bệnh nhân
        /// Default:
        /// Nullable:True
        /// </summary>
        [Required(ErrorMessage = "患者id不可为空")]
        public string patid { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        [Required(ErrorMessage = "就诊卡号不可为空")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:họ tên bệnh nhân
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "患者姓名不可为空")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xb不可为空")]
        public string xb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string? nl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? birth { get; set; }

        /// <summary>
        /// Desc:tính chất bệnh nhân
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        public string brxz { get; set; } = ((int)EnumBrxz.zf).ToString();

        /// <summary>
        /// Desc:số phòng họp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:thông tin người đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqr { get; set; }

        /// <summary>
        /// Desc:số thứ tự đăng ký（do hệ thống bên thứ ba tạo ra，không được trùng lặp）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqlsh { get; set; }
        /// <summary>
        /// đăng kýAppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// cơ quan đăng ký
        /// </summary>
        public string ApplyOrg { get; set; }
        /// <summary>
        /// tên cơ quan
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// số hiệu nhân viên đăng ký
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }

    public class TreatedApplyBaseVO
    {
        /// <summary>
        /// đăng kýAppId
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// cơ quan đăng ký
        /// </summary>
        public string ApplyOrg { get; set; }
        /// <summary>
        /// tên cơ quan
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// số hiệu nhân viên đăng ký
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// Desc:thời gian đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "申请会诊时间不可为空")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:thời gian khám bệnh
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:khoa đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "科室不可为空")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:bác sĩ đăng ký
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "医生工号不可为空")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:định danh bệnh nhân
        /// Default:
        /// Nullable:True
        /// </summary>
        [Required(ErrorMessage = "患者id不可为空")]
        public string patid { get; set; }
        /// <summary>
        /// số thẻ khám bệnh
        /// </summary>
        [Required(ErrorMessage = "就诊卡号不可为空")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:họ tên bệnh nhân
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "患者姓名不可为空")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:số phòng họp
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:thông tin người đăng ký(có thể dùng để đăng ký phòng họp)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqr { get; set; }

        /// <summary>
        /// Desc:số thứ tự đăng ký（do hệ thống bên thứ ba tạo ra，không được trùng lặp）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqlsh { get; set; }
        /// <summary>
        /// số khám ngoại trú
        /// </summary>
        public string? mzh { get; set; }

    }
    /// <summary>
    /// thông tin dự phòng đăng ký khám chữa bệnh
    /// </summary>
    public class TreatedApplyExtendVO : TreatedApplyVO
    {
        public string? ksmc { get; set; }
        public string? ysxm { get; set; }
    }
}
