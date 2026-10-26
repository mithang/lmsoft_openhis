using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc thêm mới/Chi tiết xóa yêu cầu 
    /// </summary>
    public class PrescriptionAddOrUpdateRequest : RequestBase
    {
        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        [Required]
        public string mzh { get; set; }

        /// <summary>
        /// Bác sĩ(bác sĩ mã nhân viên)
        /// </summary>
        [Required]
        public string ys { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// Đ ơn thuốc List
        /// </summary>
        [Required]
        public IList<PrescriptionUpdateDTO> PrescriptionList { get; set; }

    }
}
