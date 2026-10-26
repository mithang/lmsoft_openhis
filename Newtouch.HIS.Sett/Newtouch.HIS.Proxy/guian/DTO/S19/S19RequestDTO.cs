using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S19
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng ngoại trú trả về sau khi tải lênoutpid tiến hành trả lại ngoại trú bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S19")]
    [XmlRoot("body")]
    public class S19RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        /// Đ ăng ký ngoại trú hủy nguyên nhân (liên tỉnh)
        /// </summary>
        public string cancelCause { get; set; }

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