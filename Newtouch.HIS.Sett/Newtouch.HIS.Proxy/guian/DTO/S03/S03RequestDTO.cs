using System.Xml.Serialization;
using Newtouch.HIS.Proxy.Attribute;

namespace Newtouch.HIS.Proxy.guian.DTO.S03
{
    /// <summary>
    /// theo truyền vàocủa số chứng nhận y tế lấy gia đ ình của tham gia hợp tác thành viên danh sách bản tin yêu cầu
    /// </summary>
    [InterfaceCode("S03")]
    [XmlRoot("body")]
    public class S03RequestDTO 
    {
        /// <summary>
        /// tham gia hợp tác năm 
        /// </summary>
        public string year { get; set; }

        /// <summary>
        /// số chứng nhận y tế liên tỉnh ít nhất một tham số đầu vào không thể là trống; không liên tỉnh, số chứng nhận y tế không là trống 
        /// </summary>
        public string medicalNo { get; set; }

        /// <summary>
        /// Số CMND(liên tỉnh) liên tỉnh ít nhất một tham số đầu vào không thể là trống; không liên tỉnh, số chứng nhận y tế không là trống 
        /// </summary>
        public string idcardNo { get; set; }

        /// <summary>
        /// ngư ời giám hộ số CMND(liên tỉnh) liên tỉnh ít nhất một tham số đầu vào không thể là trống; không liên tỉnh, số chứng nhận y tế không là trống 
        /// </summary>
        public string guardianCardNo { get; set; }

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