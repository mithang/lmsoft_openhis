using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.EMRRequest
{
    /// <summary>
    /// tậpthànhđiện tửHồ sơ
    /// </summary>
    public class EMRRequestEntity
    {
        /// <summary>
        /// tàinguồnTên:EMR_WRITE
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Bác sĩSố hiệu:tứcBác sĩĐăng nhậpHệ thốngcủaNgười dùngtên
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bệnh nhânSố hiệu:Bệnh nhânduy nhấtmộtnhận dạng
        /// </summary>
        public string patientId { get; set; }
    }
}