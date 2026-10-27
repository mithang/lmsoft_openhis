namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// Thông tin thuốc đơn phát thuốc
    /// </summary>
    public class GetfyDetailCFList
    {
        /// <summary>
        /// Mã nội bộ đơn thuốc
        /// </summary>
        public string cfnm { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Chi tiết đơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string yp { get; set; }
    }

    /// <summary>
    /// Chi tiết đơn phát thuốc
    /// </summary>
    public class FyCfmxRequest
    {
        /// <summary>
        /// Mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// Số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
    }
}
