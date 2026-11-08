using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// nhắc lấy điện tử Hồ sơ yêu cầu thể
    /// </summary>
    [XmlRoot("Request")]
    public class ReceiveRequestEntity : RequestBase
    {
        /// <summary>
        /// nhận thu thông tin yêu cầu thể
        /// </summary>
        public Receive Receive { get; set; }
    }

}