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
        /// Desc:Đăng ký时间
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Thời gian đăng ký không được để trống")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:就诊时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:Đăng ký科室
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Khoa phòng không được để trống")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:Đăng ký医生
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Mã bác sĩ không được để trống")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:Đăng ký状态
        /// Default:EmunRemoteTreatedStu
        /// Nullable:False
        /// </summary>           
        public int sqzt { get; set; }

        /// <summary>
        /// Desc:Mã định danh bệnh nhân
        /// Default:
        /// Nullable:True
        /// </summary>
        [Required(ErrorMessage = "Bệnh nhânid không được để trống")]
        public string patid { get; set; }
        /// <summary>
        /// 就诊Số thẻ
        /// </summary>
        [Required(ErrorMessage = "Số thẻ khám bệnh không được để trống")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:Họ tên bệnh nhân
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Họ tên bệnh nhân không được để trống")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "xb không được để trống")]
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
        /// Desc:病人性质
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        public string brxz { get; set; } = ((int)EnumBrxz.zf).ToString();

        /// <summary>
        /// Desc:Cuộc họp号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:Đăng ký人信息
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqr { get; set; }

        /// <summary>
        /// Desc:Đăng ký流水号（由第三方系统生成，不可重复）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqlsh { get; set; }
        /// <summary>
        /// Đăng kýAppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// Đăng kýTổ chức
        /// </summary>
        public string ApplyOrg { get; set; }
        /// <summary>
        /// Tổ chức名称
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng ký人工号
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }
    }

    public class TreatedApplyBaseVO
    {
        /// <summary>
        /// Đăng kýAppId
        /// </summary>
        public string? AppId { get; set; }
        /// <summary>
        /// Đăng kýTổ chức
        /// </summary>
        public string ApplyOrg { get; set; }
        /// <summary>
        /// Tổ chức名称
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng ký人工号
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// Desc:Đăng ký时间
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Thời gian hội chẩn đăng ký không được để trống")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:就诊时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:Đăng ký科室
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Khoa phòng không được để trống")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:Đăng ký医生
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Mã bác sĩ không được để trống")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:Mã định danh bệnh nhân
        /// Default:
        /// Nullable:True
        /// </summary>
        [Required(ErrorMessage = "Bệnh nhânid không được để trống")]
        public string patid { get; set; }
        /// <summary>
        /// 就诊Số thẻ
        /// </summary>
        [Required(ErrorMessage = "Số thẻ khám bệnh không được để trống")]
        public string kh { get; set; }

        /// <summary>
        /// Desc:Họ tên bệnh nhân
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "Họ tên bệnh nhân không được để trống")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:Cuộc họp号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:Đăng ký人信息(可用于Đăng kýCuộc họp室)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqr { get; set; }

        /// <summary>
        /// Desc:Đăng ký流水号（由第三方系统生成，不可重复）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqlsh { get; set; }
        /// <summary>
        /// Số ngoại trú
        /// </summary>
        public string? mzh { get; set; }

    }
    /// <summary>
    /// Đăng ký khám chữa bệnh冗余信息
    /// </summary>
    public class TreatedApplyExtendVO : TreatedApplyVO
    {
        public string? ksmc { get; set; }
        public string? ysxm { get; set; }
    }
}
