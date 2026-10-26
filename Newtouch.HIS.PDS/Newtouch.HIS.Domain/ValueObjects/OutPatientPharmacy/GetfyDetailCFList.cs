namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// phát thuốc đơn thuốc thông tin thuốc
    /// </summary>
    public class GetfyDetailCFList
    {
        /// <summary>
        /// mã nội bộ đơn thuốc
        /// </summary>
        public string cfnm { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// chi tiết đơn thuốc ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// mã thuốc
        /// </summary>
        public string yp { get; set; }
    }

    /// <summary>
    /// phát thuốc chi tiết đơn thuốc
    /// </summary>
    public class FyCfmxRequest
    {
        /// <summary>
        /// mã thuốc
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// số đơn thuốc
        /// </summary>
        public string cfh { get; set; }
    }
}
