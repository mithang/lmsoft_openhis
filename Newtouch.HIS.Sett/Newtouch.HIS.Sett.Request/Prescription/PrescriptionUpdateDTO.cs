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
        /// 领药 nhà thuốc 
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// trong 药贴数
        /// </summary>
        public int? ts { get; set; }

        /// <summary>
        /// trong 药代煎 dấu hiệu 1 cần代煎
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// Xóa Chi tiết thuốc
        /// Xóa có thể以 mã 来 phán đoán duy nhất 
        /// </summary>
        public IList<PrescriptionMedicineItemDTO> DeleteMedicineItems { get; set; }

        /// <summary>
        /// Xóa Chi tiết hạng mục điều trị
        /// Xóa có thể以 mã 来 phán đoán duy nhất 
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
