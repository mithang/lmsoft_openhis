using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S15")]
    [XmlRoot("body")]
    public class S15RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }
        /// <summary>
        /// Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }
    }
}