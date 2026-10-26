namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.RCIntegrateRequest
{
    /// <summary>
    /// Thân yêu cầu tích hợp hội chẩn từ xa
    /// </summary>
    public class RCIntegrateRequestEntity
    {
        /// <summary>
        /// Người dùng nền tảng duy nhất: tên đăng nhập hệ thống của bác sĩ
        /// Bắt buộc
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        /// Tên người dùng
        /// Bắt buộc
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// id bệnh nhân trên nền tảng
        /// Bắt buộc
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// Số thứ tự khám bệnh của bệnh nhân
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Số điện thoại di động của bệnh nhân
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân 18 chữ số
        /// Bắt buộc
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// Giới tính 1 Nam 2 Nữ
        /// Bắt buộc
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// yyyy-MM-dd
        /// Bắt buộc
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Loại bảo hiểm y tế 1 Tự chi trả 2 Bảo hiểm y tế
        /// Bắt buộc
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Số thẻ bảo hiểm y tế hoặc số thẻ khám bệnh
        /// </summary>
        public string cardId { get; set; }

        /// <summary>
        /// Mã tổ chức
        /// Bắt buộc
        /// </summary>
        public string organCode { get; set; }

        /// <summary>
        /// Gọi mô-đun chỉ định ở phía pc: test
        /// Bắt buộc
        /// </summary>
        public string m { get; set; }

        /// <summary>
        /// Gọi một phương thức của mô-đun chỉ định ở phía pc: /eh.bus.web.cloud.wizard.Wizard
        /// Bắt buộc
        /// </summary>
        public string clz { get; set; }
    }
}