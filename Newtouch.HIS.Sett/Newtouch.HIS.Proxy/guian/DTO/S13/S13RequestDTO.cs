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
        /// năm hộ bảo hiểm, hộ nghèoV.v.Đ ặc biệt tham gia hợp tác bệnh nhân có hay khôngcung cấp thân phận thuộc tính tài liệu chứng minh(liên tỉnh)1:Đ ãcung cấp 0:Chưacung cấp
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