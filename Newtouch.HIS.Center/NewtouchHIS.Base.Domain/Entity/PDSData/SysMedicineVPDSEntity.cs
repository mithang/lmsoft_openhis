using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///
    ///</summary>
    [Tenant(DBEnum.PdsDb)]
    [SugarTable("V_C_xt_yp", "View từ điển thuốc")]
    public partial class SysMedicineVPDSEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypId không được để trống")]
        public int ypId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypCode không được để trống")]
        [StringLength(20, ErrorMessage = "ypCode giới hạn độ dài là 20")]
        public string ypCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ypmc không được để trống")]
        [StringLength(256, ErrorMessage = "ypmc giới hạn độ dài là 256")]
        public string ypmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "OrganizeId không được để trống")]
        [StringLength(50, ErrorMessage = "OrganizeId giới hạn độ dài là 50")]
        public string OrganizeId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "spm giới hạn độ dài là 50")]
        public string spm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "py không được để trống")]
        [StringLength(70, ErrorMessage = "py giới hạn độ dài là 70")]
        public string py { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "cfl giới hạn độ dài là 9")]
        public decimal? cfl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cfdw giới hạn độ dài là 20")]
        public string cfdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "jl giới hạn độ dài là 9")]
        public decimal? jl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "jldw giới hạn độ dài là 10")]
        public string jldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzs không được để trống")]
        [StringLength(4, ErrorMessage = "bzs giới hạn độ dài là 4")]
        public decimal bzs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "bzdw không được để trống")]
        [StringLength(20, ErrorMessage = "bzdw giới hạn độ dài là 20")]
        public string bzdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "mzcls giới hạn độ dài là 9")]
        public decimal? mzcls { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mzcldw giới hạn độ dài là 20")]
        public string mzcldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "zycls giới hạn độ dài là 9")]
        public decimal? zycls { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zycldw giới hạn độ dài là 20")]
        public string zycldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zxdw không được để trống")]
        [StringLength(20, ErrorMessage = "zxdw giới hạn độ dài là 20")]
        public string zxdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "djdw không được để trống")]
        [StringLength(20, ErrorMessage = "djdw giới hạn độ dài là 20")]
        public string djdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "lsj không được để trống")]
        [StringLength(11, ErrorMessage = "lsj giới hạn độ dài là 11")]
        public decimal lsj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(11, ErrorMessage = "pfj giới hạn độ dài là 11")]
        public decimal? pfj { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl không được để trống")]
        [StringLength(9, ErrorMessage = "zfbl giới hạn độ dài là 9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz không được để trống")]
        [StringLength(1, ErrorMessage = "zfxz giới hạn độ dài là 1")]
        public string zfxz { get; set; }

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
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jx giới hạn độ dài là 20")]
        public string jx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "yfCode giới hạn độ dài là 20")]
        public string yfCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ycmc không được để trống")]
        [StringLength(100, ErrorMessage = "ycmc giới hạn độ dài là 100")]
        public string ycmc { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ypbzdm giới hạn độ dài là 50")]
        public string ypbzdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "lsbz giới hạn độ dài là 1")]
        public bool? lsbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? mjzbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "tsbz giới hạn độ dài là 1")]
        public string tsbz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "gzy giới hạn độ dài là 1")]
        public string gzy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "mzy giới hạn độ dài là 1")]
        public string mzy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "yljsy giới hạn độ dài là 1")]
        public string yljsy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "zlff giới hạn độ dài là 2")]
        public string zlff { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "sjap giới hạn độ dài là 2")]
        public string sjap { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(6, ErrorMessage = "yl giới hạn độ dài là 6")]
        public decimal? yl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yldw giới hạn độ dài là 10")]
        public string yldw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "ypgg giới hạn độ dài là 100")]
        public string ypgg { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ybdm giới hạn độ dài là 20")]
        public string ybdm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? syts { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "dczdjl giới hạn độ dài là 9")]
        public decimal? dczdjl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "dczdsl giới hạn độ dài là 9")]
        public decimal? dczdsl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "ljzdjl giới hạn độ dài là 9")]
        public decimal? ljzdjl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(9, ErrorMessage = "ljzdsl giới hạn độ dài là 9")]
        public decimal? ljzdsl { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "pzwh giới hạn độ dài là 50")]
        public string pzwh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "yptssx giới hạn độ dài là 10")]
        public string yptssx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "ypflCode giới hạn độ dài là 20")]
        public string ypflCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "jzlx giới hạn độ dài là 1")]
        public string jzlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? mrbzq { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "ghdw giới hạn độ dài là 50")]
        public string ghdw { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? ypcd { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(1, ErrorMessage = "xzyy giới hạn độ dài là 1")]
        public bool? xzyy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(256, ErrorMessage = "xzyysm giới hạn độ dài là 256")]
        public string xzyysm { get; set; }

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
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jybz không được để trống")]
        [StringLength(20, ErrorMessage = "jybz giới hạn độ dài là 20")]
        public string jybz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "isKss không được để trống")]
        [StringLength(1, ErrorMessage = "isKss giới hạn độ dài là 1")]
        public string isKss { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
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
        public string xnhybdm { get; set; }

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
        [StringLength(50, ErrorMessage = "tsypbz giới hạn độ dài là 50")]
        public string tsypbz { get; set; }

    }
}
