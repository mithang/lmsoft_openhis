using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// nhận thu thông tin yêu cầu thể
    /// </summary>
    public class Receive
    {
        /// <summary>
        /// Cơ sở Mã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Khám bệnh dòng chảy số
        /// bắt buộc
        /// </summary>
        public string serialNo { get; set; }
    }
}