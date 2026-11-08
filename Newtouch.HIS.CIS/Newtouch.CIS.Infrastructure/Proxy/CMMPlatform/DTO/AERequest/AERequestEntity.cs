using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest
{
    /// <summary>
    /// biện chứng luận điều trị tập thành yêu cầu tham số thể
    /// </summary>
    public class AERequestEntity
    {
        /// <summary>
        /// tài nguồn Tên:AE_EDIT
        /// bắt buộc
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Cơ sở Số hiệu
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Bác sĩ Mã nhân viên:tức Bác sĩ Đăng nhập Hệ thống của Người dùng tên
        /// bắt buộc
        /// </summary>
        public string chisZggh { get; set; }

        /// <summary>
        /// Bác sĩ Họ tên
        /// bắt buộc
        /// </summary>
        public string chisEmpName { get; set; }

        /// <summary>
        /// nhận khám Số hiệu
        /// bắt buộc
        /// </summary>
        public string clinicId { get; set; }

        /// <summary>
        /// bệnh nhân Họ tên
        /// bắt buộc
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// bệnh nhân Giới tính-1/2/9 Nam/Nữ/Không xác định
        /// bắt buộc
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Bệnh nhân Số hiệu:Bệnh nhân duy nhất một nhận dạng
        /// bắt buộc
        /// </summary>
        public string patientid { get; set; }

        /// <summary>
        /// Quay lại Dữ liệu cho HIS của thay lý trang mặt Địa chỉ
        /// bắt buộc
        /// </summary>
        public string returnHisUrl { get; set; }
    }
}