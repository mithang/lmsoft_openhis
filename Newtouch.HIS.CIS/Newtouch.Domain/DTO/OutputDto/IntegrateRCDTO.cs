namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// Tham số yêu cầu y tế từ xa
    /// </summary>
    public class IntegrateRcDto
    {
        /// <summary>
        /// Tài khoản bác sĩ
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Họ tên bác sĩ
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// Bệnh nhânID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// Họ tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Số điện thoại bệnh nhân
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// Số CMND bệnh nhân
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// Giới tính bệnh nhân: 1 Nam 2 Nữ
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Ngày sinh bệnh nhân
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Loại bệnh nhân: 1 Tự chi trả 2 BHYT
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Số thẻ BHYT hoặc số thẻ khám bệnh
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// Mã cơ sở
        /// </summary>
        public string organCode { get; set; }
    }
}