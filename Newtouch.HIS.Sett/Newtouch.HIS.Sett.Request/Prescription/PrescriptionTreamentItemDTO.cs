namespace Newtouch.HIS.Sett.Request.Prescription
{
    /// <summary>
    /// Chi tiết hạng mục điều trị
    /// </summary>
    public class PrescriptionTreamentItemDTO
    {
        /// <summary>
        /// Hạng mục thu phí(mã)
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// Số lư ợng(tính phí số lư ợng)
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// mỗi lần điều trị lư ợng 
        /// </summary>
        public int? dczll { get; set; }

        /// <summary>
        /// thực hiện số lần 
        /// </summary>
        public int? zxcs { get; set; }

        /// <summary>
        /// điều trị lư ợng (总)
        /// </summary>
        public int? zll { get; set; }

        /// <summary>
        /// Đ ơn vị(tên)
        /// </summary>
        public string dw { get; set; }
        /// <summary>
        /// chuyển tự trả dấu hiệu 
        /// </summary>
        public int? zzfbz { get; set; }

        public string ztId { get; set; }
        public string ztmc { get; set; }
        public int? ztsl { get; set; }
    }
}
