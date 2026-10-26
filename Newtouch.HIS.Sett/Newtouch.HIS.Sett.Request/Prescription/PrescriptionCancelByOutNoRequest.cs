using Newtouch.HIS.API.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc hủy bỏ yêu cầu theo số khám ngoại trú
    /// </summary>
    public class PrescriptionCancelByOutNoRequest : RequestBase
    {
        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        [Required]
        public string mzh { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

    }
}