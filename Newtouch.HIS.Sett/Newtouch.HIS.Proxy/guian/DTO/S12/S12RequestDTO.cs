using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S12")]
    [XmlRoot("body")]
    public class S12RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng(liên tỉnh xóa một phầnchi tiết cần truyền đ ơn dòngchi tiết của thứ tự số,nếu không truyền chi tiết thứ tự số xóa tất cả các chi tiết)
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
        [XmlArray("list"), XmlArrayItem("string")]
        public List<string> list { get; set; }
    }
}