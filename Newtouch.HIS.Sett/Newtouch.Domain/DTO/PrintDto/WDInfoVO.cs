namespace Newtouch.HIS.Domain.PrintDto
{
    public class WDInfoVO
    {
        /// <summary>
        /// Mã nội bộ bệnh nhân
        /// </summary>
        public string patid { get; set; }
        /// <summary>
        /// Họ tên
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string csrq { get; set; }

        /// <summary>
        /// Khoa/Khu bệnh
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// Tên khu bệnh
        /// </summary>
        public string bqmc { get; set; }

        /// <summary>
        /// Số nhập viện
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// Loại in
        /// </summary>
        public string Flag { get; set; }

    }
}
