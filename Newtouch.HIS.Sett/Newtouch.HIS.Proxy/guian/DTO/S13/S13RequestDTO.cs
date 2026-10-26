using Newtouch.HIS.Proxy.Attribute;
using System.Xml.Serialization;

namespace Newtouch.HIS.Proxy.guian.DTO
{
    [InterfaceCode("S13")]
    [XmlRoot("body")]
    public class S13RequestDTO
    {
        /// <summary>
        /// Số thứ tự bồi thư ờng
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// Bồi thư ờng loại mã 
        /// </summary>
        public string redeemNo { get; set; }
        /// <summary>
        /// Thời gian xuất viện(liên tỉnh)
        /// </summary>
        public string outDate { get; set; }
        /// <summary>
        /// năm 保户, 低保户V.v.Đ ặc biệt tham gia hợp tác bệnh nhân có hay không提供 thân phận thuộc tính 证明材料(liên tỉnh)1:Đ ã提供 0:Chưa提供
        /// </summary>
        public string isMaterials { get; set; }
        /// <summary>
        /// Thao tác viên họ tên(liên tỉnh)
        /// </summary>
        public string operationName { get; set; }
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