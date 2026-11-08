using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.HEALRequest
{
    /// <summary>
    /// điều trị chưa bệnh tập thành yêu cầu tham số thể
    /// </summary>
    public class HEALRequestEntity
    {
        /// <summary>
        /// tài nguồn Tên:HEAL_CHECKBODY
        /// bắt buộc
        /// </summary>
        public string resource { get; set; }

        /// <summary>
        /// Bệnh nhân chứng mục Loại:1-CMND/CCCD,2-Giấy chứng nhận sĩ quan
        /// bắt buộc
        /// </summary>
        public string CertificatesType { get; set; }

        /// <summary>
        /// Bệnh nhân Số giấy tờ
        /// bắt buộc
        /// </summary>
        public string CertificatesNumber { get; set; }

    }
}