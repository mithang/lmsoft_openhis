using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// Thân yêu cầu trích xuất bệnh án điện tử
    /// </summary>
    [XmlRoot("Request")]
    public class ReceiveRequestEntity : RequestBase
    {
        /// <summary>
        /// Thân yêu cầu nhận thông tin
        /// </summary>
        public Receive Receive { get; set; }
    }


}