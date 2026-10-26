using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.Domain.Entity.V
{
    /// <summary>
    /// 
    /// </summary>
    [Table("V_S_xt_yzpc")]
    public class SysMedicalOrderFrequencyVEntity
    {       
        /// <summary>
        /// 
        /// </summary>
        public int yzpcId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzpcmc { get; set; }

        /// <summary>
        /// Số lần thực hiện
        /// </summary>
        public int zxcs { get; set; }

        /// <summary>
        /// Chu kỳ thực hiện
        /// </summary>
        public int zxzq { get; set; }

        /// <summary>
        /// Đơn vị chu kỳ thực hiện
        /// </summary>
        public string zxzqdw { get; set; }

        /// <summary>
        /// Thời gian thực hiện
        /// </summary>
        public string zxsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zbz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

    }
}
