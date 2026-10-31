using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.RCIntegrateRequest
{
    /// <summary>
    /// xatrìnhsẽkhámtậpthànhyêu cầuthể
    /// </summary>
    public class RCIntegrateRequestEntity
    {
        /// <summary>
        /// bìnhbànNgười dùngduy nhấtmột:Bác sĩĐăng nhậpHệ thốngcủaNgười dùngtên
        /// bắt buộc
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// Người dùngTên
        /// bắt buộc
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// bìnhbànbệnh nhân id
        /// bắt buộc
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// bệnh nhânKhám bệnhdòngchảysố
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// bệnh nhânHọ tên
        /// bắt buộc
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// bệnh nhânSố điện thoại
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 18  ký tựCMND/CCCDsố
        /// bắt buộc
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// Giới tính 1 Nam 2 Nữ
        /// bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// yyyy-MM-dd
        /// bắt buộc
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Bảo hiểm y tếLoại 1 tự chi trả 2 Bảo hiểm y tế
        /// bắt buộc
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Bảo hiểm y tếthẻSố thẻhoặcKhám bệnhthẻSố thẻ
        /// </summary>
        public string cardId { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// bắt buộc
        /// </summary>
        public string organCode { get; set; }

        /// <summary>
        /// gọi pc đầuchỉđịnhmẫukhối:test
        /// bắt buộc
        /// </summary>
        public string m { get; set; }

        /// <summary>
        /// gọi pc đầuchỉđịnhmẫukhốicủanào đócáiPhương pháp:/eh.bus.web.cloud.wizard.Wizard
        /// bắt buộc
        /// </summary>
        public string clz { get; set; }
    }
}