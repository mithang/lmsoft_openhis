using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S17")]
    [XmlRoot("body")]
    public class S17RequestDTO
    {
        /// <summary>
        /// Thời gian bắt đ ầu
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// hạn cuốithời gian
        /// </summary>
        public string endDate { get; set; }
        /// <summary>
        /// Số thẻ chứng nhận y tế
        /// </summary>
        public string medicalNo { get; set; }
        /// <summary>
        /// Tên bệnh nhân
        /// </summary>
        public string memberName { get; set; }
    }
}