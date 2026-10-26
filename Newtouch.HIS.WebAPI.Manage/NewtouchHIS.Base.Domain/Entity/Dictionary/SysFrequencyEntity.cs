using NewtouchHIS.Lib.Base.EnumExtend;
using SqlSugar;

namespace NewtouchHIS.Base.Domain.Entity
{
    /// <summary>
    /// tần suất
    /// </summary>
    [Tenant(DBEnum.BaseDb)]
    [SugarTable("xt_ypyf", "SysFrequencyEntity")]

    public class SysFrequencyEntity : IEntity
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string yzpcId { get; set; }
        /// <summary>
        /// mã tần suất
        /// </summary>
        public string yzpcCode { get; set; }
        /// <summary>
        /// tần suất thực hiện y lệnh
        /// </summary>
        public string yzpcmc { get; set; }
        /// <summary>
        /// số lần thực hiện
        /// </summary>
        public int? zxcs { get; set; }
        /// <summary>
        /// chu kỳ thực hiện
        /// </summary>
        public int? zxzq { get; set; }
        /// <summary>
        /// đơn vị chu kỳ thực hiện
        /// </summary>
        public string? zxzqdw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? zbz { get; set; }
        /// <summary>
        /// thời điểm thực hiện
        /// </summary>
        public string? zxsj { get; set; }
        /// <summary>
        /// mô tả
        /// </summary>
        public string? yzpcmcsm { get; set; }
        public int? px { get; set; }

    }
}
