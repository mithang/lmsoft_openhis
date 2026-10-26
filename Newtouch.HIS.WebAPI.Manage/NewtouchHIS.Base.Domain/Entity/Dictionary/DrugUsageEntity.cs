using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// cách dùng thuốc
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypyf", "DrugUsageEntity")]
    public class DrugUsageEntity : ISysEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string yfId { get; set; }
        /// <summary>
        /// mã cách dùng
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// tên cách dùng
        /// </summary>
        public string yfmc { get; set; }
        public string? py { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }
        /// <summary>
        /// 1 thuốc Tây y 2 thuốc Đông y
        /// </summary>
        public string yplx { get; set; }

    }
}
