using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09
{
    /// <summary>
    /// nhắclấyChẩn đoánthông tinyêu cầuthể
    /// </summary>
    [XmlRoot("Request")]
    public class ReceiveRequestEntity : RequestBase
    {
        /// <summary>
        /// nhậnthuthông tinyêu cầuthể
        /// </summary>
        public Receive Receive { get; set; }

    }
}