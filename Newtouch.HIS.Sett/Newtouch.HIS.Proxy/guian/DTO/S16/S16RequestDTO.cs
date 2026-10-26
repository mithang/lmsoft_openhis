using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO.S16
{
    [InterfaceCode("S16")]
    [XmlRoot("body")]
    public class S16RequestDTO
    {
        /// <summary>
        /// Nhập viện số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
    }
}