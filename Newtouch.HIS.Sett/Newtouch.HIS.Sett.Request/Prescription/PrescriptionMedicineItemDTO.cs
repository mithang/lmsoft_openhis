namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Đ ơn thuốcchi tiết thuốc
    /// </summary>
    public class PrescriptionMedicineItemDTO
    {
        /// <summary>
        /// Thuốc(mã)
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// Số lư ợng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Đ ơn vị(tên)
        /// </summary>
        public string dw { get; set; }

        /// <summary>
        /// liều lư ợng 
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// liều lư ợng Đ ơn vị
        /// </summary>
        public string jldw { get; set; }

        /// <summary>
        /// nhóm số 
        /// </summary>
        public string zh { get; set; }

        /// <summary>
        /// cách dùng mã
        /// </summary>
        public string yfCode { get; set; }
        /// <summary>
        /// chuyển tự trả dấu hiệu 1:Là 0:Không
        /// </summary>

        public int? zzfbz { get; set; }
        public string pcCode { get; set; }
    }
}
