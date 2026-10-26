using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S27
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành lấy quyết toán ngoại trú đ ơn thông tin bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S27")]
    [XmlRoot("body")]
    public class S27RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng ngoại trú
        /// </summary>
        public string outpId { get; set; }
    }
}