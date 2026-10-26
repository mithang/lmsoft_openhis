namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest
{
    /// <summary>
    /// Thân tham số yêu cầu tích hợp trị vị bệnh
    /// </summary>
    public class HEALRequestEntity
    {
        /// <summary>
        /// Tên tài nguyên: HEAL_CHECKBODY
        /// Bắt buộc
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Loại giấy tờ của bệnh nhân: 1-Chứng minh nhân dân, 2-Chứng nhận sĩ quan
        /// Bắt buộc
        /// </summary>
        public string CertificatesType { get; set; }

        /// <summary>
        /// Số giấy tờ của bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string CertificatesNumber { get; set; }

    }
}