using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Chi tiết đơn thuốc(Có thể dùng khi xếp thuốc)
    /// </summary>
    [NotMapped]
    public class CfmxVo : MzCfmxEntity
    {
        /// <summary>
        /// Số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// Dặn dò của bác sĩ
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// Thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// Số lượng đơn vị
        /// </summary>
        public string slStr { get; set; }

    }
}
