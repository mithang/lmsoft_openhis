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
        [Required(ErrorMessage = "sfxmCode không được để trống")]
        [StringLength(20, ErrorMessage = "sfxmCode giới hạn độ dài là 20")]
        public string sfxmCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfxmmc không được để trống")]
        [StringLength(256, ErrorMessage = "sfxmmc giới hạn độ dài là 256")]
        public string sfxmmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfdlCode không được để trống")]
        [StringLength(200, ErrorMessage = "sfdlCode giới hạn độ dài là 200")]
        public string sfdlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "badlCode giới hạn độ dài là 20")]
        public string? badlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "nbdlCode giới hạn độ dài là 20")]
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
        [Required(ErrorMessage = "py không được để trống")]
        [StringLength(250, ErrorMessage = "py giới hạn độ dài là 250")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dw không được để trống")]
        [StringLength(220, ErrorMessage = "dw giới hạn độ dài là 220")]
        public string dw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dj không được để trống")]
        [StringLength(9, ErrorMessage = "dj giới hạn độ dài là 9")]
        public decimal dj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "flCode giới hạn độ dài là 20")]
        public string? flCode { get; set; }

        /// <summary>
        /// Desc:Xác định tỷ lệ tiền mặt tự trả,không liên quan tới tính chất của phần này(Tự trả theo phân loại,Tự lo)	   Xem trườngzfxz	   	   Chú ý:Khi tỷ lệ tự trả là số âm,thể hiện tự trả định mức	   (Ví dụ:Một vật tư nào đó12300,Được thanh toán10000,thì tỷ lệ tự trả=-2300)
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl không được để trống")]
        [StringLength(9, ErrorMessage = "zfbl giới hạn độ dài là 9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:Xác định phần tự trả(Chi phí×Tỷ lệ tự trả)tính chất	   0  Được thanh toán 1 Tự lo 2 Tự trả theo phân loại 3 Hoàn toàn tự lo(xử lý chuyên gia nghỉ hưu theo chế độ,Phí đăng ký khám theo yêu cầu đặc biệt)	   	   (Chi phí×(1-Tỷ lệ tự trả) là phần được ghi sổ)
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz không được để trống")]
        [StringLength(1, ErrorMessage = "zfxz giới hạn độ dài là 1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:0:Chung,1:Ngoại trú,2:Nội trú
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz không được để trống")]
        [StringLength(1, ErrorMessage = "mzzybz giới hạn độ dài là 1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:0 Không cần thực hiện,(Nội trú ghi sổ trực tiếp) 	   1 Cần thực hiện,(Nội trú sau khi thực hiện mới được ghi sổ)	   from xt_yjxmdz
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ssbz không được để trống")]
        [StringLength(1, ErrorMessage = "ssbz giới hạn độ dài là 1")]
        public string ssbz { get; set; }

        /// <summary>
        /// Desc:0 Hạng mục thông thường 1 Hạng mục đặc biệt refer xt_sfxmtsbz	   Giải thích hạng mục đặc biệt: chỉ tính chất bệnh nhân đặc thù mới được dùng. 
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "tsbz không được để trống")]
        [StringLength(1, ErrorMessage = "tsbz giới hạn độ dài là 1")]
        public string tsbz { get; set; }

        /// <summary>
        /// Desc:0 Hạng mục không thu phí(Ví dụ:Phí đăng ký khám..) 1 Hạng mục thu phí
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "sfbz không được để trống")]
        [StringLength(1, ErrorMessage = "sfbz giới hạn độ dài là 1")]
        public string sfbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ybdm giới hạn độ dài là 20")]
        public string? ybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(40, ErrorMessage = "wjdm giới hạn độ dài là 40")]
        public string? wjdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1000, ErrorMessage = "bz giới hạn độ dài là 1000")]
        public string? bz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }


        /// <summary>
        /// Desc:Số lượng tính theo đơn vị
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
        [StringLength(20, ErrorMessage = "zxks giới hạn độ dài là 20")]
        public string? zxks { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "gg giới hạn độ dài là 100")]
        public string? gg { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(23, ErrorMessage = "LastYBUploadTime giới hạn độ dài là 23")]
        public DateTime? LastYBUploadTime { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "ssdj giới hạn độ dài là 2")]
        public string? ssdj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "sqlx giới hạn độ dài là 20")]
        public string? sqlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:1
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ybbz không được để trống")]
        [StringLength(1, ErrorMessage = "ybbz giới hạn độ dài là 1")]
        public string ybbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xnhybdm giới hạn độ dài là 20")]
        public string? xnhybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "gjybdm giới hạn độ dài là 50")]
        public string? gjybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "cxjje giới hạn độ dài là 11")]
        public decimal? cxjje { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "pzwh giới hạn độ dài là 100")]
        public string? pzwh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "sccj giới hạn độ dài là 100")]
        public string? sccj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "gjybmc giới hạn độ dài là 100")]
        public string? gjybmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_Code giới hạn độ dài là 50")]
        public string? wz_Code { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_ConsumablesID giới hạn độ dài là 50")]
        public string? wz_ConsumablesID { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_SerialNumber giới hạn độ dài là 50")]
        public string? wz_SerialNumber { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_Packing giới hạn độ dài là 50")]
        public string? wz_Packing { get; set; }

        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "wz_IsHight giới hạn độ dài là 1")]
        public bool? wz_IsHight { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wz_MaterialCode giới hạn độ dài là 50")]
        public string? wz_MaterialCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "isYnss giới hạn độ dài là 1")]
        public string? isYnss { get; set; }

        public string? dllx { get; set; }

    }

}
