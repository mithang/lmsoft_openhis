namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest
{
    /// <summary>
    /// Thân tham số yêu cầu tích hợp biện chứng luận trị
    /// </summary>
    public class AERequestEntity
    {
        /// <summary>
        /// Tên tài nguyên: AE_EDIT
        /// Bắt buộc
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Số tổ chức
        /// Bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Mã nhân viên bác sĩ: tức tên đăng nhập hệ thống của bác sĩ
        /// Bắt buộc
        /// </summary>
        public string chisZggh { get; set; }

        /// <summary>
        /// Tên bác sĩ
        /// Bắt buộc
        /// </summary>
        public string chisEmpName { get; set; }

        /// <summary>
        /// Số tiếp nhận
        /// Bắt buộc
        /// </summary>
        public string clinicId { get; set; }

        /// <summary>
        /// Tên bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// Giới tính bệnh nhân -1/2/9 Nam/Nữ/Không rõ
        /// Bắt buộc
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Mã bệnh nhân: Định danh duy nhất của bệnh nhân
        /// Bắt buộc
        /// </summary>
        public string patientid { get; set; }

        /// <summary>
        /// Địa chỉ trang đại diện trả dữ liệu về HIS
        /// Bắt buộc
        /// </summary>
        public string returnHisUrl { get; set; }
    }
}