using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.AERequest
{
    /// <summary>
    /// biệnchứngluậnđiều trịtậpthànhyêu cầutham sốthể
    /// </summary>
    public class AERequestEntity
    {
        /// <summary>
        /// tàinguồnTên：AE_EDIT
        /// bắt buộc
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Cơ sởSố hiệu
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Bác sĩMã nhân viên：tứcBác sĩĐăng nhậpHệ thốngcủaNgười dùngtên
        /// bắt buộc
        /// </summary>
        public string chisZggh { get; set; }

        /// <summary>
        /// Bác sĩHọ tên
        /// bắt buộc
        /// </summary>
        public string chisEmpName { get; set; }

        /// <summary>
        /// nhậnkhámSố hiệu
        /// bắt buộc
        /// </summary>
        public string clinicId { get; set; }

        /// <summary>
        /// bệnh nhânHọ tên
        /// bắt buộc
        /// </summary>
        public string brxm { get; set; }

        /// <summary>
        /// bệnh nhânGiới tính-1/2/9 Nam/Nữ/Không xác định
        /// bắt buộc
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// Bệnh nhânSố hiệu：Bệnh nhânduy nhấtmộtnhận dạng
        /// bắt buộc
        /// </summary>
        public string patientid { get; set; }

        /// <summary>
        /// Quay lạiDữ liệucho HIS củathaylýtrangmặtĐịa chỉ
        /// bắt buộc
        /// </summary>
        public string returnHisUrl { get; set; }
    }
}