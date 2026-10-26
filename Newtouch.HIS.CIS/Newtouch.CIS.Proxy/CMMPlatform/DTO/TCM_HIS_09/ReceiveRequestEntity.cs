using System.Xml.Serialization;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// Thân yêu cầu trích xuất thông tin chẩn đoán
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