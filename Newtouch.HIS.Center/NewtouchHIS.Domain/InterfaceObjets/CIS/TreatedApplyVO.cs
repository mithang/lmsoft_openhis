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
        /// Desc:Đăng kýThời gian
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Thời gian đăng ký không được để trống")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:Thời gian khám
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:Đăng kýKhoa
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Khoa phòng không được để trống")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:Đăng kýBác sĩ
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Mã bác sĩ không được để trống")]
        public string ysgh { get; set; }

        /// <summary>
        /// Desc:Đăng kýTrạng thái
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
        /// Khám bệnhSố thẻ
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
        /// Desc:Tính chất bệnh nhân
        /// Default:EnumBrxz
        /// Nullable:True
        /// </summary>           
        public string brxz { get; set; } = ((int)EnumBrxz.zf).ToString();

        /// <summary>
        /// Desc:Cuộc họpSố
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:Đăng kýthông tin người
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string sqr { get; set; }

        /// <summary>
        /// Desc:Đăng kýSố giao dịch(Do hệ thống thứ ba tạo,Không được trùng lặp)
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
        /// Tổ chứcTên
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng kýMã số nhân viên
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
        /// Tổ chứcTên
        /// </summary>
        public string ApplyOrgName { get; set; }
        /// <summary>
        /// Đăng kýMã số nhân viên
        /// </summary>
        public string sqrgh { get; set; }
        /// <summary>
        /// Desc:Đăng kýThời gian
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Thời gian hội chẩn đăng ký không được để trống")]
        public DateTime? sqsj { get; set; }

        /// <summary>
        /// Desc:Thời gian khám
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? jzsj { get; set; }

        /// <summary>
        /// Desc:Đăng kýKhoa
        /// Default:
        /// Nullable:True
        /// </summary>   
        [Required(ErrorMessage = "Khoa phòng không được để trống")]
        public string ks { get; set; }

        /// <summary>
        /// Desc:Đăng kýBác sĩ
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
        /// Khám bệnhSố thẻ
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
        /// Desc:Cuộc họpSố
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? mettingId { get; set; }

        /// <summary>
        /// Desc:Đăng kýthông tin người(Có thể dùng choĐăng kýCuộc họpPhòng)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? sqr { get; set; }

        /// <summary>
        /// Desc:Đăng kýSố giao dịch(Do hệ thống thứ ba tạo,Không được trùng lặp)
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
    /// Đăng ký khám chữa bệnhThông tin dư thừa
    /// </summary>
    public class TreatedApplyExtendVO : TreatedApplyVO
    {
        public string? ksmc { get; set; }
        public string? ysxm { get; set; }
    }
}
