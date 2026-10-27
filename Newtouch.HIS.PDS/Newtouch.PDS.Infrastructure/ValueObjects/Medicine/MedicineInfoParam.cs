namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Điều kiện truy vấn thuốc
    /// </summary>
    public class MedicineInfoParam
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// Loại thuốc
        /// </summary>
        public string yplb { get; set; }

        /// <summary>
        /// Thuốc quen thuộc
        /// </summary>
        public string ypsx { get; set; }

        /// <summary>
        /// Dạng bào chế thuốc
        /// </summary>
        public string ypjx { get; set; }

        /// <summary>
        /// Trạng thái sử dụng Kiểm soát、Bình thường
        /// </summary>
        public string syzt { get; set; }

        /// <summary>
        /// Trạng thái thuốc Ngừng dùng、Bật
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// Mã nhập
        /// </summary>
        public string srm { get; set; }
    }
}
