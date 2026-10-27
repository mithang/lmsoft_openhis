using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///Nhóm thu phí lớn 
    ///Thông thường phân loại theo yêu cầu của cơ quan chủ quản cấp trên
    ///    (Phí đăng ký khám00,Phí khám chữa bệnh01,Phí thẻ từ02,Phí làm thẻ03 v.v.
    ///    BHYT ngoại trúMã varchar(2)
    ///    BHYT nội trúMã varchar(2) 
    ///    Lập lại bảng để đối ứng mã BHYT
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_sfdl", "Nhóm thu phí lớn")]
    public partial class SysChargeCategoryEntity : IEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int dlId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dlCode không được để trống")]
        [StringLength(20, ErrorMessage = "dlCode giới hạn độ dài là 20")]
        public string dlCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "dlmc không được để trống")]
        [StringLength(50, ErrorMessage = "dlmc giới hạn độ dài là 50")]
        public string dlmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ParentId { get; set; }

        /// <summary>
        /// Desc:0 Chung 1 Ngoại trú 2Nội trú
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz không được để trống")]
        [StringLength(1, ErrorMessage = "mzzybz giới hạn độ dài là 1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzprintreportcode không được để trống")]
        [StringLength(20, ErrorMessage = "mzprintreportcode giới hạn độ dài là 20")]
        public string mzprintreportcode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzprintbillcode không được để trống")]
        [StringLength(20, ErrorMessage = "mzprintbillcode giới hạn độ dài là 20")]
        public string mzprintbillcode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py không được để trống")]
        [StringLength(50, ErrorMessage = "py giới hạn độ dài là 50")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? px { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "reportdlcode giới hạn độ dài là 20")]
        public string? reportdlcode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? dllb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "fjCode giới hạn độ dài là 20")]
        public string? fjCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "fjmc giới hạn độ dài là 50")]
        public string? fjmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(19, ErrorMessage = "sn giới hạn độ dài là 19")]
        public decimal? sn { get; set; }

    }
}
