namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Danh sách tham Số xếp thuốc
    /// </summary>
    public class PyparListVo
    {
        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public string cfmxId { get; set; }

        /// <summary>
        /// Thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// Ngày lấy hồ sơ
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// Số chia lẻ
        /// </summary>
        public int cls { get; set; }
    }
}
