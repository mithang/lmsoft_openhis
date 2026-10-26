using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S25
{
    /// <summary>
    /// theo S18 số thứ tự bồi thư ờng trả về sau khi tải lên ngoại trúoutpid tiến hành quyết toán ngoại trú bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S25")]
    [XmlRoot("body")]
    public class S25RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string outpId { get; set; }

        /// <summary>
        ///Bồi thư ờng loại mã (liên tỉnh)
        /// </summary>
        public string redeemNo { get; set; }

        /// <summary>
        ///Ngày xuất viện(yyyy-mm-dd hh:mm:ss)(liên tỉnh, có thể truyền khi trư ớc thời gian)
        /// </summary>
        public string leaveDate { get; set; }

        /// <summary>
        /// mã hành chính (liên tỉnh)
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        ///Có phải bệnh nhân khám chữa bệnh liên tỉnh không1:Là0:Không(liên tỉnh)
        /// </summary>
        public string isTransProvincial { get; set; }
    }
}