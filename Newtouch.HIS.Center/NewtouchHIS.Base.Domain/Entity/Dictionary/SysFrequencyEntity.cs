using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// Tần suất
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypyf", "SysFrequencyEntity")]

    public class SysFrequencyEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string yzpcId { get; set; }
        /// <summary>
        /// Tần suấtMã
        /// </summary>
        public string yzpcCode { get; set; }
        /// <summary>
        /// Tần suất thực hiện y lệnh
        /// </summary>
        public string yzpcmc { get; set; }
        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        public int? zxcs { get; set; }
        /// <summary>
        /// Chu kỳ thực hiện
        /// </summary>
        public int? zxzq { get; set; }
        /// <summary>
        /// Đơn vị chu kỳ thực hiện
        /// </summary>
        public string? zxzqdw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? zbz { get; set; }
        /// <summary>
        /// Thời điểm thực hiện
        /// </summary>
        public string? zxsj { get; set; }
        /// <summary>
        /// Giải thích
        /// </summary>
        public string? yzpcmcsm { get; set; }
        public int? px { get; set; }

    }
}
