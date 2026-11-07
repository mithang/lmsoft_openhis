namespace Newtouch.Domain.DTO
{
    /// <summary>
    /// xatrìnhy tếyêu cầutham số
    /// </summary>
    public class IntegrateRcDto
    {
        /// <summary>
        /// Bác sĩ Tài khoản
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        /// Bác sĩ Họ tên
        /// </summary>
        public string doctorName { get; set; }

        /// <summary>
        /// Bệnh nhân ID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// Bệnh nhân Số điện thoại
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// Bệnh nhân CMND/CCCD số
        /// </summary>
        public string certid { get; set; }

        /// <summary>
        /// Bệnh nhân Giới tính: 1 Nam 2 Nữ
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// Bệnh nhân Ngày sinh
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// Bệnh nhân Loại: 1 tự chi trả 2 Bảo hiểm y tế
        /// </summary>
        public string patientType { get; set; }

        /// <summary>
        /// Bảo hiểm y tếthẻ Số thẻhoặc Khám bệnhthẻSố thẻ
        /// </summary>
        public string series { get; set; }

        /// <summary>
        /// Cơ sở Mã
        /// </summary>
        public string organCode { get; set; }
    }
}
