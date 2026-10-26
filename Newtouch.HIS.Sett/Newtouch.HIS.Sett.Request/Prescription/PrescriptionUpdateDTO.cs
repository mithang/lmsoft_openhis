using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốc thông tinDTO
    /// </summary>
    public class PrescriptionUpdateDTO
    {
        /// <summary>
        /// Loại đ ơn thuốc Tư ơng ứng enum enumPrescriptionType
        /// </summary>
        [Required]
        public int cflx { get; set; }

        /// <summary>
        /// Loại đ ơn thuốc tên
        /// </summary>
        public string cflxmc { get; set; }

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        [Required]
        public string cfh { get; set; }

        /// <summary>
        /// Nhà thuốc nhận thuốc 
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// Số thuốc dán trong
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// Dấu hiệu sắc thuốc hộ trong thuốc, 1 cần sắc thuốc hộ
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Xóa Chi tiết thuốc
        /// Xóa có thể dùng mã để phán đoán duy nhất 
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> DeleteMedicineItems { get; set; }

        /// <summary>
        /// Xóa Chi tiết hạng mục điều trị
        /// Xóa có thể dùng mã để phán đoán duy nhất 
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
