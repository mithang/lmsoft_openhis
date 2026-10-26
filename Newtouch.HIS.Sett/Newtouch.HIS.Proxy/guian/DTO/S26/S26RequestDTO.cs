using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S26
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành ngoại trú ghi đ ỏ bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S26")]
    [XmlRoot("body")]
    public class S26RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }

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