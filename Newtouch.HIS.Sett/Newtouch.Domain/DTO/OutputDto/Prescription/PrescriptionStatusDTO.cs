namespace Newtouch.HIS.Domain.DTO
{
    /// <summary>
    /// Trạng thái đơn thuốc
    /// </summary>
    public class PrescriptionStatusDTO
    {
        /// <summary>
        /// 1 0
        /// </summary>
        public string cfzt { get; set; }

        /// <summary>
        /// 1Bình thường 0Đã hủy
        /// </summary>
        public string cfztValue { get; set; }

        /// <summary>
        /// 0 1
        /// </summary>
        public string jszt { get; set; }

        /// <summary>
        /// 0Chưa quyết toán 1Đã quyết toán
        /// </summary>
        public string jsztValue { get; set; }


    }
}
