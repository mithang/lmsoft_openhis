namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest
{
    /// <summary>
    /// Tích hợp bệnh án điện tử
    /// </summary>
    public class EMRRequestEntity
    {
        /// <summary>
        /// Tên tài nguyên: EMR_WRITE
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Mã bác sĩ: tức tên đăng nhập hệ thống của bác sĩ
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Mã bệnh nhân: Định danh duy nhất của bệnh nhân
        /// </summary>
        public string patientId { get; set; }
    }
}