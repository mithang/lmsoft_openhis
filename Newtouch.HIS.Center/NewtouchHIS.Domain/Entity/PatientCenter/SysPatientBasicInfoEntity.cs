using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Domain.Enum;
using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.Entity.PatientCenter
{
    [Tenant(DBEnum.SettDb)]
    [SugarTable("xt_brjbxx", "SysPatientBasicInfoEntity")]
    public partial class SysPatientBasicInfoEntity : IEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "patid không được để trống")]
        [SugarColumn(IsPrimaryKey = true)]
        public int patid { get; set; }


        /// <summary>
        /// Desc:本院磁Số thẻ（包括医保离休病人），或者医保Số thẻ
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "blh không được để trống")]
        [StringLength(30, ErrorMessage = "blh giới hạn độ dài là 30")]
        public string blh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "xm giới hạn độ dài là 50")]
        public string xm { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "py giới hạn độ dài là 50")]
        public string? py { get; set; }

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
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "csny giới hạn độ dài là 10")]
        public DateTime? csny { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(2, ErrorMessage = "zjlx giới hạn độ dài là 2")]
        public string? zjlx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(18, ErrorMessage = "zjh giới hạn độ dài là 18")]
        public string? zjh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_sheng giới hạn độ dài là 100")]
        public string? cs_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_shi giới hạn độ dài là 100")]
        public string? cs_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_xian giới hạn độ dài là 100")]
        public string? cs_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "cs_dz giới hạn độ dài là 100")]
        public string? cs_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "cs_yb giới hạn độ dài là 20")]
        public string? cs_yb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_sheng giới hạn độ dài là 100")]
        public string? hu_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_shi giới hạn độ dài là 100")]
        public string? hu_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_xian giới hạn độ dài là 100")]
        public string? hu_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_dz giới hạn độ dài là 100")]
        public string hu_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "hu_yb giới hạn độ dài là 100")]
        public string? hu_yb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_sheng giới hạn độ dài là 100")]
        public string? xian_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_shi giới hạn độ dài là 100")]
        public string? xian_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_xian giới hạn độ dài là 100")]
        public string? xian_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_dz giới hạn độ dài là 100")]
        public string? xian_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(100, ErrorMessage = "xian_yb giới hạn độ dài là 100")]
        public string? xian_yb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_sheng giới hạn độ dài là 200")]
        public string? jjlxr_sheng { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_shi giới hạn độ dài là 200")]
        public string? jjlxr_shi { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_xian giới hạn độ dài là 200")]
        public string? jjlxr_xian { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jjlxr_dz giới hạn độ dài là 200")]
        public string? jjlxr_dz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "jjllrgx giới hạn độ dài là 20")]
        public string? jjllrgx { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "jjllr giới hạn độ dài là 50")]
        public string? jjllr { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "jjlldh giới hạn độ dài là 50")]
        public string? jjlldh { get; set; }

        /// <summary>
        /// Desc:from xtdy
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int? dybh { get; set; }

        /// <summary>
        /// Desc:单位名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(30, ErrorMessage = "dwmc giới hạn độ dài là 30")]
        public string? dwmc { get; set; }

        /// <summary>
        /// Desc:单位地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(10, ErrorMessage = "dwdz giới hạn độ dài là 10")]
        public string? dwdz { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "xl giới hạn độ dài là 20")]
        public string? xl { get; set; }

        /// <summary>
        /// Desc:xt_gj.gj
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "gj giới hạn độ dài là 20")]
        public string? gj { get; set; }

        /// <summary>
        /// Desc:0 未婚 1 已婚
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(3, ErrorMessage = "hf giới hạn độ dài là 3")]
        public byte? hf { get; set; }

        /// <summary>
        /// Desc:民族
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "mz giới hạn độ dài là 20")]
        public string? mz { get; set; }

        /// <summary>
        /// Desc:职业
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zy giới hạn độ dài là 20")]
        public string? zy { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "zjlxfs giới hạn độ dài là 20")]
        public string? zjlxfs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(30, ErrorMessage = "dh giới hạn độ dài là 30")]
        public string? dh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(30, ErrorMessage = "phone giới hạn độ dài là 30")]
        public string? phone { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "wechat giới hạn độ dài là 50")]
        public string? wechat { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(50, ErrorMessage = "email giới hạn độ dài là 50")]
        public string? email { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(20, ErrorMessage = "brly giới hạn độ dài là 20")]
        public string? brly { get; set; }

        /// <summary>
        /// Desc:过敏史
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "gms giới hạn độ dài là 200")]
        public string? gms { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "jbs giới hạn độ dài là 200")]
        public string jbs { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(200, ErrorMessage = "bz giới hạn độ dài là 200")]
        public string? bz { get; set; }

       

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
        [StringLength(12, ErrorMessage = "dwdh giới hạn độ dài là 12")]
        public string? dwdh { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(12, ErrorMessage = "dwyb giới hạn độ dài là 12")]
        public string? dwyb { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        [StringLength(12, ErrorMessage = "jg giới hạn độ dài là 12")]
        public string? jg { get; set; }
    }
}
