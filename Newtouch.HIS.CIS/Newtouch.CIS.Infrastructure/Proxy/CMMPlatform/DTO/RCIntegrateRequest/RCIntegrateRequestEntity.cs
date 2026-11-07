using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.RCIntegrateRequest
{
    /// <summary>
    /// xatrìnhsẽkhámtậpthànhyêu cầuthể
    /// </summary>
    public class RCIntegrateRequestEntity
    {
        /// <summary>
        /// bìnhbàn Người dùngduy nhấtmột:Bác sĩ Đăng nhập Hệ thốngcủa Người dùngtên
        /// bắt buộc
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// Người dùng Tên
        /// bắt buộc
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// bìnhbànbệnh nhân id
        /// bắt buộc
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// bệnh nhân Khám bệnhdòngchảysố
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// bệnh nhân Họ tên
        /// bắt buộc
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// bệnh nhân Số điện thoại
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 18  ký tự CMND/CCCD số
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
        /// Bảo hiểm y tế Loại 1 tự chi trả 2 Bảo hiểm y tế
        /// bắt buộc
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Bảo hiểm y tếthẻ Số thẻhoặc Khám bệnhthẻ Số thẻ
        /// </summary>
        public string cardId { get; set; }

        /// <summary>
        /// Cơ sở Mã
        /// bắt buộc
        /// </summary>
        public string organCode { get; set; }

        /// <summary>
        /// gọi pc đầuchỉđịnhmẫukhối:test
        /// bắt buộc
        /// </summary>
        public string m { get; set; }

        /// <summary>
        /// gọi pc đầuchỉđịnhmẫukhốicủanào đócái Phương pháp:/eh.bus.web.cloud.wizard.Wizard
        /// bắt buộc
        /// </summary>
        public string clz { get; set; }
    }
}