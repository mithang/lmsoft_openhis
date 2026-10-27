using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    ///<summary>
    ///包括挂号（挂号费00、诊疗费01、磁卡费02、工本费03 等）、收费项目、检验项目、
    /// 注意：当注销收费项目时，需要校验其实效性（例：是否正被用在床位费设置上）
    ///费用分类见“系统病人收费算法”
    ///</summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_sfxm", "收费项目")]
    public partial class SysChargeItemEntity : IEntity
    {

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
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
        /// Desc:确定自负现金比例，无关这部分的性质（分类自负、自理）	   见字段zfxz	   	   注意：当自负比例为负数时，表示定额自负	   (例：某材料12300，可报10000,那么自负比例＝-2300)
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfbl không được để trống")]
        [StringLength(9, ErrorMessage = "zfbl giới hạn độ dài là 9")]
        public decimal zfbl { get; set; }

        /// <summary>
        /// Desc:确定自负部分（费用×自负比例）的性质	   0  可报 1 自理 2 分类自负 3 绝对自理（处理离休专家、特需挂号费）	   	   （费用×（1－自负比例） 为可记帐部分）
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "zfxz không được để trống")]
        [StringLength(1, ErrorMessage = "zfxz giới hạn độ dài là 1")]
        public string zfxz { get; set; }

        /// <summary>
        /// Desc:0：通用，1：门诊，2：住院
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "mzzybz không được để trống")]
        [StringLength(1, ErrorMessage = "mzzybz giới hạn độ dài là 1")]
        public string mzzybz { get; set; }

        /// <summary>
        /// Desc:0 无需实施，（住院直接入帐） 	   1 需要实施，（住院实施后方可入帐）	   from xt_yjxmdz
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "ssbz không được để trống")]
        [StringLength(1, ErrorMessage = "ssbz giới hạn độ dài là 1")]
        public string ssbz { get; set; }

        /// <summary>
        /// Desc:0 普通项目 1 特殊项目 refer xt_sfxmtsbz	   特殊项目说明是特定的病人性质才可使用的。 
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "tsbz không được để trống")]
        [StringLength(1, ErrorMessage = "tsbz giới hạn độ dài là 1")]
        public string tsbz { get; set; }

        /// <summary>
        /// Desc:0 非收费项目（例：挂号费..） 1 收费项目
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
        /// Desc:单位计数量
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

        ///// <summary>
        ///// Desc:
        ///// Default:
        ///// Nullable:True
        ///// </summary>           
        //[StringLength(1, ErrorMessage = "isYnss giới hạn độ dài là 1")]
        //public string? isYnss { get; set; }

    }

}
