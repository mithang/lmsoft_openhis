using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// Cách dùng thuốc
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypyf", "DrugUsageEntity")]
    public class DrugUsageEntity : ISysEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string yfId { get; set; }
        /// <summary>
        /// Cách dùngMã
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// Tên cách dùng
        /// </summary>
        public string yfmc { get; set; }
        public string? py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 1 Thuốc tây 2 Thuốc đông y
        /// </summary>
        public string yplx { get; set; }

    }
}
