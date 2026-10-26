using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc thêm mới yêu cầu 
    /// </summary>
    public class PrescriptionAddRequest : RequestBase
    {
        /// <summary>
        /// Số hồ sơ bệnh án
        /// </summary>
        [Required]
        public string blh { get; set; }

        /// <summary>
        /// Số khám ngoại trú
        /// </summary>
        [Required]
        public string mzh { get; set; }

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        [Required]
        public string cfh { get; set; }

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
        /// Chi tiết thuốc
        /// </summary>
        [Required]
        public IList<PrescriptionMedicineItemDTO> MedicineItems { get; set; }

        /// <summary>
        /// Chi tiết hạng mục điều trị
        /// </summary>
        [Required]
        public IList<PrescriptionTreamentItemDTO> TreamentItems { get; set; }

    }
}
