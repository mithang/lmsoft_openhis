namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// xatrìnhy tếyêu cầutham số
    /// </summary>
    public class IntegrateRcDto
    {
        /// <summary>
        /// Bác sĩTài khoản
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bác sĩHọ tên
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// Bệnh nhânID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Bệnh nhânSố điện thoại
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// Bệnh nhânCMND/CCCDsố
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// Bệnh nhânGiới tính: 1 Nam 2 Nữ
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Bệnh nhânNgày sinh
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Bệnh nhânLoại: 1 tự chi trả 2 Bảo hiểm y tế
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Bảo hiểm y tếthẻSố thẻhoặcKhám bệnhthẻSố thẻ
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// Cơ sởMã
        /// </summary>
        public string organCode { get; set; }
    }
}
