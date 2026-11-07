using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest
{
    /// <summary>
    /// tậpthànhđiện tử Hồ sơ
    /// </summary>
    public class EMRRequestEntity
    {
        /// <summary>
        /// tàinguồn Tên:EMR_WRITE
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Bác sĩ Số hiệu:tức Bác sĩ Đăng nhập Hệ thốngcủa Người dùngtên
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bệnh nhân Số hiệu:Bệnh nhânduy nhấtmộtnhận dạng
        /// </summary>
        public string patientId { get; set; }
    }
}