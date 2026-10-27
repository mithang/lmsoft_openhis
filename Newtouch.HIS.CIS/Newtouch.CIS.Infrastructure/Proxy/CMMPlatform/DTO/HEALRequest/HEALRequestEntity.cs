using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest
{
    /// <summary>
    /// điều trịchưabệnhtậpthànhyêu cầutham sốthể
    /// </summary>
    public class HEALRequestEntity
    {
        /// <summary>
        /// tàinguồnTên：HEAL_CHECKBODY
        /// bắt buộc
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Bệnh nhânchứngmụcLoại：1-CMND/CCCD，2-Giấy chứng nhận sĩ quan
        /// bắt buộc
        /// </summary>
        public string CertificatesType { get; set; }

        /// <summary>
        /// Bệnh nhânSố giấy tờ
        /// bắt buộc
        /// </summary>
        public string CertificatesNumber { get; set; }

    }
}