using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypjx", "Dạng bào chế thuốc")]
    public partial class DrugFormulationEntity : ISysEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int jxId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jxCode không được để trống")]
        [StringLength(20, ErrorMessage = "jxCode giới hạn độ dài là 20")]
        public string jxCode { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [Required(ErrorMessage = "jxmc không được để trống")]
        [StringLength(50, ErrorMessage = "jxmc giới hạn độ dài là 50")]
        public string jxmc { get; set; }

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
    }    
}
