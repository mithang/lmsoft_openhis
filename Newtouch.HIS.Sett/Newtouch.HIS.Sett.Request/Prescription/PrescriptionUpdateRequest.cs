using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc sửa yêu cầu 
    /// </summary>
    public class PrescriptionUpdateRequest : RequestBase
    {
        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// Thời gian thao tác
        /// </summary>
        [Required]
        public DateTime OperateTime { get; set; }

        /// <summary>
        /// Xóa Chi tiết thuốc
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> DeleteMedicineItems { get; set; }

        /// <summary>
        /// Xóa Chi tiết hạng mục điều trị
        /// </summary>
        public IList<PrescriptionTreamentItemDTO> DeleteTreamentItems { get; set; }

        /// <summary>
        /// Thêm mới Chi tiết thuốc
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> AddMedicineItems { get; set; }

        /// <summary>
        /// Thêm mới Chi tiết hạng mục điều trị
        /// </summary>
        public IList<PrescriptionTreamentItemDTO> AddTreamentItems { get; set; }

    }
}
