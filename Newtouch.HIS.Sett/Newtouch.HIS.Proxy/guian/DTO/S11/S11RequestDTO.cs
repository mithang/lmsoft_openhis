using Newtouch.HIS.Proxy.Attribute;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S11")]
    [XmlRoot("body")]
    public class S11RequestDTO
    {
        /// <summary>
        /// Thời gian bắt đ ầu(yyyy-mm-dd)
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        /// 截止thời gian(yyyy-mm-dd)
        /// </summary>
        public string endDate { get; set; }
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