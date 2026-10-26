namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// xếp thuốc tham số danh sách
    /// </summary>
    public class PyparListVo
    {
        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// chi tiết đơn thuốc ID
        /// </summary>
        public string cfmxId { get; set; }

        /// <summary>
        /// thuốc
        /// </summary>
        public string yp { get; set; }

        /// <summary>
        /// số lượng
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// ngày rút hồ sơ
        /// </summary>
        public string tybz { get; set; }

        /// <summary>
        /// tách lẻ số
        /// </summary>
        public int cls { get; set; }
    }
}
