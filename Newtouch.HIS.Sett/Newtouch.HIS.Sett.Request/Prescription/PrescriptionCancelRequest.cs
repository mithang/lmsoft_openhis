using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc hủy bỏ yêu cầu 
    /// </summary>
    public class PrescriptionCancelRequest : RequestBase
    {
        /// <summary>
        /// Đ ơn thuốcList
        /// </summary>
        [Required]
        public IList<PrescriptionQueryDTO> cfList { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

    }
}
