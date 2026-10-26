using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.ValueObjects
{

    public class SysChargeItemVO
    {

        public string OrganizeId { get; set; }
        public int sfxmId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfxmCode不可为空")]
        [StringLength(20, ErrorMessage = "sfxmCode长度限制为20")]
        public string sfxmCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfxmmc不可为空")]
        [StringLength(256, ErrorMessage = "sfxmmc长度限制为256")]
        public string sfxmmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfdlCode不可为空")]
        [StringLength(200, ErrorMessage = "sfdlCode长度限制为200")]
        public string sfdlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "badlCode长度限制为20")]
        public string? badlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "nbdlCode长度限制为20")]
        public string? nbdlCode { get; set; }


        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? duration { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py不可为空")]
        [StringLength(250, ErrorMessage = "py长度限制为250")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dw不可为空")]
        [StringLength(220, ErrorMessage = "dw长度限制为220")]
        public string dw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dj不可为空")]
        [StringLength(9, ErrorMessage = "dj长度限制为9")]
        public decimal dj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "flCode长度限制为20")]
        public string? flCode { get; set; }

        /// <summary>
        /// Desc:xác định tỷ lệ tiền mặt tự trả，không liên quan đến tính chất của phần này（tự trả theo phân loại、tự lo）	   xem trườngzfxz	   	   chú ý：khi tỷ lệ tự trả là số âm，biểu thị tự trả định mức	   (Ví dụ：một loại vật tư nào đó12300，có thể thanh toán bảo hiểm10000,khi đó tỷ lệ tự trả＝-2300)
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl不可为空")]
        [StringLength(9, ErrorMessage = "zfbl长度限制为9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:xác định phần tự trả（chi phí×tỷ lệ tự trả）tính chất của	   0  có thể thanh toán bảo hiểm 1 tự lo 2 tự trả theo phân loại 3 hoàn toàn tự lo（xử lý chuyên gia đã nghỉ hưu、phí đăng ký khám đặc biệt）	   	   （chi phí×（1－tỷ lệ tự trả） là phần có thể ghi sổ）
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz不可为空")]
        [StringLength(1, ErrorMessage = "zfxz长度限制为1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:0：chung，1：khám ngoại trú，2：nội trú
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz不可为空")]
        [StringLength(1, ErrorMessage = "mzzybz长度限制为1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:0 không cần thực hiện，（ghi sổ trực tiếp nội trú） 	   1 cần thực hiện，（chỉ được ghi sổ sau khi thực hiện nội trú）	   from xt_yjxmdz
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ssbz不可为空")]
        [StringLength(1, ErrorMessage = "ssbz长度限制为1")]
        public string ssbz { get; set; }

        /// <summary>
        /// Desc:0 hạng mục thông thường 1 hạng mục đặc biệt refer xt_sfxmtsbz	   mô tả hạng mục đặc biệt: chỉ tính chất bệnh nhân đặc biệt mới được sử dụng。 
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "tsbz不可为空")]
        [StringLength(1, ErrorMessage = "tsbz长度限制为1")]
        public string tsbz { get; set; }

        /// <summary>
        /// Desc:0 hạng mục không thu phí（Ví dụ：phí đăng ký khám..） 1 hạng mục thu phí
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfbz不可为空")]
        [StringLength(1, ErrorMessage = "sfbz长度限制为1")]
        public string sfbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ybdm长度限制为20")]
        public string? ybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(40, ErrorMessage = "wjdm长度限制为40")]
        public string? wjdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1000, ErrorMessage = "bz长度限制为1000")]
        public string? bz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }


        /// <summary>
        /// Desc:số lượng tính theo đơn vị
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? dwjls { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? jjcl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zxks长度限制为20")]
        public string? zxks { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "gg长度限制为100")]
        public string? gg { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastYBUploadTime长度限制为23")]
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "ssdj长度限制为2")]
        public string? ssdj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "sqlx长度限制为20")]
        public string? sqlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ybbz不可为空")]
        [StringLength(1, ErrorMessage = "ybbz长度限制为1")]
        public string ybbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xnhybdm长度限制为20")]
        public string? xnhybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjybdm长度限制为50")]
        public string? gjybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "cxjje长度限制为11")]
        public decimal? cxjje { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "pzwh长度限制为100")]
        public string? pzwh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "sccj长度限制为100")]
        public string? sccj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "gjybmc长度限制为100")]
        public string? gjybmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_Code长度限制为50")]
        public string? wz_Code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_ConsumablesID长度限制为50")]
        public string? wz_ConsumablesID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_SerialNumber长度限制为50")]
        public string? wz_SerialNumber { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_Packing长度限制为50")]
        public string? wz_Packing { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "wz_IsHight长度限制为1")]
        public bool? wz_IsHight { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_MaterialCode长度限制为50")]
        public string? wz_MaterialCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "isYnss长度限制为1")]
        public string? isYnss { get; set; }

        public string? dllx { get; set; }

    }

}
