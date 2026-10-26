using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// chi tiết đơn thuốc (có thể tại xếp thuốc khiến dùng)
    /// </summary>
    [NotMapped]
    public class CfmxVo : MzCfmxEntity
    {
        /// <summary>
        /// số hóa đơn
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// lời dặn của bác sĩ
        /// </summary>
        public string yszt { get; set; }

        /// <summary>
        /// thời gian thu phí
        /// </summary>
        public DateTime? sfsj { get; set; }

        /// <summary>
        /// đơn vị số lượng
        /// </summary>
        public string slStr { get; set; }

    }
}
