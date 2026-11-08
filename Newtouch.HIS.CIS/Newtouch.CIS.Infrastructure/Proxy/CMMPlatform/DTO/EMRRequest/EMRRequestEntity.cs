using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest
{
    /// <summary>
    /// tập thành điện tử Hồ sơ
    /// </summary>
    public class EMRRequestEntity
    {
        /// <summary>
        /// tài nguồn Tên:EMR_WRITE
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Bác sĩ Số hiệu:tức Bác sĩ Đăng nhập Hệ thống của Người dùng tên
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bệnh nhân Số hiệu:Bệnh nhân duy nhất một nhận dạng
        /// </summary>
        public string patientId { get; set; }
    }
}