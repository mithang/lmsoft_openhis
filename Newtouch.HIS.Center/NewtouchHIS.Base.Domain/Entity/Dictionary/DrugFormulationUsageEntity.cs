using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace NewtouchHIS.Base.Domain.Entity.Dictionary
{
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_yp_jxyfdy", "Đối chiếu dạng bào chế và cách dùng thuốc")]
    public partial class DrugFormulationUsageEntity : ISysEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string Id { get; set; }

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
        [Required(ErrorMessage = "yfCode không được để trống")]
        [StringLength(50, ErrorMessage = "yfCode giới hạn độ dài là 50")]
        public string yfCode { get; set; }        
    }    
}
