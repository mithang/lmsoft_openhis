using System.Xml.Serialization;
using Newtouch.CIS.Compat;

namespace Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_07
{
    /// <summary>
    /// nhắclấyđiện tửHồ sơyêu cầuthể
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