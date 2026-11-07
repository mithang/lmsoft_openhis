using Newtouch.CIS.Compat;
namespace Newtouch.CIS.Proxy.CMMPlatform.DTO
{
    /// <summary>
    /// nhậnthuthông tinyêu cầuthể
    /// </summary>
    public class Receive
    {
        /// <summary>
        /// Cơ sở Mã
        /// bắt buộc
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// Khám bệnhdòngchảysố
        /// bắt buộc
        /// </summary>
        public string serialNo { get; set; }
    }
}