using System.Collections.Generic;
using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S23
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid ngoại trú chi phí chi tiếttra cứuS22 tiến hành ngoại trú chi phí chi tiết trả lại đ ơn; liên tỉnh nếu只 truyền số thứ tự bồi thư ờng, không truyền chi tiếtID thì là tất cả các chi tiết xóa bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S23")]
    [XmlRoot("body")]
    public class S23RequestDTO
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

        /// <summary>
        /// Chi tiếtID
        /// </summary>
        public List<string> list { get; set; }

    }
}