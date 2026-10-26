using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S30
{
    /// <summary>
    /// Tải thông tin từ điển bệnh theo dấu thời gian Bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S30")]
    [XmlRoot("body")]
    public class S30RequestDTO
    {
        /// <summary>
        /// Thời gian bắt đ ầu(yyyy-MM-dd HH:mm:ss SSS)
        /// </summary>
        public string startDate { get; set; }
    }
}