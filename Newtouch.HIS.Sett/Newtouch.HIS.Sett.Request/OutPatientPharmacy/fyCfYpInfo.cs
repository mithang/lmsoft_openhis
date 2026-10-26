namespace Newtouch.HIS.Sett.Request.OutPatientPharmacy
{
    /// <summary>
    /// phát thuốc Đ ơn thuốc thuốc thông tin
    /// </summary>
    public class fyCfYpInfo
    {
        /// <summary>
        /// Mã nội bộ đ ơn thuốc
        /// </summary>
        public int cfnm { get; set; }

        /// <summary>
        /// Số đ ơn thuốc
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// Chi tiết đ ơn thuốcID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// ThuốcMã
        /// </summary>
        public string yp { get; set; }
    }
}
