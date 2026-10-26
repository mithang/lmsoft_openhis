using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S22
{
    /// <summary>
    /// Tra cứu chi tiết chi phí ngoại trú lần này đ ã tải lên bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S22")]
    [XmlRoot("body")]
    public class S22RequestDTO
    {
        /// <summary>
        /// Thời gian bắt đ ầu(yyyy-mm-dd)
        /// </summary>
        public string startDate { get; set; }

        /// <summary>
        /// hạn cuốithời gian(yyyy-mm-dd)
        /// </summary>
        public string endDate { get; set; }

        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }
    }
}